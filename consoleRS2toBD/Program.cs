﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LoggerInSystem;

using System.Runtime.InteropServices;


namespace consoleRS2toBD
{
    

    class Program
    {
        #region Переменные сообщения по стану1700
        public static string messageErrorSt100mc;
        public static DateTime dtErrorSt100mc;
        public static string messageOKSt100mc;
        public static DateTime dtOKSt100mc;
        public static string ConMessageErrorSt100mc;
        public static string ConMessageOKSt100mc;

        public static string messageErrorSt101mc;
        public static DateTime dtErrorSt101mc;
        public static string messageOKSt101mc;
        public static DateTime dtOKSt101mc;
        public static string ConMessageErrorSt101mc;
        public static string ConMessageOKSt101mc;

        public static string messageErrorSt200mc;
        public static DateTime dtErrorSt200mc;
        public static string messageOKSt200mc;
        public static DateTime dtOKSt200mc;
        public static string ConMessageErrorSt200mc;
        public static string ConMessageOKSt200mc;

        public static string messageErrorSt1c;
        public static DateTime dtErrorSt1c;
        public static string messageOKSt1c;
        public static DateTime dtOKSt1c;
        public static string ConMessageErrorSt1c;
        public static string ConMessageOKSt1c;

        public static string messageErrorStRulon;
        public static DateTime dtErrorStRulon;
        public static string messageOKStRulon;
        public static DateTime dtOKStRulon;
        public static string ConMessageErrorStRulon;
        public static string ConMessageOKStRulon;

        public static string messageErrorStProizvodstvo;
        public static DateTime dtErrorStProizvodstvo;
        public static string messageOKStProizvodstvo;
        public static DateTime dtOKStProizvodstvo;
        public static string ConMessageErrorStProizvodstvo;
        public static string ConMessageOKStProizvodstvo;


        public static string messageErrorStValki;
        public static DateTime dtErrorStValki;
        public static string messageOKStValki;
        public static DateTime dtOKStValki;
        public static string ConMessageErrorStValki;
        public static string ConMessageOKStValki;

        #endregion

        #region Переменные сообщения по Дрессировчному стану
        public static string messageErrorDs100mc;
        public static DateTime dtErrorDs100mc;
        public static string messageOKDs100mc;
        public static DateTime dtOKDs100mc;
        public static string ConMessageErrorDs100mc;
        public static string ConMessageOKDs100mc;

        public static string messageErrorDs101mc;
        public static DateTime dtErrorDs101mc;
        public static string messageOKDs101mc;
        public static DateTime dtOKDs101mc;
        public static string ConMessageErrorDs101mc;
        public static string ConMessageOKDs101mc;

        public static string messageErrorDs200mc;
        public static DateTime dtErrorDs200mc;
        public static string messageOKDs200mc;
        public static DateTime dtOKDs200mc;
        public static string ConMessageErrorDs200mc;
        public static string ConMessageOKDs200mc;

        public static string messageErrorDs1c;
        public static DateTime dtErrorDs1c;
        public static string messageOKDs1c;
        public static DateTime dtOKDs1c;
        public static string ConMessageErrorDs1c;
        public static string ConMessageOKDs1c;

        public static string messageErrorDsRulon;
        public static DateTime dtErrorDsRulon;
        public static string messageOKDsRulon;
        public static DateTime dtOKDsRulon;
        public static string ConMessageErrorDsRulon;
        public static string ConMessageOKDsRulon;

        public static string messageErrorDsProizvodstvo;
        public static DateTime dtErrorDsProizvodstvo;
        public static string messageOKDsProizvodstvo;
        public static DateTime dtOKDsProizvodstvo;
        public static string ConMessageErrorDsProizvodstvo;
        public static string ConMessageOKDsProizvodstvo;

        public static string messageErrorDsValki;
        public static DateTime dtErrorDsValki;
        public static string messageOKDsValki;
        public static DateTime dtOKDsValki;
        public static string ConMessageErrorDsValki;
        public static string ConMessageOKDsValki;
        #endregion


