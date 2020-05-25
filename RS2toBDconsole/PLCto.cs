using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;


using HWDiag;
using System.Diagnostics.Eventing;
using LoggerInSystem;
//using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;
using System.Diagnostics;

using System.Data.SqlClient;
using System.Data;
using System.Collections;

using System.Net;
using System.Net.Sockets;
using System.IO;



    class ContData
    {
        public int startbit;
        public int coefficient;
        public bool floatdata;
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
        float speed4kl, H5_work, h5w, Bw, D_tek_mot, B_Work, D_pred_mot = 0, Ves_Work, Dlina_Work;
        DateTime Time_Start, Time_Stop;

        bool blRulonSaveData101ms;

        DataTable dt101ms;

        DateTime dtSQL;
        DateTime dt1s;
        DateTime dtMessage;

        Timer TTimer100ms;
        Timer TTimerMessage;
        Timer TTimerSQL;
        Timer TTimer1s;
        //Timer TTimer250msNet;

        SqlConnection conBD;
        string connectingString;


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
        private Dictionary<string, ContData> dic101ms;
        public Dictionary<string, ContData> Data101ms
        {
            get { return dic101ms; }

            set { dic101ms = value; }
        }

        #endregion

        #region plctodb101ms - Cвойство включения сбора информации с циклом 101ms для записи в БД
        private bool plctodb101ms;  
        public bool blPLStoDB101ms
        {
            get {return plctodb101ms ; }
            set {plctodb101ms=value; }
        }
        #endregion

        #region plctodbmessage - Cвойство включения сбора информации - сообщения
        private bool plctodbmessage; 
        public bool blPLStoDBMessage
        {
            get {return plctodbmessage; }
            set {plctodbmessage=value; }
        }
        #endregion

        #region plctodb1s - Свойство включения сбора информации - 1сек
        private bool plctodb1s;
        public bool blPLStoDB1s
        {
             get{return plctodb1s; }
             set{plctodb1s=value; }
        }
        private bool PasportRulona;
        public bool blPLSPasportRulona
        {
            get { return PasportRulona; }
            set { PasportRulona=value; }
        }
        #endregion






        public PLCto()
        {
            
        }



        #region Start() Внешний метод  создаеющий соединение с контроллером и запускающий метод считывание данных с контроллера
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


                        //TTimer100ms = new Timer(new TimerCallback(TicTimer100ms), null, 0, 100);
                        TTimer100ms = new Timer(100);
                        TTimer100ms.Elapsed += TicTimer100ms;
                        TTimer100ms.AutoReset = true;
                        TTimer100ms.Enabled = true;


                       // if (plctodbmessage)  TTimerMessage = new Timer(new TimerCallback(TicTimerMessage), null, 0, 200);
                       // if (plctodb101ms) TTimerSQL = new Timer(new TimerCallback(TicTimerSQL), null, 0, 101);
                       // if (plctodb1s) TTimer1s = new Timer(new TimerCallback(TicTimer1s), null, 0, 1000);

                        CreateTable(); //В случае успешного подключения к контроллеру формируем таблицу для формирования данных и последующего сохранения в БД

                    }
                    else
                    {
                        LogSystem.Write("Start", Direction.WARNING, "Соединение не активировано. " + stan.Error(resSAC));

                    }

                }

                
            }
            catch (Exception ex)
            {
                /*все исключения кидаем в пустоту*/
                LogSystem.Write("Start-"+ex.Source, Direction.ERROR, "Start Error-"+ex.Message);
            }
        }
        private void CreateTable()
        {
            #region Формируем таблицу для формирования данных и последующего сохранения в БД
            dt101ms = new DataTable();
            dt101ms.Reset();
            dt101ms.Columns.Add("dtStan", typeof(DateTime));//Для хранения даты и времени
            foreach (var item in dic101ms)
            {
                dt101ms.Columns.Add(item.Key, item.Value.floatdata ? typeof(float) : typeof(int));
            }
            #endregion
        }

        #endregion

        #region считывание данных с контроллера и запись их в критичный буфер
        private void TicTimer100ms(object source, ElapsedEventArgs e)
        {
        Console.WriteLine(e.SignalTime.ToString("HH:mm:ss.fff"));
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
                    
                   // Thread PLS100ms = new Thread(BufferToBuffer);
                   // PLS100ms.Start();
                    
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

            Console.WriteLine("Контроллер 100ms-"+DateTime.Now.ToString("HH:mm:ss.fff"));
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

                //Console.WriteLine("Поток ="+Thread.CurrentThread.ManagedThreadId.ToString());

                //Console.WriteLine(string.Format("\t\t {0} ({1}) {2}", "SQL 101mc", DateTime.Now - dtSQL, Thread.CurrentThread.ManagedThreadId));
                dtSQL = DateTime.Now;


                //Из критичной секции получаем значения из PLC
                //Thread tSQL = new Thread(BufferSQLToBufferPLC);
                //tSQL.Start();


                if (bufferSQL == null)
                {
                    Console.WriteLine("Кол-во элементтов - 0 ");

                }
                else
                {

                    if (blRulonSaveData101ms)
                    {
                        //Формируем строку для таблицы и ее записываем при условии что начата прокатка рулона
                        DataRow dr101ms = dt101ms.NewRow();
                        dr101ms["dtStan"] = DateTime.Now;

                        foreach (var item in dic101ms)
                        {
                            if (item.Value.floatdata)
                            {
                                //Console.WriteLine(item.Key + " = " + item.Value.startbit + " - " + item.Value.coefficient);
                                float a = (float)(BitConverter.ToInt16(bufferSQL, item.Value.startbit)) / item.Value.coefficient;
                                //Console.WriteLine(a);
                                dr101ms[item.Key] = a;
                            }
                            else
                            {
                                //Console.WriteLine(item.Key + " = " + item.Value.startbit + " - " + item.Value.coefficient);
                                dr101ms[item.Key] = (BitConverter.ToInt16(bufferSQL, item.Value.startbit)) / item.Value.coefficient;

                            }
                        }

                        dt101ms.Rows.Add(dr101ms);
                     //   Console.WriteLine(" Кол-во строк в таблице=" +  dt101ms.Rows.Count);
                    }

                   

                    //Console.WriteLine("В массиве строчек "+dt101ms.Rows.Count);

                    if (PasportRulona)
                    {
                        D_tek_mot = (float)(BitConverter.ToInt16(bufferSQL, 20)) / 1000;
                        
                        h5w = (float)(BitConverter.ToInt16(bufferSQL, 12)) / 1000;
                        
                        speed4kl = (float)(BitConverter.ToInt16(bufferSQL, 6)) / 100;
                        
                        Bw = BitConverter.ToInt16(bufferSQL, 14);

                        #region Формирование признака окончания прокатки рулона
                        if (D_tek_mot>D_pred_mot)
                        {
                            if (D_tek_mot<0.615)
                            {
                                Time_Start = DateTime.Now;
                                blRulonSaveData101ms = true; //включаем сбор данных по прокатке рулона
                            }
                        }

                        //Console.WriteLine("Time_Start=" + Time_Start + "  H5_work=" + H5_work + "  D_tek_mot=" + D_tek_mot + "  D_pred_mot=" + D_pred_mot + " speed4kl=" + speed4kl);
                        if ((Time_Start != new DateTime()) && (H5_work == 0) && (D_tek_mot>0.7)&&(speed4kl>2))
                        {
                            H5_work = h5w;
                            B_Work = Bw;
                        }

                        #endregion

                        #region Окончание прокатки рулона 
                        //Console.WriteLine("Time_Start="+ Time_Start+ "  H5_work="+ H5_work+ "  D_tek_mot=" + D_tek_mot+ "  D_pred_mot=" + D_pred_mot);
                        if ((Time_Start != new DateTime()) && (H5_work != 0) && (D_tek_mot < 0.610) && (D_tek_mot < D_pred_mot))
                        {
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

                            //Time_Stop = DateTime.Now;
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Время начала записи SQL=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                            Ves_Work = (((((D_pred_mot * D_pred_mot) - 0.36F) * 3.141593F) / 4) * (B_Work / 1000)) * 7.85F;
                            Time_Stop = DateTime.Now;

                            Dlina_Work = ((Ves_Work / 7.85F) / (B_Work / 1000)) / (H5_work / 1000);

                            //Ellipse101ms.Fill = onOK;

                            #region Формируем данные для передачи в Базу Данных

                            //yyyy - MM - dd HH: mm: ss.fff
                            string strTimeStart = Time_Start.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            string strTimeStop = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                            TimeSpan tp = Time_Stop.Subtract(Time_Start);
                            double dbltp = tp.TotalMilliseconds;
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Время сбора информации (ms):" + dbltp.ToString());
                            Console.WriteLine("Кол-во строк полученных в системе:" + dt101ms.Rows.Count.ToString());
                            Console.WriteLine("Среднее время цикла обновления данных:" + (dbltp / (dt101ms.Rows.Count)).ToString());
                            Console.ResetColor();


                            string strNumberRulon = DateTime.Now.ToString("yyyyMMdd") + Time_Start.ToString("HHmm") + DateTime.Now.ToString("HHmm");
                            #endregion


                            //TODO после записи удаляем таблицу и заново создаем
                            dt101ms.Clear();
                            Console.WriteLine("Очистка таблицы");

                            //CreateTable();
                            Console.WriteLine("Кол-во строк в таблице после очистки - " + dt101ms.Rows.Count.ToString());


                        }

                        D_pred_mot = D_tek_mot;
                        #endregion







                    }




                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("ytne");
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

                //TODO добавить закрытие сетевого соединения
            }
            catch (Exception ex)
            {
                LogSystem.Write("StanStop", Direction.WARNING, "Ошибка при остановке. " + ex.Message);
            }

        }
        #endregion

        

    }
