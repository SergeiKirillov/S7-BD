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
        bool stan101ms; //данные по прокатке рулона, формируются таблица после прокатанного рулона. 
        bool stan1s;    //данные по работе стана, формируются и через 1мин (~62 сообщения) скидываются в БД.
        bool stan200ms; //сообщения формируются в течении 60 секунд и после этого записываются в БД.

        bool NetSend; //Передача данных по сети для создания визуализации на удаленном компьютере

        PLCto stan;



        public ClassStan(bool stan101ms, bool stan1s, bool stan200ms, bool NetSend)
        {
            this.stan101ms = stan101ms;
            this.stan1s = stan1s;
            this.stan200ms = stan200ms;

            this.NetSend = NetSend;
        }

        public void Start()
        {
           
            stan = new PLCto();
            stan.SlotconnPC = 3;
            stan.RackconnPC = 0;
            stan.IPconnPLC= new byte[] { 192, 168, 0, 11 }; //Передаем адресс контроллера
            stan.StartAdressTag = 3000; //старт адресов с 3000
            stan.Amount = 315; //Размер буфера для принятия данных в байтах

            stan.PLStoDB101ms = stan101ms;
            stan.PLStoDBMessage = stan200ms;
            stan.PLStoDB1s = stan1s;


            stan.Start();
        }



        public void Stop()
        {
            stan.Stop();
        }
    }

}