        static void Main(string[] args)
        {
            //Настройка на региональных параметров 
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Для выбора ветки программы выберите 1 или 2");
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("1) Запуск сбора данных по стану 1700");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("2) Запуск сбора данных по дрессировочному стану");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Выберите 1 или 2");
            ConsoleKeyInfo key;

            switch (Console.ReadLine())
            {
                case "2":
                    clDs stanDs = new clDs();
                    Thread task2 = new Thread(stanDs.goStart);
                    task2.Start();
                    break;
                case "1":
                    clStan1700 stan1700 = new clStan1700();
                    Thread task1 = new Thread(stan1700.goStart);
                    task1.Start();
                    break;
                default:
                    break;
            }



            //if (key.Key.ToString() == "d" || key.Key.ToString() == "D" || key.Key.ToString() == "В" || key.Key.ToString() == "в")
            //{
            //    clDs stanDs = new clDs();
            //    Thread task2 = new Thread(stanDs.goStart);
            //    task2.Start();
            //}
            //else if (key.Key.ToString() == "s" || key.Key.ToString() == "S" || key.Key.ToString() == "Ы" || key.Key.ToString() == "ы")
            //{
            //    clStan1700 stan1700 = new clStan1700();
            //    Thread task1 = new Thread(stan1700.goStart);
            //    task1.Start();
            //}

            Console.Clear();
            Console.SetWindowSize(190, 40);







            //clStan1700 stan1700 = new clStan1700();
            //Thread task1 = new Thread(stan1700.goStart);
            //task1.Start();

            //clDs stanDs = new clDs();
            //Thread task2 = new Thread(stanDs.goStart);
            //task2.Start();

            string MesMain = DateTime.Now.ToString("HH:mm dd.MM.yyyy");
            String strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            

            string strVersionData = strVersion + "  (" + clVersionDT.Decript()+ ")";
            //Console.Clear();


            bool clean = false;

            while (true)
            {
                Thread.Sleep(5000);
                

                
                if (false)
                {

                    messageErrorSt100mc = null;
                    messageOKSt100mc = null;
                    messageErrorSt101mc = null;
                    messageOKSt101mc = null;
                    messageErrorSt200mc = null;
                    messageOKSt200mc = null;
                    messageErrorSt1c = null;
                    messageOKSt1c = null;
                    messageErrorStRulon = null;
                    messageOKStRulon = null;
                    messageErrorStProizvodstvo = null;
                    messageOKStProizvodstvo = null;
                    messageErrorStValki = null;
                    messageOKStValki = null;

                    messageErrorDs100mc = null;
                    messageOKDs100mc = null;
                    messageErrorDs101mc = null;
                    messageOKDs101mc = null;
                    messageErrorDs200mc = null;
                    messageOKDs200mc = null;
                    messageErrorDs1c = null;
                    messageOKDs1c = null;
                    messageErrorDsRulon = null;
                    messageOKDsRulon = null;
                    messageErrorDsProizvodstvo = null;
                    messageOKDsProizvodstvo = null;
                    messageErrorDsValki = null;
                    messageOKDsValki = null;


                }



                LogSystem.Write("Время запуска программы: ", Direction.Ok, MesMain, 20, 0, true);

                LogSystem.Write("Version: ", Direction.Ok, strVersionData, 120, 0, true);

                LogSystem.Write("Для очистки экрана нажмите на клавишу С", Direction.Ok, "", 70, 1, true);


                #region Вывод на консоль сообщений по стану

                LogSystem.Write("Стан1700", Direction.Ok, "Информация о работе методов класса clStan1700 ( Цикл 5сек )", 1, 3, true);

                if (messageErrorSt100mc != null)
                {
                    ConMessageErrorSt100mc = messageErrorSt100mc;
                    LogSystem.Write("Стан1700 ERROR цикла 100mc", Direction.ERROR, dtErrorSt100mc, ConMessageErrorSt100mc, 1, 5, true);
                }
                if (messageOKSt100mc != null)
                {
                    ConMessageOKSt100mc = messageOKSt100mc;
                    LogSystem.Write("Стан1700 connection (цикл 100mc)", Direction.Ok, dtOKSt100mc, ConMessageOKSt100mc, 1, 6, true);
                }
                if (messageErrorSt101mc != null)
                {
                    ConMessageErrorSt101mc = messageErrorSt101mc;
                    LogSystem.Write("Стан1700 ERROR цикла 101mc", Direction.ERROR, dtErrorSt101mc, ConMessageErrorSt101mc, 1, 8, true);
                }
                if (messageOKSt101mc != null)
                {
                    ConMessageOKSt101mc = messageOKSt101mc;
                    LogSystem.Write("Стан1700. Read PLC and Write во временную таблицу (цикл 101mc)", Direction.Ok, dtOKSt101mc, ConMessageOKSt101mc, 1, 9, true);
                }
                if (messageErrorSt200mc != null)
                {
                    ConMessageErrorSt200mc = messageErrorSt200mc;
                    LogSystem.Write("Стан1700 ERROR цикла 200mc", Direction.ERROR, dtErrorSt200mc, ConMessageErrorSt200mc, 1, 11, true);
                }
                if (messageOKSt200mc != null)
                {
                    ConMessageOKSt200mc = messageOKSt200mc;
                    LogSystem.Write("Стан1700 Сообщения(цикл 200mc)", Direction.Ok, dtOKSt200mc, ConMessageOKSt200mc, 1, 12, true);
                }
                if (messageErrorSt1c != null)
                {
                    ConMessageErrorSt1c = messageErrorSt1c;
               
                    LogSystem.Write("Стан1700 ERROR цикла 1c", Direction.ERROR, dtErrorSt1c, ConMessageErrorSt1c, 1, 14, true);
                }
                if (messageOKSt1c != null)
                {
                    ConMessageOKSt1c = messageOKSt1c;
                    LogSystem.Write("Стан1700. Цикл 1c.", Direction.Ok, dtOKSt1c, ConMessageOKSt1c, 1, 15, true);
                }
                if (messageErrorStRulon != null)
                {
                    ConMessageErrorStRulon = messageErrorStRulon;
                    LogSystem.Write("Стан1700 ERROR при переименовании таблицы рулонов", Direction.ERROR, dtErrorStRulon, ConMessageErrorStRulon, 1, 17, true);
                }
                if (messageOKStRulon != null)
                {
                    ConMessageOKStRulon = messageOKStRulon;
                    LogSystem.Write("Стан1700. Rename-", Direction.Ok, dtOKStRulon, ConMessageOKStRulon, 1, 18, true);
                }
                if (messageErrorStProizvodstvo != null)
                {
                    ConMessageErrorStProizvodstvo = messageErrorStProizvodstvo;
                    
                    LogSystem.Write("Стан1700 ERROR записи в таблицу Производства", Direction.ERROR, dtErrorStProizvodstvo, ConMessageErrorStProizvodstvo, 1, 20, true);
                }
                if (messageOKStProizvodstvo != null)
                {
                    ConMessageOKStProizvodstvo = messageOKStProizvodstvo;
                    LogSystem.Write("Стан1700. Производствo.", Direction.Ok, dtOKStProizvodstvo, ConMessageOKStProizvodstvo, 1, 21, true);
                }
                if (messageErrorStValki != null)
                {
                    ConMessageErrorStValki = messageErrorStValki;
                    
                    LogSystem.Write("Стан1700 ERROR записи в таблицу перевалок валков", Direction.ERROR, dtErrorStValki, ConMessageErrorStValki, 1, 23, true);
                }
                if (messageOKStValki != null)
                {
                    ConMessageOKStValki = messageOKStValki;

                    LogSystem.Write("Стан1700. Write перевалок валков.", Direction.Ok, dtOKStValki, ConMessageOKStValki, 1, 24, true);
                }


                #endregion

                #region Вывод на консоль сообщений по дрессировке


                LogSystem.Write("Дрессировка", Direction.Ok, "Информация о работе методов класса Дрессировочного стана ( Цикл 5сек )", 100, 3, true);

                if (messageErrorDs100mc != null)
                {
                    ConMessageErrorDs100mc = messageErrorDs100mc;
                    
                    LogSystem.Write("Дрессировка. ERROR цикла 70mc", Direction.ERROR, dtErrorDs100mc, ConMessageErrorDs100mc, 100, 5, true);
                }
                if (messageOKDs100mc != null)
                {
                    ConMessageOKDs100mc = messageOKDs100mc;
                    LogSystem.Write("Дрессировка. Сonnection (цикл 70mc)", Direction.Ok, dtOKDs100mc, ConMessageOKDs100mc, 100, 6, true);
                }
                if (messageErrorDs101mc != null)
                {
                    ConMessageErrorDs101mc = messageErrorDs101mc;
                    
                    LogSystem.Write("Дрессировка. ERROR цикла 80mc", Direction.ERROR, dtErrorDs101mc, ConMessageErrorDs101mc, 100, 8, true);
                }
                if (messageOKDs101mc != null)
                {
                    ConMessageOKDs101mc = messageOKDs101mc;
                    LogSystem.Write("Дрессировка. Read PLC and Write во временную таблицу (цикл 80mc)", Direction.Ok, dtOKDs101mc, ConMessageOKDs101mc, 100, 9, true);
                }
                if (messageErrorDs200mc != null)
                {
                    ConMessageErrorDs200mc = messageErrorDs200mc;
                    
                    LogSystem.Write("Дрессировка.ERROR цикла 200mc", Direction.ERROR, dtErrorDs200mc, ConMessageErrorDs200mc, 100, 11, true);
                }
                if (messageOKDs200mc != null)
                {
                    ConMessageOKDs200mc = messageOKDs200mc;
                    LogSystem.Write("Дрессировка.Сообщения(цикл 200mc)", Direction.Ok, dtOKDs200mc, ConMessageOKDs200mc, 100, 12, true);
                }
                if (messageErrorDs1c != null)
                {
                    ConMessageErrorDs1c = messageErrorDs1c;
                    
                    LogSystem.Write("Дрессировка.ERROR цикла 1c", Direction.ERROR, dtErrorDs1c, ConMessageErrorDs1c, 100, 14, true);
                }
                if (messageOKDs1c != null)
                {
                    ConMessageOKDs1c = messageOKDs1c;
                    LogSystem.Write("Дрессировка.Цикл 1c.", Direction.Ok, dtOKDs1c, ConMessageOKDs1c, 100, 15, true);
                }
                if (messageErrorDsRulon != null)
                {
                    ConMessageErrorDsRulon = messageErrorDsRulon;
                    
                    LogSystem.Write("Дрессировка. ERROR Rename", Direction.ERROR, dtErrorDsRulon, ConMessageErrorDsRulon, 100, 17, true);
                }
                if (messageOKDsRulon != null)
                {
                    ConMessageOKDsRulon = messageOKDsRulon;

                    LogSystem.Write("Дрессировка. Rename", Direction.Ok, dtOKDsRulon, ConMessageOKDsRulon, 100, 18, true);
                }
                if (messageErrorDsProizvodstvo != null)
                {
                    ConMessageErrorDsProizvodstvo = messageErrorDsProizvodstvo;
                    
                    LogSystem.Write("Дрессировка. ERROR Write Производства", Direction.ERROR, dtErrorDsProizvodstvo, ConMessageErrorDsProizvodstvo, 100, 20, true);
                }
                if (messageOKDsProizvodstvo != null)
                {
                    ConMessageOKDsProizvodstvo = messageOKDsProizvodstvo;

                    LogSystem.Write("Дрессировка. Производство", Direction.Ok, dtOKDsProizvodstvo, ConMessageOKDsProizvodstvo, 100, 21, true);
                }
                if (messageErrorDsValki != null)
                {
                    ConMessageErrorDsValki = messageErrorDsValki;
                    
                    LogSystem.Write("Дрессировка. ERROR записи в таблицу перевалок валков", Direction.ERROR, dtErrorDsValki, ConMessageErrorDsValki, 100, 23, true);
                }
                if (messageOKDsValki != null)
                {
                    ConMessageOKDsValki = messageOKDsValki;

                    LogSystem.Write("Дрессировка. Запись в таблицу перевалок валков", Direction.Ok, dtOKDsValki, ConMessageOKDsValki, 100, 24, true);
                }


                #endregion

                #region Сброс сообщений в конце цикла

                
                messageOKSt100mc = null;
                messageErrorSt101mc = null;
                messageOKSt101mc = null;
                messageErrorSt200mc = null;
                messageOKSt200mc = null;
                messageErrorSt1c = null;
                messageOKSt1c = null;
                messageErrorStRulon = null;
                messageOKStRulon = null;
                messageErrorStProizvodstvo = null;
                messageOKStProizvodstvo = null;
                messageErrorStValki = null;
                messageOKStValki = null;

                messageErrorDs100mc = null;
                messageOKDs100mc = null;
                messageErrorDs101mc = null;
                messageOKDs101mc = null;
                messageErrorDs200mc = null;
                messageOKDs200mc = null;
                messageErrorDs1c = null;
                messageOKDs1c = null;
                messageErrorDsRulon = null;
                messageOKDsRulon = null;
                messageErrorDsProizvodstvo = null;
                messageOKDsProizvodstvo = null;
                messageErrorDsValki = null;
                messageOKDsValki = null;

                #endregion

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.C) clean = true;
                    else break;

                    if (clean)
                    {
                        Console.Clear();
                    }

                    System.Threading.Thread.Sleep(1000);

                }


            }


