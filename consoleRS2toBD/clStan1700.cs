using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        //DataTable dtstan101ms;

        bool blstanRulonProkatSaveInData101ms;
        DateTime stanTimeStart;
        private string connectionString = "Data Source = 192.168.0.46; Initial Catalog = rs2; User ID = rs2admin; Password = 159951";



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
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff")+ " ("+stanNamePLC + ")  +");
            }
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

        private void stanSQL101ms()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(101);

                    #region Формируем SQL запрос с циклом 101мс и записываем его во временную БД

                    
                    string comRulon101ms = "INSERT INTO RS2stan100ms" +
                   "(v1,v2,v3,v4,v5,h1,h5,b,dvip,drazm,dmot,vvip,d1,d2,d3,d4,d5,e2,e3,e4,e5,n1l,n1p,n2l,n2p,n3l,n3p,n4l,n4p,n5l,n5p,reserv1,reserv2,t1,t2,t3,t4,t1l,t2l,t3l,t4l,t1p,t2p,t3p,t4p,t1z,t2z,t3z,t4z,erazm,ivozbrazm,izadrazm,w1,w2v,w2n,w3v,w3n,w4v,w4n,w5v,w5n,wmot,imot,izadmot,u1,u2v,u2n,u3v,u3n,u4v,u4n,u5v,u5n,umot,i1,i2v,i2n,i3v,i3n,i4v,i4n,i5v,i5n,rtv,dt1,dt2,dt3,dt4,grt,trt,mv1,mv2,mv3,dh1,dh5,os1klvb,rezerv,mezdoza4)" +
                   " VALUES " +
                   "(" +
                    (float)(BitConverter.ToInt16(stanbufferSQL, 0)) / 100 + "," +    //v1
                    (float)(BitConverter.ToInt16(stanbufferSQL, 2)) / 100 + "," +    //v2
                    (float)(BitConverter.ToInt16(stanbufferSQL, 4)) / 100 + "," +    //v3
                    (float)(BitConverter.ToInt16(stanbufferSQL, 6)) / 100 + "," +    //v4
                    (float)(BitConverter.ToInt16(stanbufferSQL, 8)) / 100 + "," +    //v5
                    (float)(BitConverter.ToInt16(stanbufferSQL, 10)) / 1000 + "," +  //h1    
                    (float)(BitConverter.ToInt16(stanbufferSQL, 12)) / 1000 + "," +  //h5
                    BitConverter.ToInt16(stanbufferSQL, 14) + "," +                  //b
                    (float)(BitConverter.ToInt16(stanbufferSQL, 16)) / 1000 + "," +  //dvip
                    (float)(BitConverter.ToInt16(stanbufferSQL, 18)) / 1000 + "," +  //drazm
                    (float)(BitConverter.ToInt16(stanbufferSQL, 20)) / 1000 + "," +  //dmot
                    (float)(BitConverter.ToInt16(stanbufferSQL, 22)) / 1000 + "," +  //vvip
                    BitConverter.ToInt16(stanbufferSQL, 24) + "," +                  //d1
                    BitConverter.ToInt16(stanbufferSQL, 26) + "," +                  //d2
                    BitConverter.ToInt16(stanbufferSQL, 28) + "," +                  //d3
                    BitConverter.ToInt16(stanbufferSQL, 30) + "," +                  //d4
                    BitConverter.ToInt16(stanbufferSQL, 32) + "," +                  //d5
                    (float)(BitConverter.ToInt16(stanbufferSQL, 34)) / 100 + "," +   //e2
                    (float)(BitConverter.ToInt16(stanbufferSQL, 36)) / 100 + "," +   //e3
                    (float)(BitConverter.ToInt16(stanbufferSQL, 38)) / 100 + "," +   //e4
                    (float)(BitConverter.ToInt16(stanbufferSQL, 40)) / 100 + "," +   //e5
                    (float)(BitConverter.ToInt16(stanbufferSQL, 42)) / 100 + "," +   //n1l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 44)) / 100 + "," +   //n1p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 46)) / 100 + "," +   //n2l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 48)) / 100 + "," +   //n2p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 50)) / 100 + "," +   //n3l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 52)) / 100 + "," +   //n3p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 54)) / 100 + "," +   //n4l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 56)) / 100 + "," +   //n4p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 58)) / 100 + "," +   //n5l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 60)) / 100 + "," +   //n5p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 68)) / 100 + "," +   //reserv1
                    (float)(BitConverter.ToInt16(stanbufferSQL, 70)) / 100 + "," +   //reserv2
                    (float)(BitConverter.ToInt16(stanbufferSQL, 72)) / 100 + "," +   //t1
                    (float)(BitConverter.ToInt16(stanbufferSQL, 74)) / 100 + "," +   //t2
                    (float)(BitConverter.ToInt16(stanbufferSQL, 76)) / 100 + "," +   //t3
                    (float)(BitConverter.ToInt16(stanbufferSQL, 78)) / 100 + "," +   //t4
                    (float)(BitConverter.ToInt16(stanbufferSQL, 80)) / 100 + "," +   //t1l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 82)) / 100 + "," +   //t2l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 84)) / 100 + "," +   //t3l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 86)) / 100 + "," +   //t4l
                    (float)(BitConverter.ToInt16(stanbufferSQL, 88)) / 100 + "," +   //t1p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 90)) / 100 + "," +   //t2p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 92)) / 100 + "," +   //t3p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 94)) / 100 + "," +   //t4p
                    (float)(BitConverter.ToInt16(stanbufferSQL, 96)) / 100 + "," +   //t1z
                    (float)(BitConverter.ToInt16(stanbufferSQL, 98)) / 100 + "," +   //t2z
                    (float)(BitConverter.ToInt16(stanbufferSQL, 100)) / 100 + "," +  //t3z
                    (float)(BitConverter.ToInt16(stanbufferSQL, 112)) / 100 + "," +  //t4z
                    (float)(BitConverter.ToInt16(stanbufferSQL, 114)) / 10 + "," +   //erazm
                    (float)(BitConverter.ToInt16(stanbufferSQL, 116)) / 100 + "," +  //ivozbrazm
                    (float)(BitConverter.ToInt16(stanbufferSQL, 118)) / 10 + "," +   //izadrazm 
                    (float)(BitConverter.ToInt16(stanbufferSQL, 120)) / 10 + "," +   //w1
                    (float)(BitConverter.ToInt16(stanbufferSQL, 122)) / 10 + "," +   //w2v
                    (float)(BitConverter.ToInt16(stanbufferSQL, 124)) / 10 + "," +   //w2n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 126)) / 10 + "," +   //w3v
                    (float)(BitConverter.ToInt16(stanbufferSQL, 128)) / 10 + "," +   //w3n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 130)) / 10 + "," +   //w4v
                    (float)(BitConverter.ToInt16(stanbufferSQL, 132)) / 10 + "," +   //w4n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 134)) / 10 + "," +   //w5v
                    (float)(BitConverter.ToInt16(stanbufferSQL, 136)) / 10 + "," +   //w5n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 138)) / 10 + "," +   //wmot
                    BitConverter.ToInt16(stanbufferSQL, 140) + "," +                 //imot
                    BitConverter.ToInt16(stanbufferSQL, 142) + "," +                 //izadmot
                    (float)(BitConverter.ToInt16(stanbufferSQL, 144)) / 10 + "," +   //u1
                    (float)(BitConverter.ToInt16(stanbufferSQL, 146)) / 10 + "," +   //u2v
                    (float)(BitConverter.ToInt16(stanbufferSQL, 148)) / 10 + "," +   //u2n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 150)) / 10 + "," +   //u3v
                    (float)(BitConverter.ToInt16(stanbufferSQL, 152)) / 10 + "," +   //u3n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 154)) / 10 + "," +   //u4v
                    (float)(BitConverter.ToInt16(stanbufferSQL, 156)) / 10 + "," +   //u4n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 158)) / 10 + "," +   //u5v
                    (float)(BitConverter.ToInt16(stanbufferSQL, 160)) / 10 + "," +   //u5n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 162)) / 10 + "," +   //umot
                    BitConverter.ToInt16(stanbufferSQL, 164) + "," +                 //i1
                    BitConverter.ToInt16(stanbufferSQL, 166) + "," +                 //i2v
                    BitConverter.ToInt16(stanbufferSQL, 168) + "," +                 //i2n
                    BitConverter.ToInt16(stanbufferSQL, 170) + "," +                 //i3v
                    BitConverter.ToInt16(stanbufferSQL, 172) + "," +                 //i3n
                    BitConverter.ToInt16(stanbufferSQL, 174) + "," +                 //i4v
                    BitConverter.ToInt16(stanbufferSQL, 176) + "," +                 //i4n
                    BitConverter.ToInt16(stanbufferSQL, 178) + "," +                 //i5v
                    BitConverter.ToInt16(stanbufferSQL, 180) + "," +                 //i5n
                    (float)(BitConverter.ToInt16(stanbufferSQL, 192)) / 10 + "," +   //rtv
                    (float)(BitConverter.ToInt16(stanbufferSQL, 194)) / 10 + "," +   //dt1
                    (float)(BitConverter.ToInt16(stanbufferSQL, 196)) / 10 + "," +   //dt2
                    (float)(BitConverter.ToInt16(stanbufferSQL, 198)) / 10 + "," +   //dt3
                    (float)(BitConverter.ToInt16(stanbufferSQL, 200)) / 10 + "," +   //dt4
                    (float)(BitConverter.ToInt16(stanbufferSQL, 202)) / 10 + "," +   //grt
                    (float)(BitConverter.ToInt16(stanbufferSQL, 204)) / 10 + "," +   //trt
                    (float)(BitConverter.ToInt16(stanbufferSQL, 206)) / 10 + "," +   //mv1
                    (float)(BitConverter.ToInt16(stanbufferSQL, 208)) / 10 + "," +   //mv2
                    (float)(BitConverter.ToInt16(stanbufferSQL, 210)) / 10 + "," +   //mv3
                    (float)(BitConverter.ToInt16(stanbufferSQL, 62)) / 10 + "," +    //dh1
                    (float)(BitConverter.ToInt16(stanbufferSQL, 64)) / 10 + "," +    //dh5
                    BitConverter.ToInt16(stanbufferSQL, 216) + "," +                 //os1klvb
                    BitConverter.ToInt16(stanbufferSQL, 218) + "," +                 //rezerv
                    BitConverter.ToInt16(stanbufferSQL, 220) +                       //mezdoza4
                    ")";



                    if (true) //TODO Если установлен bit что прокатка рулона(1s) то тогда пишем во временную таблицу
                    {
                        using (SqlConnection conSQL101ms = new SqlConnection(connectionString))
                        {
                            conSQL101ms.Open();
                            SqlCommand command = new SqlCommand(comRulon101ms, conSQL101ms);
                            command.ExecuteNonQuery();
                            
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {

                LogSystem.Write(stanNamePLC + " SQL(101ms)-" + ex.Source, Direction.ERROR, "Start Error-" + ex.Message, 0, 3, true);
            }


        }
        #endregion

        #region записываем данные 1с

        #endregion
        private void stanSQL1s()
        {

        }
    }
}
