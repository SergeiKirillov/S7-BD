﻿using System;

using System.Diagnostics;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LoggerInSystem
{
    public class classEvents
    {
        public bool EventToConsole;
        public bool EventToFile;
        public bool EventToSystem;
        public bool EventToDebug;
        public EventLogEntryType elet;

        public classEvents(bool eventToConsole, bool eventToFile, bool eventToSystem, bool eventToDebug, EventLogEntryType ELET)
        {
            EventToConsole = eventToConsole;
            EventToFile = eventToFile;
            EventToSystem = eventToSystem;
            EventToDebug = eventToDebug;
            elet = ELET;
        }
    }

    public enum Direction
    {
        Ok,
        ERROR,
        WARNING,
        OkStanMessage,
        OkStanMessageNull,
        OkStan1s,
        OkStanPassportRulona,
        OkStanPerevalki
    }

    public class LogSystem
    {

        static string message0;
        
        static string eventLogName = "ProDave";

        static Dictionary<Direction, classEvents> dicEvent = new Dictionary<Direction, classEvents>()
        {
            [Direction.ERROR] = new classEvents(eventToConsole:true, eventToFile:true, eventToSystem:false, eventToDebug:true, ELET:EventLogEntryType.Error),
            [Direction.WARNING] = new classEvents(eventToConsole: true, eventToFile: true, eventToSystem: false, eventToDebug: true, ELET: EventLogEntryType.Warning),
            [Direction.Ok] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false, ELET: EventLogEntryType.SuccessAudit),
            [Direction.OkStan1s] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false, ELET: EventLogEntryType.Information),
            [Direction.OkStanMessage] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false, ELET: EventLogEntryType.Information),
            [Direction.OkStanMessageNull] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false, ELET: EventLogEntryType.Information),
            [Direction.OkStanPassportRulona] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false, ELET: EventLogEntryType.Information),
            [Direction.OkStanPerevalki] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false, ELET: EventLogEntryType.Information),
            
        };


        #region public метод который распределяет что куда писать в зависимости от переданного классаСообщения

        public static void Write(string _className, Direction _clasMessage, string _MessageText, int _CurX, int _CurY, bool dtVizible)
        {
            classEvents value;
            if (dicEvent.TryGetValue(_clasMessage, out value))
            {
                //Проходимся по словарю и если класс ошибки совпадает с классом словаря со словаря вытаскиваем значения необходимые
                // для распределения вывода
                //
                //к примеру 
                //LogSystem.Write("StanStart", Direction.ERROR, "Error connection!. Error - " + stan.Error(res));
                //Direction.Error соответствует в словаре 
                //  eventToConsole:true
                //  eventToFile:true
                //  eventToSystem:true
                //  eventToDebug:true 
                //  ELET:EventLogEntryType.Error),
                //соответственно вывод будет сделан на 
                //  консоль -   WriteConsoleLog(_clasMessage, _MessageText);
                //  в файл  -   WriteFileLog(_MessageText);
                //  в журнал сообщений виндоус - WriteEventLogApplication(_className, _MessageText, value.elet);
                //  в диагностическое окно VS - WriteOutputVSLog(_MessageText);


                if (value.EventToConsole) WriteConsoleLog(_clasMessage, _className +": "+_MessageText, _CurX, _CurY, dtVizible);
                if (value.EventToFile) WriteFileLog(_className,DateTime.Now, _MessageText);
                if (value.EventToSystem) WriteEventLogApplication(_className, _MessageText, value.elet);
                if (value.EventToDebug) WriteOutputVSLog(_MessageText);
            }
            
           

        }

        public static void Write(string _className, Direction _clasMessage, string _MessageText)
        {
            classEvents value;
            if (dicEvent.TryGetValue(_clasMessage, out value))
            {
                //Проходимся по словарю и если класс ошибки совпадает с классом словаря со словаря вытаскиваем значения необходимые
                // для распределения вывода
                //
                //к примеру 
                //LogSystem.Write("StanStart", Direction.ERROR, "Error connection!. Error - " + stan.Error(res));
                //Direction.Error соответствует в словаре 
                //  eventToConsole:true
                //  eventToFile:true
                //  eventToSystem:true
                //  eventToDebug:true 
                //  ELET:EventLogEntryType.Error),
                //соответственно вывод будет сделан на 
                //  консоль -   WriteConsoleLog(_clasMessage, _MessageText);
                //  в файл  -   WriteFileLog(_MessageText);
                //  в журнал сообщений виндоус - WriteEventLogApplication(_className, _MessageText, value.elet);
                //  в диагностическое окно VS - WriteOutputVSLog(_MessageText);


                if (value.EventToConsole) WriteConsoleLog(_clasMessage, _className + ": " + _MessageText, 10, 10, true);
                if (value.EventToFile) WriteFileLog(_className, DateTime.Now, _MessageText);
                if (value.EventToSystem) WriteEventLogApplication(_className, _MessageText, value.elet);
                if (value.EventToDebug) WriteOutputVSLog(_MessageText);
            }



        }


        public static void Write(string _className, Direction _clasMessage, DateTime dtNow, string _MessageText, int _CurX, int _CurY, bool dtVizible)
        {
            classEvents value;
            if (dicEvent.TryGetValue(_clasMessage, out value))
            {
                //Проходимся по словарю и если класс ошибки совпадает с классом словаря со словаря вытаскиваем значения необходимые
                // для распределения вывода
                //
                //к примеру 
                //LogSystem.Write("StanStart", Direction.ERROR, "Error connection!. Error - " + stan.Error(res));
                //Direction.Error соответствует в словаре 
                //  eventToConsole:true
                //  eventToFile:true
                //  eventToSystem:true
                //  eventToDebug:true 
                //  ELET:EventLogEntryType.Error),
                //соответственно вывод будет сделан на 
                //  консоль -   WriteConsoleLog(_clasMessage, _MessageText);
                //  в файл  -   WriteFileLog(_MessageText);
                //  в журнал сообщений виндоус - WriteEventLogApplication(_className, _MessageText, value.elet);
                //  в диагностическое окно VS - WriteOutputVSLog(_MessageText);


                if (value.EventToConsole) WriteConsoleLog(_clasMessage, dtNow.ToString("dd.MM.yyyy HH:mm:ss.fff")+" - "+_className + ": " + _MessageText, _CurX, _CurY, dtVizible);
                if (value.EventToFile) WriteFileLog(_className, dtNow, _MessageText);
                if (value.EventToSystem) WriteEventLogApplication(_className, _MessageText, value.elet);
                if (value.EventToDebug) WriteOutputVSLog(_MessageText);
            }



        }
        #endregion

        #region вывод на консоль
        private static void WriteConsoleLog(Direction clMes, string message, int curx, int cury, bool DateTimeNowVizible)
        {

            switch (clMes)
            {
                case Direction.Ok:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    //Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Direction.ERROR:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Direction.WARNING:
                    break;
                case Direction.OkStanMessage:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Direction.OkStanMessageNull:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.OkStan1s:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Direction.OkStanPassportRulona:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Direction.OkStanPerevalki:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                default:
                    break;
            }




            Console.SetCursorPosition(curx, cury);
            Console.Write(message);
            Console.ResetColor();
            
        }
        #endregion

        #region Вывод в журнал сообщений виндоус (HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog)
        //private static void WriteEventLog(string eventLogName, string sourceName, string message, EventLogEntryType type)
        private static void WriteEventLog(string sourceName, string message, EventLogEntryType type)
        {
            
            try
            {
                
                EventLog log = new EventLog { Log = eventLogName };
                if (string.IsNullOrEmpty(sourceName))
                {
                    sourceName = eventLogName;
                }

                log.Source = sourceName;
                try
                {
                    log.WriteEntry(message, type, 0, 0);
                    return;
                }
                catch(Exception e)
                {
                    string kmkg = e.Message;
                }

                ////Создание логера и источника
                //byte[] rawEventData = Encoding.ASCII.GetBytes("");
                //string keyName = @"SYSTEM\CurrentControlSet\Services\EventLog\" + eventLogName;
                //var rkEventSource = Registry.LocalMachine.OpenSubKey(keyName + @"\" + sourceName);

                //if (rkEventSource == null)
                //{
                //    rkEventSource = Registry.LocalMachine.CreateSubKey(keyName + @"\" + sourceName);
                //}

                //object eventMessageFile = rkEventSource.GetValue("EventMessageFile");
                //if (eventMessageFile == null)
                //{
                //    using (var dotNetFrameworkSettings = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NetFramework\"))
                //    {
                //        if (dotNetFrameworkSettings != null)
                //        {
                //            object dotNetInstallRoot = dotNetFrameworkSettings.GetValue("InstallRoot", null, RegistryValueOptions.None);

                //            if (dotNetInstallRoot != null)
                //            {
                //                string eventMessageFileLocation = dotNetInstallRoot.ToString() +
                //                        "v" +
                //                        System.Environment.Version.Major.ToString() + "." +
                //                        System.Environment.Version.Minor.ToString() + "." +
                //                        System.Environment.Version.Build.ToString() +
                //                        @"\EventLogMessages.dll";
                //                if (File.Exists(eventMessageFileLocation))
                //                {
                //                    rkEventSource = Registry.LocalMachine.OpenSubKey(keyName + @"\" + sourceName, true);
                //                    rkEventSource.SetValue("EventMessageFile", eventMessageFileLocation, RegistryValueKind.String);
                //                }

                //            }




                //        }

                //    }
                //}

                //rkEventSource.Close();
                //log.WriteEntry(message, type, 0, 0, rawEventData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static void WriteEventLogApplication(string sourceName, string message, EventLogEntryType type)
        {
            try
            {
                string Event = message;
                //string Source = "My App";
                string Source = sourceName;
                string Log = "Application";

                if (!EventLog.SourceExists(Source))
                    EventLog.CreateEventSource(Source, Log);

                using (EventLog eventLog = new EventLog(Log))
                {
                    eventLog.Source = Source;
                    eventLog.WriteEntry(Event, type);
                }
            }
            catch (Exception e)
            {

                string ex = e.Message;
            }
            
        }

        #endregion

        #region Вывод в файл
        private static void WriteFileLog(string classInMessage, DateTime dt,string message)
        {
            try
            {
                if (message!=""||message!=null||message!=" ")
                {
                    string tmptxt;
                    DateTime currenttime = dt;

                    if (message0 != message)
                    {

                        tmptxt = String.Format("{0:dd.MM.yyyy HH:mm:ss} {1}", currenttime, message);


                    }
                    else
                    {
                        tmptxt = String.Format("{0:dd.MM.yyyy HH:mm:ss} {1}", currenttime, "-----Повтор---- " + message);

                    }

                    //Если не удачно то записываем в локальный файл

                    string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log.txt";

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathProg, true))
                    {

                        file.WriteLine(tmptxt);
                        file.Close();
                    }

                    message0 = message;
                }
                
            }
            catch
            { }
        }
        
        #endregion

        #region Вывод сообщения в окно вывода VS
        static void WriteOutputVSLog(string messageL)
        {
            System.Diagnostics.Debug.WriteLine(messageL);
        }
        
        
        #endregion

    }



}