            //ConsoleKeyInfo cki;
            //do
            //{
            //    while (Console.KeyAvailable == false)
            //    {
            //        Thread.Sleep(5000);
            //        Console.Clear();

            //        //int iKey = Console.Read();
            //        //char cKey = Convert.ToChar(iKey);

            //        //ConsoleKeyInfo key = Console.ReadKey();

            //        if (false)
            //        {

            //            messageErrorSt100mc = null;
            //            messageOKSt100mc = null;
            //            messageErrorSt101mc = null;
            //            messageOKSt101mc = null;
            //            messageErrorSt200mc = null;
            //            messageOKSt200mc = null;
            //            messageErrorSt1c = null;
            //            messageOKSt1c = null;
            //            messageErrorStRulon = null;
            //            messageOKStRulon = null;
            //            messageErrorStProizvodstvo = null;
            //            messageOKStProizvodstvo = null;
            //            messageErrorStValki = null;
            //            messageOKStValki = null;

            //            messageErrorDs100mc = null;
            //            messageOKDs100mc = null;
            //            messageErrorDs101mc = null;
            //            messageOKDs101mc = null;
            //            messageErrorDs200mc = null;
            //            messageOKDs200mc = null;
            //            messageErrorDs1c = null;
            //            messageOKDs1c = null;
            //            messageErrorDsRulon = null;
            //            messageOKDsRulon = null;
            //            messageErrorDsProizvodstvo = null;
            //            messageOKDsProizvodstvo = null;
            //            messageErrorDsValki = null;
            //            messageOKDsValki = null;



