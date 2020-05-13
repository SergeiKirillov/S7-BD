using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using HWDiag;
using System.Diagnostics.Eventing;
using LoggerInSystem;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

using System.Data.SqlClient;
using System.Data;
using System.Collections;

using System.Net;
using System.Net.Sockets;
using System.IO;


namespace RS2toBD
{
    class ContData
    {
        private int startbit;
        private int coefficient;
        private bool floatdata;
        public ContData(int startBit, int Coefficient, bool floatData)
        {
            startbit = startBit;
            coefficient = Coefficient;
            floatdata = floatData;
        }
    }

    class PLCto
    {
        Prodave stan;
        int Connect = 0;

        byte[] buffer; //данные c контроллера 100ms
        byte[] bufferPLC;
        byte[] bufferSQL;
        byte[] bufferMessage;
        byte[] bufferMessageOld;
        byte[] buffer1s;
        byte[] bufferNet;
        readonly object locker = new object();
        readonly object locker2 = new object();

        DateTime dtSQL;
        DateTime dt1s;
        DateTime dtMessage;

        Timer TTimer100ms;
        Timer TTimerMessage;
        Timer TTimerSQL;
        Timer TTimer1s;
        //Timer TTimer250msNet;

        #region Свойства определяющие настройки связи с контроллером

        private byte[] conn; //ip адресс контроллера
        public byte[] IPconnPLC
        {
            get { return conn; }

            set { conn = value; }
        }

        private int slot;
        public int SlotconnPC
        {
            get { return slot; }
            set { slot = value; }
        }
        private int rack;
        public int RackconnPC
        {
            get { return rack; }
            set { rack = value; }
        }
        private int startBuffer;
        public int StartAdressTag
        {
            get { return startBuffer; }
            set { startBuffer = value; }
        }
        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        #endregion

        #region Свойства определяющие как обрабатывать данные получаемые с контроллера
        //таблица значений имени тега и его стартовогоБита 
        private Dictionary<string, ContData> dt101ms;
        public Dictionary<string, ContData> Data101ms
        {
            get { return dt101ms; }

            set { dt101ms = value; }
        }

        #endregion

        #region plctodb101ms - Cвойство включения сбора информации с циклом 101ms
        private bool plctodb101ms;  
        public bool PLStoDB101ms
        {
            get {return plctodb101ms ; }
            set {plctodb101ms=value; }
        }
        #endregion

        #region plctodbmessage - Cвойство включения сбора информации - сообщения
        private bool plctodbmessage; 
        public bool PLStoDBMessage
        {
            get {return plctodbmessage; }
            set {plctodbmessage=value; }
        }
        #endregion

        #region plctodb1s - Свойство включения сбора информации - 1сек
        private bool plctodb1s;
        public bool PLStoDB1s
        {
             get{return plctodb1s; }
             set{plctodb1s=value; }
        }
        #endregion  






        public PLCto()
        {
            
        }



        #region Внешний метод  создаеющий соединение с контроллером и запускающий метод считывание данных с контроллера
        public void Start()
        {

            //Метод производит подключение к котроллеру и устанавливает связь
            //Если соединение успешно то вызывает поток-таймеры. и внутри них выполнение действия по таймеру.

            try
            {
                //запуск таймеров 100ms(100ms), 101ms(SQL), 200ms(message), 1000ms(1s)
                stan = new Prodave();
                 
                int res = stan.LoadConnection(Connect, 2, conn, slot, rack);

                if (res != 0)
                {
                    LogSystem.Write("Start", Direction.ERROR, "Error connection!. Error - " + stan.Error(res));

                }
                else
                {
                    LogSystem.Write("Start", Direction.Ok, "Connect OK!");

                    int resSAC = stan.SetActiveConnection(Connect);
                    if (resSAC == 0)
                    {
                        LogSystem.Write("Start", Direction.Ok, "Соединение активно.");
                        
                        TTimer100ms = new Timer(new TimerCallback(TicTimer100ms), null, 0, 100);

                        if (plctodbmessage) TTimerMessage = new Timer(new TimerCallback(TicTimerMessage), null, 0, 200);
                        if (plctodb101ms) TTimerSQL = new Timer(new TimerCallback(TicTimerSQL), null, 0, 101);
                        if (plctodb1s) TTimer1s = new Timer(new TimerCallback(TicTimer1s), null, 0, 1000);
 
                    }
                    else
                    {
                        LogSystem.Write("Start", Direction.WARNING, "Соединение не активировано. " + stan.Error(resSAC));

                    }

                }
            }
            catch(Exception ex)
            {
                /*все исключения кидаем в пустоту*/
                LogSystem.Write("Start-"+ex.Source, Direction.ERROR, "Start Error-"+ex.Message);
            }
        }

