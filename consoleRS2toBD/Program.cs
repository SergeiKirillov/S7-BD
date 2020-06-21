using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LoggerInSystem;

namespace consoleRS2toBD
{
    class Program
    {
        public static string messageError100mc;
        public static DateTime dtError100mc;
        public static string messageOK100mc;
        public static DateTime dtOK100mc;
        
        public static string messageError101mc;
        public static DateTime dtError101mc;
        public static string messageOK101mc;
        public static DateTime dtOK101mc;
        
        public static string messageError200mc;
        public static DateTime dtError200mc;
        public static string messageOK200mc;
        public static DateTime dtOK200mc;
        
        public static string messageError1c;
        public static DateTime dtError1c;
        public static string messageOK1c;
        public static DateTime dtOK1c;
        
        public static string messageErrorRulon;
        public static DateTime dtErrorRulon;
        public static string messageOKRulon;
        public static DateTime dtOKRulon;
        
        public static string messageErrorProizvodstvo;
        public static DateTime dtErrorProizvodstvo;
        public static string messageOKProizvodstvo;
        public static DateTime dtOKProizvodstvo;
        
        public static string messageErrorValki;
        public static DateTime dtErrorValki;
        public static string messageOKValki;
        public static DateTime dtOKValki;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetWindowSize(200, 40);

            clStan1700 stan1700 = new clStan1700();
            Thread task1 = new Thread(stan1700.goStart);
            task1.Start();

            //clStanDress stanDs = new clStanDress();
            //Thread task2 = new Thread(stanDs.goStart);
            //task2.Start();

            while (true)
            {
                Thread.Sleep(5000);
                Console.Clear();

                #region Вывод на консоль

                LogSystem.Write("Стан1700", Direction.Ok, "Информация о работе методов класса clStan1700 ( Цикл 5сек )", 1, 1, true);

                if (messageError100mc != null)
                {
                    LogSystem.Write("Стан1700 ERROR цикла 100mc", Direction.ERROR, dtError100mc, messageError100mc, 1, 5, true);
                }
                if (messageOK100mc != null)
                {
                    LogSystem.Write("Стан1700 connection (цикл 100mc)", Direction.Ok, dtOK100mc, messageOK100mc, 1, 6, true);
                }


                if (messageError101mc != null)
                {
                    LogSystem.Write("Стан1700 ERROR цикла 101mc", Direction.ERROR, dtError101mc, messageError101mc, 1, 8, true);
                }
                if (messageOK101mc != null)
                {
                    LogSystem.Write("Стан1700 Получение данных и запись во временную таблицу (цикл 101mc)", Direction.Ok, dtOK101mc, messageOK101mc, 1, 9, true);
                }

                if (messageError200mc != null)
                {
                    LogSystem.Write("Стан1700 ERROR цикла 200mc", Direction.ERROR, dtError200mc, messageError200mc, 1, 11, true);
                }
                if (messageOK200mc != null)
                {
                    LogSystem.Write("Стан1700 Сообщения (цикл 200mc)", Direction.Ok, dtOK200mc, messageOK200mc, 1, 12, true);
                }

                if (messageError1c != null)
                {
                    LogSystem.Write("Стан1700 ERROR цикла 1c", Direction.ERROR, dtError1c, messageError1c, 1, 14, true);
                }
                if (messageOK1c != null)
                {
                    LogSystem.Write("Стан1700 Получение данных с энергосистемы (Цикл 1c)", Direction.Ok, dtOK1c, messageOK1c, 1, 15, true);
                }



                if (messageErrorRulon != null)
                {
                    LogSystem.Write("Стан1700 ERROR при переименовании таблицы рулонов", Direction.ERROR, dtErrorRulon, messageErrorRulon, 1, 17, true);
                }
                if (messageOKRulon != null)
                {
                    LogSystem.Write("Стан1700 переименование таблицы рулонов", Direction.Ok, dtOKRulon, messageOKRulon, 1, 18, true);
                }



                if (messageErrorProizvodstvo != null)
                {
                    LogSystem.Write("Стан1700 ERROR записи в таблицу Производства", Direction.ERROR, dtErrorProizvodstvo, messageErrorProizvodstvo, 1, 20, true);
                }
                if (messageOKProizvodstvo != null)
                {
                    LogSystem.Write("Стан1700 запись в таблицу производства", Direction.Ok, dtOKProizvodstvo, messageOKProizvodstvo, 1, 21, true);
                }



                if (messageErrorValki != null)
                {
                    LogSystem.Write("Стан1700 ERROR записи в таблицу перевалок валков", Direction.ERROR, dtErrorValki, messageErrorValki, 1, 23, true);
                }
                if (messageOKValki != null)
                {
                    LogSystem.Write("Стан1700 запись в таблицу перевалок валков", Direction.Ok, dtOKValki, messageOKValki, 1, 24, true);
                }

                
                #endregion
            }

        }
    }
}