            //        }

            //        #region Вывод на консоль сообщений по стану

            //        LogSystem.Write("Стан1700", Direction.Ok, "Информация о работе методов класса clStan1700 ( Цикл 5сек )", 1, 1, true);

            //        if (messageErrorSt100mc != null)
            //        {
            //            LogSystem.Write("Стан1700 ERROR цикла 100mc", Direction.ERROR, dtErrorSt100mc, messageErrorSt100mc, 1, 5, true);
            //        }
            //        if (messageOKSt100mc != null)
            //        {
            //            LogSystem.Write("Стан1700 connection (цикл 100mc)", Direction.Ok, dtOKSt100mc, messageOKSt100mc, 1, 6, true);
            //        }


            //        if (messageErrorSt101mc != null)
            //        {
            //            LogSystem.Write("Стан1700 ERROR цикла 101mc", Direction.ERROR, dtErrorSt101mc, messageErrorSt101mc, 1, 8, true);
            //        }
            //        if (messageOKSt101mc != null)
            //        {
            //            LogSystem.Write("Стан1700. Read PLC and Write во временную таблицу (цикл 101mc)", Direction.Ok, dtOKSt101mc, messageOKSt101mc, 1, 9, true);
            //        }

            //        if (messageErrorSt200mc != null)
            //        {
            //            LogSystem.Write("Стан1700 ERROR цикла 200mc", Direction.ERROR, dtErrorSt200mc, messageErrorSt200mc, 1, 11, true);
            //        }
            //        if (messageOKSt200mc != null)
            //        {
            //            LogSystem.Write("Стан1700 Сообщения(цикл 200mc)", Direction.Ok, dtOKSt200mc, messageOKSt200mc, 1, 12, true);
            //        }

