using HWDiag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Media;
using System.Diagnostics;
using LoggerInSystem;



class ClassDresStan
{

    bool blds101ms; //данные по прокатке рулона, формируются таблица после прокатанного рулона. 
    bool blds1s;    //данные по работе стана, формируются и через 1мин (~62 сообщения) скидываются в БД.
    bool blds200ms; //сообщения формируются в течении 60 секунд и после этого записываются в БД.

    bool blNetSend; //Передача данных по сети для создания визуализации на удаленном компьютере

    PLCto dsstan;

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
        {"dmot", new ContData(18,1,true)},          //RMot
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






    public ClassDresStan(bool ds101ms, bool ds1s, bool ds200ms, bool NetSend)
    {
        this.blds101ms = ds101ms;
        this.blds1s = ds1s;
        this.blds200ms = ds200ms;

        this.blNetSend = NetSend;
    }

    public void Start()
    {
        dsstan = new PLCto();
        dsstan.NamePLC = "DresStan";
        dsstan.SlotconnPC = 2;
        dsstan.RackconnPC = 0;
        dsstan.IPconnPLC = new byte[] { 192, 168, 0, 21 }; //Передаем адресс контроллера
        dsstan.StartAdressTag = 2000; //старт адресов с 3000
        dsstan.Amount = 150; //Размер буфера для принятия данных в байтах
        dsstan.connect = 1;
       
        dsstan.blPLStoDB101ms = blds101ms;  //Битовый сигнал разрешающий обработки и запись в БД с циклом 101ms
        dsstan.Data101ms = stanData100ms;     // Словарь значений Тег <-> поле БД
        dsstan.blPLSPasportRulona = false;
        dsstan.dMot = 0.301;
        
        dsstan.blPLStoDBMessage = blds200ms;
        dsstan.blPLStoDB1s = blds1s;

        dsstan.ConnectCurX = 0;
        dsstan.ConnectCurY = 7;

        dsstan.mc100CurX = 0;
        dsstan.mc100CurY = 8;

        dsstan.mc101CurX = 0;
        dsstan.mc101CurY = 9;

        dsstan.MessageCurX = 0;
        dsstan.MessageCurY = 10;

        dsstan.mc1000CurX = 0;
        dsstan.mc1000CurY = 11;


        //Console.WriteLine("................Дрессировка старт!");
        dsstan.Start();
    }



    public void Stop()
    {
        dsstan.Stop();
    }
        
}
