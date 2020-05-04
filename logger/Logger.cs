using System;

using System.Diagnostics;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Xml;
using System.Threading.Tasks;


namespace LoggerInSystem
{
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
        //Запись в системный журнал приложений
        /// <param name="eventLogName">Название лога</param>
        /// <param name="sourceName">Название источника</param>
        /// <param name="message">Сообщение</param>
        /// <param name="type">Тип сообщения</param>
        /// 

        static string eventLogName = "ProDaveStan";

        #region public метод который распределяет что куда писать в зависимости от переданного классаСообщения

        public static void Write(string _className, Direction _clasMessage, string _MessageText)
        {

            switch (_clasMessage)
            {

                case Direction.ERROR:
                    WriteConsoleLog(_clasMessage, _MessageText);
                    WriteFileLog(_MessageText);
                    WriteEventLog(_className, _MessageText, EventLogEntryType.Error);

                    break;
                case Direction.WARNING:
                    WriteConsoleLog(_clasMessage, _MessageText);
                    WriteFileLog(_MessageText);
                    WriteEventLog(_className, _MessageText, EventLogEntryType.Warning);
                    break;

                case Direction.Ok:
                    WriteConsoleLog(_clasMessage, _MessageText);
                    WriteFileLog(_MessageText);
                    WriteEventLog(_className, _MessageText, EventLogEntryType.Information);
                    break;
                case Direction.OkStanMessage:
                    WriteConsoleLog(_clasMessage, _MessageText);
                    break;
                case Direction.OkStanMessageNull:
                    WriteConsoleLog(_clasMessage, _MessageText);
                    break;
                case Direction.OkStan1s:
                    WriteConsoleLog(_clasMessage, _MessageText);
                    break;
                case Direction.OkStanPassportRulona:
                    WriteConsoleLog(_clasMessage, _MessageText);
                    break;
                case Direction.OkStanPerevalki:
                    WriteConsoleLog(_clasMessage, _MessageText);
                    break;
                default:
                    break;
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
