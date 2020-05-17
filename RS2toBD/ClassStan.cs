using HWDiag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Diagnostics;
using LoggerInSystem;


namespace RS2toBD
{
    
    class ClassStan
    {
        bool blstan101ms; //данные по прокатке рулона, формируются таблица после прокатанного рулона. 
        bool blstan1s;    //данные по работе стана, формируются и через 1мин (~62 сообщения) скидываются в БД.
        bool blstan200ms; //сообщения формируются в течении 60 секунд и после этого записываются в БД.

        bool blNetSend; //Передача данных по сети для создания визуализации на удаленном компьютере

        PLCto stan;

        Dictionary<string, ContData> stanData100ms = new Dictionary<string, ContData>
        {
            {"v1", new ContData(0,100,true)},
            {"v2", new ContData(2,100,true)},
            {"v3", new ContData(4,100,true)},
            {"v4", new ContData(6,100,true)},
            {"v5", new ContData(8,100,true)},
            {"h1", new ContData(10,1000,true)},
            {"h5", new ContData(12,1000,true)},

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

                    
    



        public ClassStan(bool stan101ms, bool stan1s, bool stan200ms, bool NetSend)
        {
            this.blstan101ms = stan101ms;
            this.blstan1s = stan1s;
            this.blstan200ms = stan200ms;

            this.blNetSend = NetSend;
        }

        public void Start()
        {
           
            stan = new PLCto();
            stan.SlotconnPC = 3;
            stan.RackconnPC = 0;
            stan.IPconnPLC= new byte[] { 192, 168, 0, 11 }; //Передаем адресс контроллера
            stan.StartAdressTag = 3000; //старт адресов с 3000
            stan.Amount = 315; //Размер буфера для принятия данных в байтах

            stan.blPLStoDB101ms = blstan101ms;  //Битовый сигнал разрешающий обработки и запись в БД с циклом 101ms
            stan.Data101ms = stanData100ms;     // Словарь значений Тег <-> поле БД


            stan.blPLStoDBMessage = blstan200ms;
            stan.blPLStoDB1s = blstan1s;

            


            stan.Start();
        }



        public void Stop()
        {
            stan.Stop();
        }
    }

}
