using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HWDiag;
using LoggerInSystem;

using System.Data;
using System.Data.SqlClient;


namespace consoleRS2toBD
{
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

    class clPLCtoBD
    {
        #region Свойства внутренние
       

        byte[] buffer;          //данные c контроллера 100ms
        byte[] bufferPLC;       //Промежуточное хранение даных
        byte[] bufferSQL;       //Данные 101мс
        byte[] bufferMessage;   //Данные сообщений
        byte[] bufferMessageOld;//Данные сообщений
        byte[] buffer1s;        //Технологические данные
        byte[] bufferNet;       //Передача по сети (визуализация)

        readonly object locker1 = new object();
        readonly object locker2 = new object();

        float speed4kl, H_work, hw, Bw, D_tek_mot, B_Work, D_pred_mot = 0, Ves_Work, Dlina_Work;

        DataTable dt101ms;

        bool blRulonProkatSaveInData101ms;
        DateTime TimeStart;
        DateTime TimeStop;

        SqlConnection connectSQL;

        string SQLAll;


        #endregion

        #region Свойства внешние

        string namePLC;
        public String NamePLC
        {
            get { return namePLC; }
            set { namePLC = value; }
        } 

        int curLeft;
        public int CursorPositionLeft
        {
            get { return curLeft; }
            set { curLeft = value; }
        }

        int curTop;
        public int CursorPositionTop
        {
            get { return curTop; }
            set { curTop = value; }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

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

        private int Connect;
        public int connect
        {
            get { return Connect; }
            set { Connect = value; }

        }

        private Dictionary<string, ContData> dic101ms;
        public Dictionary<string, ContData> Data101ms
        {
            get { return dic101ms; }

            set { dic101ms = value; }
        }

        private int startBuffer;
        public int StartAdressTag
        {
            get { return startBuffer; }
            set { startBuffer = value; }
        }

        private double diametr_motalki;
        public double dMot
        {
            get { return diametr_motalki; }
            set { diametr_motalki = value; }
        }

       

        #endregion

        #region Критичные секции


        void BufferToBuffer()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker1)
            {

                //Array.Clear(bufferPLC, 0, bufferPLC.Length);
                bufferPLC = buffer;
            }

        }