            //        if (messageErrorSt1c != null)
            //        {
            //            LogSystem.Write("Стан1700 ERROR цикла 1c", Direction.ERROR, dtErrorSt1c, messageErrorSt1c, 1, 14, true);
            //        }
            //        if (messageOKSt1c != null)
            //        {
            //            LogSystem.Write("Стан1700. Цикл 1c.", Direction.Ok, dtOKSt1c, messageOKSt1c, 1, 15, true);
            //        }



            //        if (messageErrorStRulon != null)
            //        {
            //            LogSystem.Write("Стан1700 ERROR при переименовании таблицы рулонов", Direction.ERROR, dtErrorStRulon, messageErrorStRulon, 1, 17, true);
            //        }
            //        if (messageOKStRulon != null)
            //        {
            //            LogSystem.Write("Стан1700. Rename-", Direction.Ok, dtOKStRulon, messageOKStRulon, 1, 18, true);
            //        }



            //        if (messageErrorStProizvodstvo != null)
            //        {
            //            LogSystem.Write("Стан1700 ERROR записи в таблицу Производства", Direction.ERROR, dtErrorStProizvodstvo, messageErrorStProizvodstvo, 1, 20, true);
            //        }
            //        if (messageOKStProizvodstvo != null)
            //        {
            //            LogSystem.Write("Стан1700. Производствo.", Direction.Ok, dtOKStProizvodstvo, messageOKStProizvodstvo, 1, 21, true);
            //        }



