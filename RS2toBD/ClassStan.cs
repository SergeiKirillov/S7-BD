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
        bool stan100ms; //данные по прокатке рулона, формируются таблица после прокатанного рулона. 
        bool stan1s;    //данные по работе стана, формируются и через 1мин (~62 сообщения) скидываются в БД.
        bool stan200ms; //сообщения формируются в течении 60 секунд и после этого записываются в БД.

        bool NetSend; //Передача данных по сети для создания визуализации на удаленном компьютере

        private Prodave stan;
        int Connect = 0;

        Timer TTimer100ms;
        Timer TTimerMessage;
        Timer TTimerSQL;
        Timer TTimer1s;
        //Timer TTimer250msNet;


        SolidColorBrush offLed = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        SolidColorBrush onOK = new SolidColorBrush(Color.FromRgb(130, 190, 125));
        SolidColorBrush onError = new SolidColorBrush(Color.FromRgb(255, 0, 0));


        public ClassStan(bool stan100ms, bool stan1s, bool stan200ms, bool NetSend)
        {
            this.stan100ms = stan100ms;
            this.stan1s = stan1s;
            this.stan200ms = stan200ms;
            this.NetSend = NetSend;
        }

        #region Внешний метод  создаеющий соединение с контроллером и запускающий метод считывание данных с контроллера
        public void Start()
        {

            //Метод производит подключение к котроллеру и устанавливает связь
            //Если соединение успешно то вызывает поток-таймеры. и внутри них выполнение действия по таймеру.

            try
            {
                //запуск таймеров 100ms(100ms), 101ms(SQL), 200ms(message), 1000ms(1s)
                stan = new Prodave();
                byte[] conn = new byte[] { 192, 168, 0, 11 }; //ip адресс контроллера
                int res = stan.LoadConnection(Connect, 2, conn, 3, 0);

                if (res != 0)
                {
                    LogSystem.Write("StanStart", Direction.ERROR, "Error connection!. Error - " + stan.Error(res));

                }
                else
                {
                    LogSystem.Write("StanStart", Direction.Ok, "Connect OK!");

                    int resSAC = stan.SetActiveConnection(Connect);
                    if (resSAC == 0)
                    {
                        LogSystem.Write("StanStart", Direction.Ok, "Соединение активно.");

                        TTimer100ms = new Timer(new TimerCallback(TicTimer100ms), null, 0, 100);
                        TTimerMessage = new Timer(new TimerCallback(TicTimerMessage), null, 0, 200);
                        TTimerSQL = new Timer(new TimerCallback(TicTimerSQL), null, 0, 101);
                        TTimer1s = new Timer(new TimerCallback(TicTimer1s), null, 0, 1000);

                    }
                    else
                    {
                        LogSystem.Write("StanStart", Direction.WARNING, "Соединение не активировано. " + stan.Error(resSAC));

                    }

                }
            }
            catch { /*все исключения кидаем в пустоту*/ }
        }

        #endregion

        #region считывание данных с контроллера и запись их в критичный буфер
        private void TicTimer100ms(object state)
        {


        }
        #endregion


        private void TicTimer1s(object state)
        {

        }

        private void TicTimerSQL(object state)
        {

        }

        private void TicTimerMessage(object state)
        {

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

            }
            catch (Exception ex)
            {
                LogSystem.Write("StanStop", Direction.WARNING, "Ошибка при остановке. " + ex.Message);
            }

        }
        #endregion
    }

}
