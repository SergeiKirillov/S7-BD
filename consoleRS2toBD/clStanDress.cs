﻿using System;
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
    class clStanDress
    {
        Dictionary<string, ContData> stanData100ms = new Dictionary<string, ContData>
        {
            {"VKlet", new ContData(0,100,true)},
            {"IzadR", new ContData(2,1,true)},
            {"IzadM", new ContData(4,1,true)},
            {"NKlet", new ContData(6,10,true)},
            {"NRazm", new ContData(8,10,true)},
            {"NMot", new ContData(10,10,true)},
            {"TRazm", new ContData(12,10,true)},
            {"TMot", new ContData(14,10,true)},
            {"RRazm", new ContData(16,1,true)},
            {"dmot", new ContData(18,1,false)},          //RMot
            {"NVlev", new ContData(20,100,true)},
            {"NVpr", new ContData(22,100,true)},
            {"IvozM", new ContData(48,100,true)},
            {"Imot", new ContData(50,1,true)},
            {"Urazm", new ContData(52,10,true)},
            {"IvozR", new ContData(54,100,true)},
            {"Umot", new ContData(56,10,true)},

            {"b", new ContData(124,1,true)},
            {"h", new ContData(126,100,true)},

            {"IRUZ4", new ContData(128,1,true)},
            {"IRUZ5", new ContData(130,1,true)},
            {"IMUZ4", new ContData(132,1,true)},
            {"IMUZ5", new ContData(134,1,true)},
            {"IzovK", new ContData(136,100,true)},
            {"Ukl", new ContData(138,10,true)},
            {"IKUZ4", new ContData(140,1,true)},
            {"IKUZ5", new ContData(142,1,true)},
            {"ObgTek", new ContData(144,100,true)},
            {"DatObgDo", new ContData(146,1,true)},
            {"DatObgZa", new ContData(148,1,true)},

        };

        byte[] buffer;          //данные c контроллера 100ms
        byte[] bufferPLC;       //Промежуточное хранение даных
        byte[] bufferSQL;       //Данные 101мс
        byte[] bufferMessage;   //Данные сообщений
        byte[] bufferMessageOld;//Данные сообщений
        byte[] buffer1s;        //Технологические данные
        byte[] bufferNet;       //Передача по сети (визуализация)

        int amount = 150; //Размер буфера для принятия данных в байтах

        byte[] IPconnPLC = new byte[] { 192, 168, 0, 21 }; //Передаем адресс контроллера
        int connect = 1;

        double dMot = 301;

        string NamePLC = "ДрессировочныйСтан1700";
        int SlotconnPC = 2;
        int RackconnPC = 0;

        int StartAdressTag = 2000; //старт адресов с 3000

        readonly object locker1 = new object();
        readonly object locker2 = new object();

        float speed4kl, H_work, hw, Bw, D_tek_mot, B_Work, D_pred_mot = 0, Ves_Work, Dlina_Work;

        DataTable dt101ms;

        bool blRulonProkatSaveInData101ms;
        DateTime TimeStart;

        public void goStart()
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

        private void SQL1s()
        {
           
        }

        private void Message200ms()
        {
         
        }

        

        #region Соединение и прием данных с контроллера
        private void PLC()
        {
            try
            {
                int i = 100;     //начальная позиция по Top
                int y = 2;     //Конечная позиция по Top

                Prodave rs2 = new Prodave();

                buffer = new byte[amount];
                bufferPLC = new byte[amount];
                bufferSQL = new byte[amount];

                int resultReadField = 5;


                while (true)
                {
                    Thread.Sleep(100);

                    if (resultReadField != 0)
                    {
                        int res = rs2.LoadConnection(connect, 2, IPconnPLC, SlotconnPC, RackconnPC);

                        if (res != 0)
                        {
                            //Console.WriteLine("error" + rs2.Error(res));
                            LogSystem.Write(NamePLC + " start", Direction.ERROR, "Error connection!. Error - " + rs2.Error(res), 100, 0, true);

                        }
                        else
                        {
                            int resSAC = rs2.SetActiveConnection(connect);
                        }

                    }

                    int Byte_Col_r = 0;

                    resultReadField = rs2.field_read('M', 0, StartAdressTag, amount, out buffer, out Byte_Col_r);

                    if (resultReadField == 0)
                    {
                        //LogSystem.Write(NamePLC + " start", Direction.Ok, "Соединение активно.", 100, 1, true);

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
                        rs2.UnloadConnection(connect);
                        LogSystem.Write(NamePLC + " 100ms", Direction.ERROR, "Error.Read fied PLC. " + rs2.Error(resultReadField), 100, 0, true);
                    }

                }


            }
            catch (Exception ex)
            {
                /*все исключения кидаем в пустоту*/
                LogSystem.Write(NamePLC + " start-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 100, 0, true);

            }
        }


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

        #region Запись данных(101ms) с контроллера в Базу Данных

        private void CreateTable()
        {
            #region Формируем таблицу для формирования данных и последующего сохранения в БД
            dt101ms = new DataTable();
            dt101ms.Reset();
            dt101ms.Columns.Add("dtStan", typeof(DateTime));//Для хранения даты и времени
            foreach (var item in stanData100ms)
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

                    if (bufferSQL == null)
                    {
                        Console.WriteLine("0");
                    }
                    else
                    {
                        //Формируем строку для таблицы и ее записываем при условии что начата прокатка рулона

                        DataRow dr101ms = dt101ms.NewRow();
                        dr101ms["dtStan"] = DateTime.Now;



                        foreach (var item in stanData100ms)
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
                            dt101ms.Rows.Add(dr101ms); //Добавляем троку в таблицу
                                                       //LogSystem.Write(namePLC + " SQL", Direction.Ok, "+", curLeft, (curTop+4), true);
                                                       //LogSystem.Write(namePLC + " SQL", Direction.Ok, "+", curLeft, 4, true);
                                                       //Console.WriteLine(" Кол-во строк в таблице=" +  dt101ms.Rows.Count);
                                                       //Console.Write(".");

                            Console.WriteLine(NamePLC + " " + DateTime.Now.ToString("HH:mm:ss.fff") + "  + " + D_tek_mot);

                        }
                        else
                        {
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "-", curLeft, (curTop+4), true);
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "-", curLeft, 4, true);
                            //Console.Write("_");

                            Console.WriteLine(NamePLC + " " + DateTime.Now.ToString("HH:mm:ss.fff") + "  - " + D_tek_mot);

                        }


                        if (D_tek_mot > D_pred_mot)
                        {
                            //if (D_tek_mot<0.615)
                            if (D_tek_mot < dMot)
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

                LogSystem.Write(NamePLC + " SQL(101ms)-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 0, 3, true);
            }


        }
        #endregion
    }
}