            //        if (messageErrorStValki != null)
            //        {
            //            LogSystem.Write("Стан1700 ERROR записи в таблицу перевалок валков", Direction.ERROR, dtErrorStValki, messageErrorStValki, 1, 23, true);
            //        }
            //        if (messageOKStValki != null)
            //        {
            //            LogSystem.Write("Стан1700. Write перевалок валков.", Direction.Ok, dtOKStValki, messageOKStValki, 1, 24, true);
            //        }


            //        #endregion

            //        #region Вывод на консоль сообщений по дрессировке

            //        LogSystem.Write("Дрессировка", Direction.Ok, "Информация о работе методов класса Дрессировочного стана ( Цикл 5сек )", 100, 1, true);

            //        if (messageErrorDs100mc != null)
            //        {
            //            LogSystem.Write("Дрессировка. ERROR цикла 70mc", Direction.ERROR, dtErrorDs100mc, messageErrorDs100mc, 100, 5, true);
            //        }
            //        if (messageOKDs100mc != null)
            //        {
            //            LogSystem.Write("Дрессировка. Сonnection (цикл 70mc)", Direction.Ok, dtOKDs100mc, messageOKDs100mc, 100, 6, true);
            //        }


            //        if (messageErrorDs101mc != null)
            //        {
            //            LogSystem.Write("Дрессировка. ERROR цикла 80mc", Direction.ERROR, dtErrorDs101mc, messageErrorDs101mc, 100, 8, true);
            //        }
            //        if (messageOKDs101mc != null)
            //        {
            //            LogSystem.Write("Дрессировка. Read PLC and Write во временную таблицу (цикл 80mc)", Direction.Ok, dtOKDs101mc, messageOKDs101mc, 100, 9, true);
            //        }

            //        if (messageErrorDs200mc != null)
            //        {
            //            LogSystem.Write("Дрессировка.ERROR цикла 200mc", Direction.ERROR, dtErrorDs200mc, messageErrorDs200mc, 100, 11, true);
            //        }
            //        if (messageOKDs200mc != null)
            //        {
            //            LogSystem.Write("Дрессировка.Сообщения(цикл 200mc)", Direction.Ok, dtOKDs200mc, messageOKDs200mc, 100, 12, true);
            //        }

            //        if (messageErrorDs1c != null)
            //        {
            //            LogSystem.Write("Дрессировка.ERROR цикла 1c", Direction.ERROR, dtErrorDs1c, messageErrorDs1c, 100, 14, true);
            //        }
            //        if (messageOKDs1c != null)
            //        {
            //            LogSystem.Write("Дрессировка.Цикл 1c.", Direction.Ok, dtOKDs1c, messageOKDs1c, 100, 15, true);
            //        }



            //        if (messageErrorDsRulon != null)
            //        {
            //            LogSystem.Write("Дрессировка. ERROR Rename", Direction.ERROR, dtErrorDsRulon, messageErrorDsRulon, 100, 17, true);
            //        }
            //        if (messageOKDsRulon != null)
            //        {
            //            LogSystem.Write("Дрессировка. Rename", Direction.Ok, dtOKDsRulon, messageOKDsRulon, 100, 18, true);
            //        }



            //        if (messageErrorDsProizvodstvo != null)
            //        {
            //            LogSystem.Write("Дрессировка. ERROR Write Производства", Direction.ERROR, dtErrorDsProizvodstvo, messageErrorDsProizvodstvo, 100, 20, true);
            //        }
            //        if (messageOKDsProizvodstvo != null)
            //        {
            //            LogSystem.Write("Дрессировка. Производство", Direction.Ok, dtOKDsProizvodstvo, messageOKDsProizvodstvo, 100, 21, true);
            //        }



            //        if (messageErrorDsValki != null)
            //        {
            //            LogSystem.Write("Дрессировка. ERROR записи в таблицу перевалок валков", Direction.ERROR, dtErrorDsValki, messageErrorDsValki, 100, 23, true);
            //        }
            //        if (messageOKDsValki != null)
            //        {
            //            LogSystem.Write("Дрессировка. Запись в таблицу перевалок валков", Direction.Ok, dtOKDsValki, messageOKDsValki, 100, 24, true);
            //        }


            //        #endregion


            //    }

            //    cki = Console.ReadKey(true);
            //    Console.WriteLine(cki.Key);
            //} while (cki.Key != ConsoleKey.X);



        }

        
    }
}
