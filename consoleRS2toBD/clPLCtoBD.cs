using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HWDiag;
using LoggerInSystem;

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
      //  byte[] bufferMessage;   //Данные сообщений
      //  byte[] bufferMessageOld;//Данные сообщений
      //  byte[] buffer1s;        //Технологические данные
        byte[] bufferNet;       //Передача по сети (визуализация)

        readonly object locker = new object();
        readonly object locker2 = new object();

        float speed4kl, H_work, hw, Bw, D_tek_mot, B_Work, D_pred_mot = 0, Ves_Work, Dlina_Work;

        DataTable dt101ms;

        bool blRulonProkatSaveInData101ms;
        DateTime TimeStart;
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
            lock (locker)
            {

                //Array.Clear(bufferPLC, 0, bufferPLC.Length);
                bufferPLC = buffer;
            }

        }


        void BufferSQLToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                //Array.Clear(bufferSQL, 0, bufferSQL.Length);
                bufferSQL = bufferPLC;
            }

        }

        #endregion

       
        public void Start()
        {
            
            Thread queryPLC = new Thread(PLC);
            queryPLC.Start();

            Thread querySQL = new Thread(SQL101ms);
            querySQL.Start();

            Thread queryMes = new Thread(Message200ms);
            queryMes.Start();

            Thread query1s = new Thread(SQL1s);
            query1s.Start();

            while (true)
            {
                Thread.Sleep(5000); //??????????????????????????????????????????????????????????????????????????????????????
                
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
                                        Console.WriteLine(item.Key + " = " + D_tek_mot);
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
                            dt101ms.Rows.Add(dr101ms); //Добавляем троку в таблицу
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "+", curLeft, (curTop+4), true);
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "+", curLeft, 4, true);
                            //Console.WriteLine(" Кол-во строк в таблице=" +  dt101ms.Rows.Count);
                            //Console.Write(".");
                            
                            Console.WriteLine(namePLC +" "+ DateTime.Now.ToShortTimeString() + "  + " + D_tek_mot);

                        }
                        else
                        {
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "-", curLeft, (curTop+4), true);
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "-", curLeft, 4, true);
                            //Console.Write("_");
                            
                            Console.WriteLine(namePLC +" " + DateTime.Now.ToShortTimeString() + "  - " + D_tek_mot);

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


    }
}
