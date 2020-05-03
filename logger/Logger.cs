using System;

using System.Diagnostics;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Xml;

namespace LoggerInSystem
{
    public enum Direction
    {
        ERROR,
        WARNING,
        StanMessage,
        StanMessageNull,
        Stan1s,
        StanPassportRulona,
        StanPerevalki
    }

    public class LogSystem
    {
        //Запись в системный журнал приложений
        /// <param name="eventLogName">Название лога</param>
        /// <param name="sourceName">Название источника</param>
        /// <param name="message">Сообщение</param>
        /// <param name="type">Тип сообщения</param>
        /// 
       

        public static void WriteConsoleLog(Direction clMes, string message)
        {
            switch (clMes)
            {
                case Direction.ERROR:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.WARNING:
                    break;
                case Direction.StanMessage:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.StanMessageNull:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.Stan1s:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Direction.StanPassportRulona:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Direction.StanPerevalki:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                default:
                    break;
            }
            
            Console.WriteLine(message);
            Console.ResetColor();
        }

        
        public static void WriteEventLog(string eventLogName, string sourceName, string message, EventLogEntryType type)
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
                catch
                {
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

                            if (dotNetInstallRoot!=null)
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

       
    }

    

}
