using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static public void goStart()
        {
            clPLCtoBD ds = new clPLCtoBD();
            ds.CursorPositionLeft = 70;
            ds.CursorPositionTop = 1;
            ds.NamePLC = "ДрессировочныйСтан1700";
            
            ds.SlotconnPC = 2;
            ds.RackconnPC = 0;
            ds.IPconnPLC = new byte[] { 192, 168, 0, 21 }; //Передаем адресс контроллера
            ds.StartAdressTag = 2000; //старт адресов с 3000
            ds.Amount = 150; //Размер буфера для принятия данных в байтах
            ds.connect = 1;

            

            ds.Start();
        }

    }
}
