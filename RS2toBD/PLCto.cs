using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerInSystem;
using HWDiag;
using System.Threading;

namespace RS2toBD
{
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
            get { return Amount; }
            set { amount = value; }
        }
        #endregion

        #region Свойства для формирования таблицы 101ms и запись в БД

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
                        TTimerMessage = new Timer(new TimerCallback(TicTimerMessage), null, 0, 200);
                        TTimerSQL = new Timer(new TimerCallback(TicTimerSQL), null, 0, 101);
                        TTimer1s = new Timer(new TimerCallback(TicTimer1s), null, 0, 1000);

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
        private void TicTimerSQL(object state)
        {

        }
        #endregion

        private void TicTimer1s(object state)
        {

        }


        private void TicTimerMessage(object state)
        {

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