        void BufferSQLToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker2)
            {
                //Array.Clear(bufferSQL, 0, bufferSQL.Length);
                bufferSQL = bufferPLC;
            }

        }

        #endregion

       
        public void Start()
        {
            SQLconDB();

            Thread queryPLC = new Thread(PLC);
            queryPLC.Start();

            Thread querySQL = new Thread(SQL101ms2);
            querySQL.Start();

            //Thread queryMes = new Thread(Message200ms);
            //queryMes.Start();

            //Thread query1s = new Thread(SQL1s);
            //query1s.Start();

            while (true)
            {
                Thread.Sleep(5000); //??????????????????????????????????????????????????????????????????????????????????????
                //Console.WriteLine(DateTime.Now.ToString()+" - "+namePLC);
                Console.WriteLine(SQLAll);
            }
        }

        #region Соединение и прием данных с контроллера
        private void PLC()
        {
            try
            {
                int i = curTop;         //начальная позиция по Top
                int y = curTop + 2;     //Конечная позиция по Top

                Prodave rs2 = new Prodave();

                buffer = new byte[amount];
                bufferPLC = new byte[amount];
                bufferSQL = new byte[amount];

                int resultReadField=5;


                while (true)
                {
                    Thread.Sleep(100);

                    if (resultReadField != 0)
                    {
                        int res = rs2.LoadConnection(Connect, 2, conn, slot, rack);

                        if (res != 0)
                        {
                            //Console.WriteLine("error" + rs2.Error(res));
                            LogSystem.Write(namePLC + " start", Direction.ERROR, "Error connection!. Error - " + rs2.Error(res), curLeft, i, true);

                        }
                        else
                        {
                            int resSAC = rs2.SetActiveConnection(Connect);
                        }

                    }       

                    int Byte_Col_r = 0;

                    resultReadField = rs2.field_read('M', 0, startBuffer, amount, out buffer, out Byte_Col_r);

                    if (resultReadField == 0)
                    {
                        //LogSystem.Write(namePLC + " start", Direction.Ok, "Соединение активно.", curLeft, (i+1), true);

                        //Буфер PLC
                        Thread PLS100ms = new Thread(BufferToBuffer);
                        PLS100ms.Start();

                        //Буфер SQL 100mc
                        Thread PLS101ms = new Thread(BufferSQLToBufferPLC);
                        PLS101ms.Start();

                        //Буфер сообщений

                        //Буфер 1с

                    }
                    else
                    {
                        rs2.UnloadConnection(Connect);
                        LogSystem.Write(namePLC + " 100ms", Direction.ERROR, "Error.Read fied PLC. " + rs2.Error(resultReadField), curLeft, i, true);
                    }
                             
                }

                    //if (i < y)
                    //{
                    //    i = i + 1;
                    //    Console.SetCursorPosition(curLeft, i);
                    //    Console.Write(namePLC + " 100ms - " + DateTime.Now.ToString("HH:mm:ss.fff"));

                    //}
                    //else
                    //{
                    //    i = curTop;
                    //    Console.SetCursorPosition(curLeft, i);
                    //    Console.Write(namePLC + " 100ms - " + DateTime.Now.ToString("HH:mm:ss.fff"));

                    //}
                
            }
            catch (Exception ex)
            {
                /*все исключения кидаем в пустоту*/
                LogSystem.Write(namePLC + " start-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, curLeft, curTop, true);
                
            }
        }


        #endregion

        #region Запись 101ms2 с контроллера в Базу данных
        private void SQL101ms2()
        {
            try
            {
                Thread.Sleep(101);

                string sqlExpression =
                                "INSERT INTO RS2stan100ms" +
                                "(v1,v2,v3,v4,v5,h1,h5,b,dvip,drazm,dmot,vvip,d1,d2,d3,d4,d5,e2,e3,e4,e5,n1l,n1p,n2l,n2p,n3l,n3p,n4l,n4p,n5l,n5p,reserv1,reserv2,t1,t2,t3,t4,t1l,t2l,t3l,t4l,t1p,t2p,t3p,t4p,t1z,t2z,t3z,t4z,erazm,ivozbrazm,izadrazm,w1,w2v,w2n,w3v,w3n,w4v,w4n,w5v,w5n,wmot,imot,izadmot,u1,u2v,u2n,u3v,u3n,u4v,u4n,u5v,u5n,umot,i1,i2v,i2n,i3v,i3n,i4v,i4n,i5v,i5n,rtv,dt1,dt2,dt3,dt4,grt,trt,mv1,mv2,mv3,dh1,dh5,os1klvb,rezerv,mezdoza4)" +
                                " VALUES " +
                                "(" +
                                 (float)(BitConverter.ToInt16(bufferSQL, 0)) / 100 + "," +    //v1
                                 (float)(BitConverter.ToInt16(bufferSQL, 2)) / 100 + "," +    //v2
                                 (float)(BitConverter.ToInt16(bufferSQL, 4)) / 100 + "," +    //v3
                                 (float)(BitConverter.ToInt16(bufferSQL, 6)) / 100 + "," +    //v4
                                 (float)(BitConverter.ToInt16(bufferSQL, 8)) / 100 + "," +    //v5
                                 (float)(BitConverter.ToInt16(bufferSQL, 10)) / 1000 + "," +  //h1    
                                 (float)(BitConverter.ToInt16(bufferSQL, 12)) / 1000 + "," +  //h5
                                 BitConverter.ToInt16(bufferSQL, 14) + "," +                  //b
                                 (float)(BitConverter.ToInt16(bufferSQL, 16)) / 1000 + "," +  //dvip
                                 (float)(BitConverter.ToInt16(bufferSQL, 18)) / 1000 + "," +  //drazm
                                 (float)(BitConverter.ToInt16(bufferSQL, 20)) / 1000 + "," +  //dmot
                                 (float)(BitConverter.ToInt16(bufferSQL, 22)) / 1000 + "," +  //vvip
                                 BitConverter.ToInt16(bufferSQL, 24) + "," +                  //d1
                                 BitConverter.ToInt16(bufferSQL, 26) + "," +                  //d2
                                 BitConverter.ToInt16(bufferSQL, 28) + "," +                  //d3
                                 BitConverter.ToInt16(bufferSQL, 30) + "," +                  //d4
                                 BitConverter.ToInt16(bufferSQL, 32) + "," +                  //d5
                                 (float)(BitConverter.ToInt16(bufferSQL, 34)) / 100 + "," +   //e2
                                 (float)(BitConverter.ToInt16(bufferSQL, 36)) / 100 + "," +   //e3
                                 (float)(BitConverter.ToInt16(bufferSQL, 38)) / 100 + "," +   //e4
                                 (float)(BitConverter.ToInt16(bufferSQL, 40)) / 100 + "," +   //e5
                                 (float)(BitConverter.ToInt16(bufferSQL, 42)) / 100 + "," +   //n1l
                                 (float)(BitConverter.ToInt16(bufferSQL, 44)) / 100 + "," +   //n1p
                                 (float)(BitConverter.ToInt16(bufferSQL, 46)) / 100 + "," +   //n2l
                                 (float)(BitConverter.ToInt16(bufferSQL, 48)) / 100 + "," +   //n2p
                                 (float)(BitConverter.ToInt16(bufferSQL, 50)) / 100 + "," +   //n3l
                                 (float)(BitConverter.ToInt16(bufferSQL, 52)) / 100 + "," +   //n3p
                                 (float)(BitConverter.ToInt16(bufferSQL, 54)) / 100 + "," +   //n4l
                                 (float)(BitConverter.ToInt16(bufferSQL, 56)) / 100 + "," +   //n4p
                                 (float)(BitConverter.ToInt16(bufferSQL, 58)) / 100 + "," +   //n5l
                                 (float)(BitConverter.ToInt16(bufferSQL, 60)) / 100 + "," +   //n5p
                                 (float)(BitConverter.ToInt16(bufferSQL, 68)) / 100 + "," +   //reserv1
                                 (float)(BitConverter.ToInt16(bufferSQL, 70)) / 100 + "," +   //reserv2
                                 (float)(BitConverter.ToInt16(bufferSQL, 72)) / 100 + "," +   //t1
                                 (float)(BitConverter.ToInt16(bufferSQL, 74)) / 100 + "," +   //t2
                                 (float)(BitConverter.ToInt16(bufferSQL, 76)) / 100 + "," +   //t3
                                 (float)(BitConverter.ToInt16(bufferSQL, 78)) / 100 + "," +   //t4
                                 (float)(BitConverter.ToInt16(bufferSQL, 80)) / 100 + "," +   //t1l
                                 (float)(BitConverter.ToInt16(bufferSQL, 82)) / 100 + "," +   //t2l
                                 (float)(BitConverter.ToInt16(bufferSQL, 84)) / 100 + "," +   //t3l
                                 (float)(BitConverter.ToInt16(bufferSQL, 86)) / 100 + "," +   //t4l
                                 (float)(BitConverter.ToInt16(bufferSQL, 88)) / 100 + "," +   //t1p
                                 (float)(BitConverter.ToInt16(bufferSQL, 90)) / 100 + "," +   //t2p
                                 (float)(BitConverter.ToInt16(bufferSQL, 92)) / 100 + "," +   //t3p
                                 (float)(BitConverter.ToInt16(bufferSQL, 94)) / 100 + "," +   //t4p
                                 (float)(BitConverter.ToInt16(bufferSQL, 96)) / 100 + "," +   //t1z
                                 (float)(BitConverter.ToInt16(bufferSQL, 98)) / 100 + "," +   //t2z
                                 (float)(BitConverter.ToInt16(bufferSQL, 100)) / 100 + "," +  //t3z
                                 (float)(BitConverter.ToInt16(bufferSQL, 112)) / 100 + "," +  //t4z
                                 (float)(BitConverter.ToInt16(bufferSQL, 114)) / 10 + "," +   //erazm
                                 (float)(BitConverter.ToInt16(bufferSQL, 116)) / 100 + "," +  //ivozbrazm
                                 (float)(BitConverter.ToInt16(bufferSQL, 118)) / 10 + "," +   //izadrazm 
                                 (float)(BitConverter.ToInt16(bufferSQL, 120)) / 10 + "," +   //w1
                                 (float)(BitConverter.ToInt16(bufferSQL, 122)) / 10 + "," +   //w2v
                                 (float)(BitConverter.ToInt16(bufferSQL, 124)) / 10 + "," +   //w2n
                                 (float)(BitConverter.ToInt16(bufferSQL, 126)) / 10 + "," +   //w3v
                                 (float)(BitConverter.ToInt16(bufferSQL, 128)) / 10 + "," +   //w3n
                                 (float)(BitConverter.ToInt16(bufferSQL, 130)) / 10 + "," +   //w4v
                                 (float)(BitConverter.ToInt16(bufferSQL, 132)) / 10 + "," +   //w4n
                                 (float)(BitConverter.ToInt16(bufferSQL, 134)) / 10 + "," +   //w5v
                                 (float)(BitConverter.ToInt16(bufferSQL, 136)) / 10 + "," +   //w5n
                                 (float)(BitConverter.ToInt16(bufferSQL, 138)) / 10 + "," +   //wmot
                                 BitConverter.ToInt16(bufferSQL, 140) + "," +                 //imot
                                 BitConverter.ToInt16(bufferSQL, 142) + "," +                 //izadmot
                                 (float)(BitConverter.ToInt16(bufferSQL, 144)) / 10 + "," +   //u1
                                 (float)(BitConverter.ToInt16(bufferSQL, 146)) / 10 + "," +   //u2v
                                 (float)(BitConverter.ToInt16(bufferSQL, 148)) / 10 + "," +   //u2n
                                 (float)(BitConverter.ToInt16(bufferSQL, 150)) / 10 + "," +   //u3v
                                 (float)(BitConverter.ToInt16(bufferSQL, 152)) / 10 + "," +   //u3n
                                 (float)(BitConverter.ToInt16(bufferSQL, 154)) / 10 + "," +   //u4v
                                 (float)(BitConverter.ToInt16(bufferSQL, 156)) / 10 + "," +   //u4n
                                 (float)(BitConverter.ToInt16(bufferSQL, 158)) / 10 + "," +   //u5v
                                 (float)(BitConverter.ToInt16(bufferSQL, 160)) / 10 + "," +   //u5n
                                 (float)(BitConverter.ToInt16(bufferSQL, 162)) / 10 + "," +   //umot
                                 BitConverter.ToInt16(bufferSQL, 164) + "," +                 //i1
                                 BitConverter.ToInt16(bufferSQL, 166) + "," +                 //i2v
                                 BitConverter.ToInt16(bufferSQL, 168) + "," +                 //i2n
                                 BitConverter.ToInt16(bufferSQL, 170) + "," +                 //i3v
                                 BitConverter.ToInt16(bufferSQL, 172) + "," +                 //i3n
                                 BitConverter.ToInt16(bufferSQL, 174) + "," +                 //i4v
                                 BitConverter.ToInt16(bufferSQL, 176) + "," +                 //i4n
                                 BitConverter.ToInt16(bufferSQL, 178) + "," +                 //i5v
                                 BitConverter.ToInt16(bufferSQL, 180) + "," +                 //i5n
                                 (float)(BitConverter.ToInt16(bufferSQL, 192)) / 10 + "," +   //rtv
                                 (float)(BitConverter.ToInt16(bufferSQL, 194)) / 10 + "," +   //dt1
                                 (float)(BitConverter.ToInt16(bufferSQL, 196)) / 10 + "," +   //dt2
                                 (float)(BitConverter.ToInt16(bufferSQL, 198)) / 10 + "," +   //dt3
                                 (float)(BitConverter.ToInt16(bufferSQL, 200)) / 10 + "," +   //dt4
                                 (float)(BitConverter.ToInt16(bufferSQL, 202)) / 10 + "," +   //grt
                                 (float)(BitConverter.ToInt16(bufferSQL, 204)) / 10 + "," +   //trt
                                 (float)(BitConverter.ToInt16(bufferSQL, 206)) / 10 + "," +   //mv1
                                 (float)(BitConverter.ToInt16(bufferSQL, 208)) / 10 + "," +   //mv2
                                 (float)(BitConverter.ToInt16(bufferSQL, 210)) / 10 + "," +   //mv3
                                 (float)(BitConverter.ToInt16(bufferSQL, 62)) / 10 + "," +    //dh1
                                 (float)(BitConverter.ToInt16(bufferSQL, 64)) / 10 + "," +    //dh5
                                 BitConverter.ToInt16(bufferSQL, 216) + "," +                 //os1klvb
                                 BitConverter.ToInt16(bufferSQL, 218) + "," +                 //rezerv
                                 BitConverter.ToInt16(bufferSQL, 220) +                       //mezdoza4
                                 ")";


                Console.WriteLine(sqlExpression);
            }
            catch (Exception ex)
            {
                LogSystem.Write(namePLC + " SQL(101ms)-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, curLeft, (curTop + 3), true);
            }

        }
        #endregion

        #region Запись данных(101ms) с контроллера в Базу Данных

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

        private void SQL101ms()
        {
            try
            {
                CreateTable();
                

                while (true)
                {
                    Thread.Sleep(101);

                    if (bufferSQL==null)
                    {
                        Console.WriteLine("0");
                    }
                    else
                    {
                        //Формируем строку для таблицы и ее записываем при условии что начата прокатка рулона

                        DataRow dr101ms = dt101ms.NewRow();
                        dr101ms["dtStan"] = DateTime.Now;



                        foreach (var item in dic101ms)
                        {
                            if (item.Value.floatdata) //Если данные имеют тип float
                            {
                                
                                float a = (float)(BitConverter.ToInt16(bufferSQL, item.Value.startbit)) / item.Value.coefficient;
                                //Console.WriteLine(a);
                                dr101ms[item.Key] = a;
                                string namepol = item.Key;

                                switch (item.Key)
                                {
                                    case "dmot":
                                        D_tek_mot = a;
                                        //Console.WriteLine(item.Key + " = " + D_tek_mot);
                                        break;
                                    case "h":
                                        hw = a;
                                        break;
                                    case "v4":
                                        speed4kl = a;
                                        break;
                                    case "b":
                                        Bw = a;
                                        break;

                                    default:
                                        break;
                                }

                            }
                            else
                            {
                                //Console.WriteLine(item.Key + " = " + item.Value.startbit + " - " + item.Value.coefficient);
                                int a = (BitConverter.ToInt16(bufferSQL, item.Value.startbit)) / item.Value.coefficient;
                                dr101ms[item.Key] = a;

                                string namepol = item.Key;

                                switch (item.Key)
                                {
                                    case "dmot":
                                        D_tek_mot = a;
                                        //Console.WriteLine(item.Key + " = " + item.Value.startbit + " - " + item.Value.coefficient);
                                        break;
                                    case "h":
                                        hw = a;
                                        break;
                                    case "v4":
                                        speed4kl = a;
                                        break;
                                    case "b":
                                        Bw = a;
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }

                        if (blRulonProkatSaveInData101ms)
                        {
                            //dt101ms.Rows.Add(dr101ms); //Добавляем троку в таблицу

                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "+", curLeft, (curTop+4), true);
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "+", curLeft, 4, true);
                            //Console.WriteLine(" Кол-во строк в таблице=" +  dt101ms.Rows.Count);
                            //Console.Write(".");
                            
                            //Console.WriteLine(namePLC +" "+ DateTime.Now.ToShortTimeString() + "  + " + D_tek_mot);

                        }
                        else
                        {
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "-", curLeft, (curTop+4), true);
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "-", curLeft, 4, true);
                            //Console.Write("_");
                            
                            //Console.WriteLine(namePLC +" " + DateTime.Now.ToShortTimeString() + "  - " + D_tek_mot);

                        }


                        if (D_tek_mot > D_pred_mot)
                        {
                            //if (D_tek_mot<0.615)
                            if (D_tek_mot < diametr_motalki)
                            {
                                TimeStart = DateTime.Now;
                                blRulonProkatSaveInData101ms = false; //включаем сбор данных по прокатке рулона
                                //Console.Write(D_tek_mot);
                            }
                            else
                            {
                                blRulonProkatSaveInData101ms = true;
                                //Console.Write(D_tek_mot);
                            }
                        }

                        if (namePLC== "Стан1700")
                        {
                            if ((TimeStart != new DateTime()) && (H_work == 0) && (D_tek_mot > 0.7) && (speed4kl > 2))
                            {
                                H_work = hw;
                                B_Work = Bw;
                            }

                            if ((TimeStart != new DateTime()) && (H_work != 0) && (D_tek_mot < 0.610) && (D_tek_mot < D_pred_mot))
                            {
                                #region  Формируем шифр таблицы (yyyyMMddсмена)

                                string numberTable;

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
                                //Console.WriteLine("");
                                Console.WriteLine("Время начала записи SQL=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                                Ves_Work = (((((D_pred_mot * D_pred_mot) - 0.36F) * 3.141593F) / 4) * (B_Work / 1000)) * 7.85F;
                                TimeStop = DateTime.Now;

                                Dlina_Work = ((Ves_Work / 7.85F) / (B_Work / 1000)) / (H_work / 1000);

                                //Ellipse101ms.Fill = onOK;

                                #region Формируем данные для передачи в Базу Данных

                                //yyyy - MM - dd HH: mm: ss.fff
                                string strTimeStart = TimeStart.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                string strTimeStop = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                TimeSpan tp = TimeStop.Subtract(TimeStart);
                                double dbltp = tp.TotalMilliseconds;
                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Время сбора информации (ms):" + dbltp.ToString());
                                Console.WriteLine("Кол-во строк полученных в системе:" + dt101ms.Rows.Count.ToString());
                                Console.WriteLine("Среднее время цикла обновления данных:" + (dbltp / (dt101ms.Rows.Count)).ToString());
                                Console.ResetColor();


                                string strNumberRulon = DateTime.Now.ToString("yyyyMMdd") + TimeStart.ToString("HHmm") + DateTime.Now.ToString("HHmm");
                                #endregion


                                //TODO после записи удаляем таблицу и заново создаем
                                //dt101ms.Clear();
                                //Console.WriteLine("Очистка таблицы");

                                //CreateTable();
                                //Console.WriteLine("Кол-во строк в таблице после очистки - " + dt101ms.Rows.Count.ToString());
                                //Console.WriteLine("");


                            }

                        }
                        else if (namePLC== "ДрессировочныйСтан1700")
                        {

                        }
                       
                        


                    }
                  D_pred_mot = D_tek_mot;
                }
            }
            catch (Exception ex)
            {

                LogSystem.Write(namePLC + " SQL(101ms)-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, curLeft, (curTop+3), true);
            }
            
            
        }
        #endregion

        #region Запись Сообщений(200mc) с контроллера в Базу Данных
        private void Message200ms()
        {
           
            while (true)
            {
                

                Thread.Sleep(200);
                //Console.WriteLine(namePLC + " 200ms - " + DateTime.Now.ToString("HH:mm:ss.fff"));

                

            }
        }
        #endregion

        #region Запись данных(1s) с контроллера в Базу Данных
        private void SQL1s()
        {
            
            while (true)
            {
                

                Thread.Sleep(1000);
                //Console.WriteLine(namePLC + " 1s - " + DateTime.Now.ToString("HH:mm:ss.fff"));

                
               
                
            }
        }
        #endregion

        #region одключение к БД
        private void SQLconDB()
        {
            try
            {
                string strConnect = "Data Source = 192.168.0.46; Initial Catalog = rs2; User ID = rs2admin; Password = 159951";

                connectSQL = new SqlConnection(strConnect);

                connectSQL.Open();
            }
            catch (Exception ex)
            {
                LogSystem.Write(namePLC + " SQL connect -" + ex.Source, Direction.ERROR, "SQLconnection Error-" + ex.Message, curLeft, (curTop + 3), true);
            }
        }
        #endregion

    }
}
