using System;

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

        public classEvents(bool eventToConsole, bool eventToFile, bool eventToSystem, bool eventToDebug)
        {
            EventToConsole = eventToConsole;
            EventToFile = eventToFile;
            EventToSystem = eventToSystem;
            EventToDebug = eventToDebug;
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
        
        static string eventLogName = "ProDaveStan";

        static Dictionary<Direction, classEvents> dicEvent = new Dictionary<Direction, classEvents>()
        {
            [Direction.ERROR] = new classEvents(eventToConsole:true, eventToFile:true, eventToSystem:true, eventToDebug:true),
            [Direction.WARNING] = new classEvents(eventToConsole: true, eventToFile: true, eventToSystem: true, eventToDebug: true),
            [Direction.Ok] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: true, eventToDebug: false),
            [Direction.OkStan1s] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false),
            [Direction.OkStanMessage] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false),
            [Direction.OkStanMessageNull] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false),
            [Direction.OkStanPassportRulona] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false),
            [Direction.OkStanPerevalki] = new classEvents(eventToConsole: true, eventToFile: false, eventToSystem: false, eventToDebug: false),
            
        };


        #region public метод который распределяет что куда писать в зависимости от переданного классаСообщения

        public static void Write(string _className, Direction _clasMessage, string _MessageText)
        {
            classEvents value;
            if (dicEvent.TryGetValue(_clasMessage, out value))
            {
                
                if (value.EventToConsole) WriteConsoleLog(_clasMessage, _MessageText);
                if (value.EventToFile) WriteFileLog(_MessageText);
                if (value.EventToSystem) WriteEventLog(_className, _MessageText, _clasMessage);
                if (value.EventToDebug) WriteOutputVSLog(_MessageText);
            }
            
           

        }

        #endregion
                
        #region вывод на консоль
        private static void WriteConsoleLog(Direction clMes, string message)
        {
            switch (clMes)
            {
                case Direction.Ok:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Direction.ERROR:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.WARNING:
                    break;
                case Direction.OkStanMessage:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.OkStanMessageNull:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.OkStan1s:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.OkStanPassportRulona:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Direction.OkStanPerevalki:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                default:
                    break;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }
        #endregion

        #region Вывод в журнал сообщений виндоус
        //private static void WriteEventLog(string eventLogName, string sourceName, string message, EventLogEntryType type)
        private static void WriteEventLog(string sourceName, string message, Direction clasMessage)
        {

            try
            {

                EventLogEntryType type;
                switch (clasMessage)
                {
                    case Direction.Ok:
                        type = EventLogEntryType.SuccessAudit;
                        break;
                    case Direction.ERROR:
                        type = EventLogEntryType.Error;
                        break;
                    case Direction.WARNING:
                        type = EventLogEntryType.Warning;
                        break;
                    case Direction.OkStanMessage:
                        type = EventLogEntryType.Information;
                        break;
                    case Direction.OkStanMessageNull:
                        type = EventLogEntryType.Information;
                        break;
                    case Direction.OkStan1s:
                        type = EventLogEntryType.Information;
                        break;
                    case Direction.OkStanPassportRulona:
                        type = EventLogEntryType.Information;
                        break;
                    case Direction.OkStanPerevalki:
                        type = EventLogEntryType.Information;
                        break;
                    default:
                        type = EventLogEntryType.FailureAudit;
                        break;
                }

                


                EventLog log = new EventLog { Log = eventLogName };
                if (string.IsNullOrEmpty(sourceName))
                {
                    sourceName = eventLogName;
                }

                log.Source = eventLogName;
                try
                {
                    log.WriteEntry(message, type, 0, 0);
                    return;
                }
                catch(Exception e)
                {
                    string kmkg = e.Message;
                }

                //Создание логера и источника
                byte[] rawEventData = Encoding.ASCII.GetBytes("");
                string keyName = @"SYSTEM\CurrentControlSet\Services\EventLog\" + eventLogName;
                var rkEventSource = Registry.LocalMachine.OpenSubKey(keyName + @"\" + sourceName);

                if (rkEventSource == null)
                {
                    rkEventSource = Registry.LocalMachine.CreateSubKey(keyName + @"\" + sourceName);
                }

                object eventMessageFile = rkEventSource.GetValue("EventMessageFile");
                if (eventMessageFile == null)
                {
                    using (var dotNetFrameworkSettings = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NetFramework\"))
                    {
                        if (dotNetFrameworkSettings != null)
                        {
                            object dotNetInstallRoot = dotNetFrameworkSettings.GetValue("InstallRoot", null, RegistryValueOptions.None);

                            if (dotNetInstallRoot != null)
                            {
                                string eventMessageFileLocation = dotNetInstallRoot.ToString() +
                                        "v" +
                                        System.Environment.Version.Major.ToString() + "." +
                                        System.Environment.Version.Minor.ToString() + "." +
                                        System.Environment.Version.Build.ToString() +
                                        @"\EventLogMessages.dll";
                                if (File.Exists(eventMessageFileLocation))
                                {
                                    rkEventSource = Registry.LocalMachine.OpenSubKey(keyName + @"\" + sourceName, true);
                                    rkEventSource.SetValue("EventMessageFile", eventMessageFileLocation, RegistryValueKind.String);
                                }

                            }




                        }

                    }
                }

                rkEventSource.Close();
                log.WriteEntry(message, type, 0, 0, rawEventData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        


        
        #endregion

        #region Вывод в файл
        private static void WriteFileLog(string message)
        {
            try
            {
                //Если не удачно то записываем в локальный файл
                DateTime currenttime = DateTime.Now;
                string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log.txt";

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathProg, true))
                {
                    string tmptxt = String.Format("{0:dd.MM.yyyy HH:mm:ss} {1}", currenttime, message);
                    file.WriteLine(tmptxt);
                    file.Close();
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
