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
    class clStan1700
    {
        Dictionary<string, ContData> stanData100ms = new Dictionary<string, ContData>
        {
            {"v1", new ContData(0,100,true)},
            {"v2", new ContData(2,100,true)},
            {"v3", new ContData(4,100,true)},
            {"v4", new ContData(6,100,true)},
            {"v5", new ContData(8,100,true)},
            {"h1", new ContData(10,1000,true)},
            {"h", new ContData(12,1000,true)},

            {"b", new ContData(14,1,false)},

            {"dvip", new ContData(16,1000,true)},
            {"drazm", new ContData(18,1000,true)},
            {"dmot", new ContData(20,1000,true)},
            {"vvip", new ContData(22,1000,true)},

            {"d1", new ContData(24,1,false)},
            {"d2", new ContData(26,1,false)},
            {"d3", new ContData(28,1,false)},
            {"d4", new ContData(30,1,false)},
            {"d5", new ContData(32,1,false)},

            {"e2", new ContData(34,100,true)},
            {"e3", new ContData(36,100,true)},
            {"e4", new ContData(38,100,true)},
            {"e5", new ContData(40,100,true)},

            {"n1l", new ContData(42,100,true)},
            {"n1p", new ContData(44,100,true)},
            {"n2l", new ContData(46,100,true)},
            {"n2p", new ContData(48,100,true)},
            {"n3l", new ContData(50,100,true)},
            {"n3p", new ContData(52,100,true)},
            {"n4l", new ContData(54,100,true)},
            {"n4p", new ContData(56,100,true)},
            {"n5l", new ContData(58,100,true)},
            {"n5p", new ContData(60,100,true)},

            {"reserv1", new ContData(68,100,true)},
            {"reserv2", new ContData(70,100,true)},

            {"t1", new ContData(72,100,true)},
            {"t2", new ContData(74,100,true)},
            {"t3", new ContData(76,100,true)},
            {"t4", new ContData(78,100,true)},

            {"t1l", new ContData(80,100,true)},
            {"t2l", new ContData(82,100,true)},
            {"t3l", new ContData(84,100,true)},
            {"t4l", new ContData(86,100,true)},

            {"t1p", new ContData(88,100,true)},
            {"t2p", new ContData(90,100,true)},
            {"t3p", new ContData(92,100,true)},
            {"t4p", new ContData(94,100,true)},

            {"t1z", new ContData(96,100,true)},
            {"t2z", new ContData(98,100,true)},
            {"t3z", new ContData(100,100,true)},
            {"t4z", new ContData(112,100,true)},

            {"erazm", new ContData(114,10,true)},
            {"ivozbrazm", new ContData(116,100,true)},
            {"izadrazm", new ContData(118,10,true)},

            {"w1", new ContData(120,10,true)},
            {"w2v", new ContData(122,10,true)},
            {"w2n", new ContData(124,10,true)},
            {"w3v", new ContData(126,10,true)},
            {"w3n", new ContData(128,10,true)},
            {"w4v", new ContData(130,10,true)},
            {"w4n", new ContData(132,10,true)},
            {"w5v", new ContData(134,10,true)},
            {"w5n", new ContData(136,10,true)},
            {"wmot", new ContData(138,10,true)},

            {"imot", new ContData(140,1,false)},
            {"izadmot", new ContData(142,1,false)},

            {"u1", new ContData(144,10,true)},
            {"u2v", new ContData(146,10,true)},
            {"u2n", new ContData(148,10,true)},
            {"u3v", new ContData(150,10,true)},
            {"u3n", new ContData(152,10,true)},
            {"u4v", new ContData(154,10,true)},
            {"u4n", new ContData(156,10,true)},
            {"u5v", new ContData(158,10,true)},
            {"u5n", new ContData(160,10,true)},
            {"umot", new ContData(162,10,true)},


            {"i1", new ContData(164,1,false)},
            {"i2v", new ContData(166,1,false)},
            {"i2n", new ContData(168,1,false)},
            {"i3v", new ContData(170,1,false)},
            {"i3n", new ContData(172,1,false)},
            {"i4v", new ContData(174,1,false)},
            {"i4n", new ContData(176,1,false)},
            {"i5v", new ContData(178,1,false)},
            {"i5n", new ContData(180,1,false)},


            {"rtv", new ContData(192,10,true)},
            {"dt1", new ContData(194,10,true)},
            {"dt2", new ContData(196,10,true)},
            {"dt3", new ContData(198,10,true)},
            {"dt4", new ContData(200,10,true)},
            {"grt", new ContData(202,10,true)},
            {"trt", new ContData(204,10,true)},
            {"mv1", new ContData(206,10,true)},
            {"mv2", new ContData(208,10,true)},
            {"mv3", new ContData(210,10,true)},
            {"dh1", new ContData(62,10,true)},
            {"dh5", new ContData(64,10,true)},

            {"os1klvb", new ContData(216,1,false)},
            {"rezerv", new ContData(218,1,false)},
            {"mezdoza4", new ContData(220,1,false)},
        };

        byte[] stanbuffer;          //данные c контроллера 100ms
        byte[] stanbufferPLC;       //Промежуточное хранение даных
        byte[] stanbufferSQL;       //Данные 101мс
        byte[] stanbufferMessage;   //Данные сообщений
        byte[] stanbufferMessageOld;//Данные сообщений
        byte[] stanbuffer1s;        //Технологические данные
        byte[] stanbufferNet;       //Передача по сети (визуализация)

        int stanamount = 315; //Размер буфера для принятия данных в байтах

        byte[] stanIPconnPLC = new byte[] { 192, 168, 0, 11 }; //Передаем адресс контроллера
        int stanconnect = 0;

        double standMot = 0.615;

        string stanNamePLC = "Стан1700";
        int stanSlotconnPC = 3;
        int stanRackconnPC = 0;

        int stanStartAdressTag = 3000; //старт адресов с 3000

        readonly object stanlocker1 = new object();
        readonly object stanlocker2 = new object();

        float stanspeed4kl, stanH_work, stanhw, stanBw, stanD_tek_mot, stanB_Work, stanD_pred_mot = 0, stanVes_Work, stanDlina_Work;

        DataTable dtstan101ms;

        bool blstanRulonProkatSaveInData101ms;
        DateTime stanTimeStart;

        public void goStart()
        {
            
            //stan.Data101ms = stanData100ms;
            
            Thread queryPLC = new Thread(stanPLC);
            queryPLC.Start();

            Thread querySQL = new Thread(stanSQL101ms);
            querySQL.Start();

            Thread queryMes = new Thread(stanMessage200ms);
            queryMes.Start();

            Thread query1s = new Thread(stanSQL1s);
            query1s.Start();

            while (true)
            {
                Thread.Sleep(5000); //??????????????????????????????????????????????????????????????????????????????????????
            }
        }

        private void stanSQL1s()
        {
            
        }

        private void stanMessage200ms()
        {
            
        }

       
        
        #region Соединение и прием данных с контроллера
        private void stanPLC()
        {
            try
            {
                int i = 0;     //начальная позиция по Top
                int y = 2;     //Конечная позиция по Top

                Prodave rs2 = new Prodave();

                stanbuffer = new byte[stanamount];
                stanbufferPLC = new byte[stanamount];
                stanbufferSQL = new byte[stanamount];

                int resultReadField = 5;


                while (true)
                {
                    Thread.Sleep(100);

                    if (resultReadField != 0)
                    {
                        int res = rs2.LoadConnection(stanconnect, 2, stanIPconnPLC, stanSlotconnPC, stanRackconnPC);

                        if (res != 0)
                        {
                            //Console.WriteLine("error" + rs2.Error(res));
                            LogSystem.Write(stanNamePLC + " start", Direction.ERROR, "Error connection!. Error - " + rs2.Error(res), 0, 0, true);

                        }
                        else
                        {
                            int resSAC = rs2.SetActiveConnection(stanconnect);
                        }

                    }

                    int Byte_Col_r = 0;

                    resultReadField = rs2.field_read('M', 0, stanStartAdressTag, stanamount, out stanbuffer, out Byte_Col_r);

                    if (resultReadField == 0)
                    {
                        //LogSystem.Write(NamePLC + " start", Direction.Ok, "Соединение активно.", 0, 1, true);

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
                        rs2.UnloadConnection(stanconnect);
                        LogSystem.Write(stanNamePLC + " 100ms", Direction.ERROR, "Error.Read fied PLC. " + rs2.Error(resultReadField), 0, 0, true);
                    }

                }


            }
            catch (Exception ex)
            {
                /*все исключения кидаем в пустоту*/
                LogSystem.Write(stanNamePLC + " start -" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 0, 0, true);

            }
        }



        void BufferToBuffer()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (stanlocker1)
            {

                //Array.Clear(bufferPLC, 0, bufferPLC.Length);
                stanbufferPLC = stanbuffer;
            }

        }


        void BufferSQLToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (stanlocker2)
            {
                //Array.Clear(bufferSQL, 0, bufferSQL.Length);
                stanbufferSQL = stanbufferPLC;
            }

        }

        #endregion


        #region Запись данных(101ms) с контроллера в Базу Данных

        private void CreateTable()
        {
            #region Формируем таблицу для формирования данных и последующего сохранения в БД
            dtstan101ms = new DataTable();
            dtstan101ms.Reset();
            dtstan101ms.Columns.Add("dtStan", typeof(DateTime));//Для хранения даты и времени
            foreach (var item in stanData100ms)
            {
                dtstan101ms.Columns.Add(item.Key, item.Value.floatdata ? typeof(float) : typeof(int));
            }
            #endregion
        }

        private void stanSQL101ms()
        {
            try
            {
                CreateTable();


                while (true)
                {
                    Thread.Sleep(101);

                    if (stanbufferSQL == null)
                    {
                        Console.WriteLine("0");
                    }
                    else
                    {
                        //Формируем строку для таблицы и ее записываем при условии что начата прокатка рулона

                        DataRow dr101ms = dtstan101ms.NewRow();
                        dr101ms["dtStan"] = DateTime.Now;



                        foreach (var item in stanData100ms)
                        {
                            if (item.Value.floatdata) //Если данные имеют тип float
                            {

                                float a = (float)(BitConverter.ToInt16(stanbufferSQL, item.Value.startbit)) / item.Value.coefficient;
                                //Console.WriteLine(a);
                                dr101ms[item.Key] = a;
                                string namepol = item.Key;

                                switch (item.Key)
                                {
                                    case "dmot":
                                        stanD_tek_mot = a;
                                        //Console.WriteLine(item.Key + " = " + D_tek_mot);
                                        break;
                                    case "h":
                                        stanhw = a;
                                        break;
                                    case "v4":
                                        stanspeed4kl = a;
                                        break;
                                    case "b":
                                        stanBw = a;
                                        break;

                                    default:
                                        break;
                                }

                            }
                            else
                            {
                                //Console.WriteLine(item.Key + " = " + item.Value.startbit + " - " + item.Value.coefficient);
                                int a = (BitConverter.ToInt16(stanbufferSQL, item.Value.startbit)) / item.Value.coefficient;
                                dr101ms[item.Key] = a;

                                string namepol = item.Key;

                                switch (item.Key)
                                {
                                    case "dmot":
                                        stanD_tek_mot = a;
                                        //Console.WriteLine(item.Key + " = " + item.Value.startbit + " - " + item.Value.coefficient);
                                        break;
                                    case "h":
                                        stanhw = a;
                                        break;
                                    case "v4":
                                        stanspeed4kl = a;
                                        break;
                                    case "b":
                                        stanBw = a;
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }

                        if (blstanRulonProkatSaveInData101ms)
                        {
                            dtstan101ms.Rows.Add(dr101ms); //Добавляем троку в таблицу
                                                       //LogSystem.Write(namePLC + " SQL", Direction.Ok, "+", curLeft, (curTop+4), true);
                                                       //LogSystem.Write(namePLC + " SQL", Direction.Ok, "+", curLeft, 4, true);
                                                       //Console.WriteLine(" Кол-во строк в таблице=" +  dt101ms.Rows.Count);
                                                       //Console.Write(".");

                            Console.WriteLine(stanNamePLC + " " + DateTime.Now.ToString("HH:mm:ss.fff") + "  + " + stanD_tek_mot);
                            

                        }
                        else
                        {
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "-", curLeft, (curTop+4), true);
                            //LogSystem.Write(namePLC + " SQL", Direction.Ok, "-", curLeft, 4, true);
                            //Console.Write("_");

                            Console.WriteLine(stanNamePLC + " " + DateTime.Now.ToString("HH:mm:ss.fff") + "  - " + stanD_tek_mot);

                        }


                        if (stanD_tek_mot > stanD_pred_mot)
                        {
                            //if (D_tek_mot<0.615)
                            if (stanD_tek_mot < standMot)
                            {
                                stanTimeStart = DateTime.Now;
                                blstanRulonProkatSaveInData101ms = false; //включаем сбор данных по прокатке рулона
                                //Console.Write(D_tek_mot);
                            }
                            else
                            {
                                blstanRulonProkatSaveInData101ms = true;
                                //Console.Write(D_tek_mot);
                            }
                        }

                    }






                    stanD_pred_mot = stanD_tek_mot;
                }
            }
            catch (Exception ex)
            {

                LogSystem.Write(stanNamePLC + " SQL(101ms)-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 0, 3, true);
            }


        }
        #endregion
    }
}