        #endregion

        #region считывание данных с контроллера и запись их в критичный буфер
        private void TicTimer100ms(object state)
        {
            Connect100ms();
        }


        private void Connect100ms()
        {
            try
            {
                //short[] buffer_array = new short[158];

                DateTime dt100ms;


                


                //var buffer2 = new ushort[128];
                int Byte_Col_r = 0;

                int resultReadField = stan.field_read('M', 0, startBuffer, amount, out buffer, out Byte_Col_r);
                if (resultReadField == 0)
                {
                    dt100ms = DateTime.Now;
                    
                    Thread PLS100ms = new Thread(BufferToBuffer);
                    PLS100ms.Start();
                    
                }
                else
                {
                    LogSystem.Write("100ms", Direction.ERROR, "Error.Read fied M3000-M3315. " + stan.Error(resultReadField));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Write("100ms", Direction.ERROR, "ИСТОЧНИК: " + ex.Source.ToString() + ".  ОШИБКА: " + ex.Message.ToString());
            }

        }

        void BufferToBuffer()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                bufferPLC = buffer;
            }

        }

        
        #endregion

        #region формирование таблицы рулонов и после окончания прокатки запись в БД

        void BufferSQLToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                bufferSQL = bufferPLC;
            }

        }

        private void TicTimerSQL(object state)
        {
            //??????вопрос с dispatcher????????
            string numberTable;
            try
            {
                //Console.WriteLine(string.Format("\t\t {0} ({1}) {2}", "SQL 101mc", DateTime.Now - dtSQL, Thread.CurrentThread.ManagedThreadId));
                dtSQL = DateTime.Now;

                //Из критичной секции получаем значения из PLC
                Thread tSQL = new Thread(BufferSQLToBufferPLC);
                tSQL.Start();



                //TODO Формировать при сохранении рулона
                #region  Формируем шифр таблицы (yyyyMMddсмена)

                if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 7 && Convert.ToInt32(DateTime.Now.ToString("HH")) < 19)
                {
                    numberTable = DateTime.Now.ToString("yyyyMMdd") + "2";
                }
                else if (Convert.ToInt32(DateTime.Now.ToString("HH")) < 7)
                {
                    numberTable = DateTime.Now.ToString("yyyyMMdd") + "1";
                }
                else if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 19)
                {
                    numberTable = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "1";
                }

                #endregion

            }
            catch (Exception ex)
            {
                LogSystem.Write("Stan(101ms)-" + ex.Source, Direction.ERROR, "Stan(101ms) Error-" + ex.Message);

            }
            
        }
        #endregion

        private void TicTimer1s(object state)
        {
            try
            {
                //Console.WriteLine(string.Format("\t\t {0} ({1}) {2}", "1c", DateTime.Now - dt1s, Thread.CurrentThread.ManagedThreadId));
                //dt1s = DateTime.Now;

            }
            catch (Exception ex)
            {

                LogSystem.Write("Stan(1s)-" + ex.Source, Direction.ERROR, "Stan(1s) Error-" + ex.Message);
            }

        }


        private void TicTimerMessage(object state)
        {
            try
            {
                //Console.WriteLine(string.Format("\t\t {0} ({1}) {2}", "Message", DateTime.Now - dtMessage, Thread.CurrentThread.ManagedThreadId));
                //dtMessage = DateTime.Now;
            }
            catch (Exception ex)
            {

                LogSystem.Write("Stan(Message)-" + ex.Source, Direction.ERROR, "Stan(Vtssage) Error-" + ex.Message);
            }
        }


        #region Метод выполняемый при остановке 
        public void Stop()
        {
            try
            {
                //TODO Закрытие таймеров
                TTimer100ms.Dispose();
                TTimerMessage.Dispose();
                TTimerSQL.Dispose();
                TTimer1s.Dispose();


                int result = stan.UnloadConnection(0);

                if (result == 0)
                {
                    //LogSystem.WriteEventLog("ProDaveStan", "Test", "Close connection", EventLogEntryType.Information);
                }
                else
                {
                    LogSystem.Write("StanStop", Direction.ERROR, "Connect open. Warning - " + stan.Error(result));
                }

            }
            catch (Exception ex)
            {
                LogSystem.Write("StanStop", Direction.WARNING, "Ошибка при остановке. " + ex.Message);
            }

        }
        #endregion

    }
}
