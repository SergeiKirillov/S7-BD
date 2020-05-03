using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Diagnostics;

using HWDiag;

using System.Diagnostics.Eventing;

using LoggerInSystem;

using System.Threading;

using System.Runtime.InteropServices;


using System.Diagnostics;






namespace Stan_BD_prodave
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public class ListMessage
    {
        public DateTime dtMessage { get; set; }
        public string strMessage { get; set; }
        public string strBlok { get; set; }
    }
    
    public partial class MainWindow : Window
    {
        private Prodave stan;
        int Connect = 0;
        Timer TTimer100ms;
        Timer TTimerMessage;
        Timer TTimerSQL;
        Timer TTimer1s;

        DateTime dt1s = DateTime.Now;
        DateTime dtMessage = DateTime.Now;
        DateTime dtSQL = DateTime.Now;
        DateTime dt100ms = DateTime.Now;

        

        static byte[] buffer;
        static byte[] bufferPLC;
        static byte[] bufferSQL;
        static byte[] bufferMessage;
        static byte[] buffer1s;
        static readonly object locker = new object();
        static readonly object locker2 = new object();

        List<ListMessage> lstMessageApp = new List<ListMessage>();


        public MainWindow()
        {
            buffer = new byte[315];
            bufferPLC = new byte[315];
            bufferSQL = new byte[315];
            bufferMessage = new byte[22];
            buffer1s = new byte[315];

            

            InitializeComponent();
           


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            int result = stan.UnloadConnection(0);

            if (result == 0)
            {
                //Console.WriteLine("Connect close");
                LogSystem.WriteEventLog("ProDaveStan", "Test", "Close connection", EventLogEntryType.Information);
            }
            else
            {
                Console.WriteLine("Connect open. Error - " + stan.Error(result));
                LogSystem.WriteEventLog("ProDaveStan", "Test", "Connect open. Error - " + stan.Error(result), EventLogEntryType.Warning);
            }

            Console.ReadKey();
        }

        private void btnLogSave_Click(object sender, RoutedEventArgs e)
        {
            string logname = "ProDave";
            string sourceName = "Stan";
            if (!EventLog.SourceExists(sourceName))
            {
                var eventSourceData = new EventSourceCreationData(sourceName, logname);
                EventLog.CreateEventSource(eventSourceData);
            }
            EventLog mylog = new EventLog();
            mylog.Source = "Stan";

            mylog.WriteEntry("Writing to event log.");

        }

        private void btnLogSaveSatandart_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.EventLog applog = new System.Diagnostics.EventLog();
            applog.Source = "This Applicaton's Name";
            applog.WriteEntry("An entry to the Application event log.");

        }

        private void btnLogSaveClassLogger_Click(object sender, RoutedEventArgs e)
        {
            LogSystem.WriteEventLog("S7-BD(LPC2)", "Stan-BD-Prodave", "test message", EventLogEntryType.Information);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO запуск таймеров 100ms(100ms), 101ms(SQL), 200ms(message), 1000ms(1s)
            
            stan = new Prodave();
            byte[] conn = new byte[] { 192, 168, 0, 11 };

            int res = stan.LoadConnection(Connect, 2, conn, 3, 0);

            if (res != 0)
            {
                Console.WriteLine("Error connection! " + stan.Error(res));
                LogSystem.WriteEventLog("ProDaveStan", "Test", "Error connection!. Error - " + stan.Error(res), EventLogEntryType.Error);
                
            }
            else
            {
                LogSystem.WriteEventLog("ProDaveStan", "Test", "Connect OK!", EventLogEntryType.Information);

                int resSAC = stan.SetActiveConnection(Connect);
                if (resSAC == 0)
                {
                    Console.WriteLine("Соединение активно.");
                    LogSystem.WriteEventLog("ProDaveStan", "Test", "Соединение активно.", EventLogEntryType.Information);

                    

                    //Connect100ms();
                    TTimer100ms = new Timer(new TimerCallback(TicTimer100ms), null, 0, 100);

                    TTimerMessage = new Timer(new TimerCallback(TicTimerMessage), null, 0, 200);
                    TTimerSQL = new Timer(new TimerCallback(TicTimerSQL), null, 0, 101);
                    TTimer1s = new Timer(new TimerCallback(TicTimer1s), null, 0, 1000);





                }
                else
                {
                    Console.WriteLine("Соединение не активировано. " + stan.Error(resSAC));
                    LogSystem.WriteEventLog("ProDaveStan", "Test", "Соединение не активировано. " + stan.Error(resSAC), EventLogEntryType.Error);
                    System.Diagnostics.Debug.WriteLine("Error - Соединение не активировано.");

                }

            }


        }

        #region 100ms

        private void TicTimer100ms(object state)
        {
            Connect100ms();
        }
        private void Connect100ms()
        {
            short[] buffer_array = new short[158];
            
            int amount = 315;

            
            //var buffer2 = new ushort[128];
            int Byte_Col_r = 0;

            int resultReadField = stan.field_read('M', 0, 3000, amount, out buffer, out Byte_Col_r);
            if (resultReadField == 0)
            {
                //Console.WriteLine("OK.Read fied M3000-M3315");
                //System.Diagnostics.Debug.WriteLine("OK.Read fied M3000-M3315");

                //Console.WriteLine(string.Format("\t\t {0}  ({1})  {2}", "Message 100mc", DateTime.Now - dt100ms, Thread.CurrentThread.ManagedThreadId));
                dt100ms = DateTime.Now;


                //Todo 100ms Далее передаем данные в критическую сецкию из которой будут забирать потоки каждый в свой момент

                //Console.WriteLine("Byte_Col_r = " + Byte_Col_r);

                Console.WriteLine("Скорость 1(in) = " + BitConverter.ToInt16(buffer, 0));
                //Console.WriteLine("Скорость 2 = " + BitConverter.ToInt16(buffer, 2));
                //Console.WriteLine("Скорость 3 = " + BitConverter.ToInt16(buffer, 4));
                //Console.WriteLine("Скорость 4 = " + BitConverter.ToInt16(buffer, 6));
                //Console.WriteLine("Скорость 5 = " + BitConverter.ToInt16(buffer, 8));

                //Console.WriteLine("Толщина 1 = " + BitConverter.ToInt16(buffer, 10));
                //Console.WriteLine("Толщина 5 = " + BitConverter.ToInt16(buffer, 12));

                //Console.WriteLine("Ширина = " + BitConverter.ToInt16(buffer, 14));
                //Console.WriteLine("Диаметр выпуска = " + BitConverter.ToInt16(buffer, 16));
                //Console.WriteLine("Диаметр разматывателя = " + BitConverter.ToInt16(buffer, 18));
                //Console.WriteLine("Диаметр моталки = " + BitConverter.ToInt16(buffer, 20));


                Thread PLS100ms = new Thread(BufferToBuffer);
                PLS100ms.Start();
                //new Thread(BufferSQLToBufferPLS).Start();


            }
            else
            {
                Console.WriteLine("Error.Read fied M3000-M3315. " + stan.Error(resultReadField));
                System.Diagnostics.Debug.WriteLine("Error.Read fied M3000-M3315. " + stan.Error(resultReadField));
                LogSystem.WriteEventLog("ProDaveStan", "Test", "Соединение не активировано. " + stan.Error(resultReadField), EventLogEntryType.Error);
            }


            
        }
        #endregion

        #region Блок критичных секций

        static void BufferToBuffer()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                bufferPLC = buffer;
            }

        }

        static void BufferSQLToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                bufferSQL = bufferPLC;
            }

        }

        static void BufferMessageToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                bufferMessage[0] = bufferPLC[66];
                bufferMessage[1] = bufferPLC[67];
                bufferMessage[2] = bufferPLC[68];
                bufferMessage[3] = bufferPLC[69];
                bufferMessage[4] = bufferPLC[70];
                bufferMessage[5] = bufferPLC[71];
                bufferMessage[6] = bufferPLC[102];
                bufferMessage[7] = bufferPLC[103];
                bufferMessage[8] = bufferPLC[104];
                bufferMessage[9] = bufferPLC[105];
                bufferMessage[10] = bufferPLC[106];
                bufferMessage[11] = bufferPLC[107];
                bufferMessage[12] = bufferPLC[108];
                bufferMessage[13] = bufferPLC[109];
                bufferMessage[14] = bufferPLC[110];
                bufferMessage[15] = bufferPLC[111];
                bufferMessage[16] = bufferPLC[6];
                bufferMessage[17] = bufferPLC[7];
                bufferMessage[18] = bufferPLC[312];
                bufferMessage[19] = bufferPLC[313];
                bufferMessage[20] = bufferPLC[310];
                bufferMessage[21] = bufferPLC[311];
                
            }

        }
        static void Buffer1sToBufferPLC()
        {
            //критичная секция которая записывает значение в bufferPLC
            lock (locker)
            {
                buffer1s = bufferPLC;
            }

        }

        #endregion



        #region 1s

        private void TicTimer1s(object state)
        {
           this.Dispatcher.Invoke((Action)this.UpdateForm1s); //выполняем в том же потоке.
        }

        private void UpdateForm1s()
        {
            // Console.WriteLine(string.Format("\t\t {0} ({1}) {2}","Расчет 1c", DateTime.Now - dt1s, Thread.CurrentThread.ManagedThreadId));
            dt1s = DateTime.Now;
            Thread t1s = new Thread(Buffer1sToBufferPLC);
            t1s.Start();
            Console.WriteLine("Скорость 1(1s)  = " + BitConverter.ToInt16(buffer1s, 0));
            lbl1s.Content = BitConverter.ToInt16(buffer1s, 0);

            //lstMessageApp.Add(new ListMessage { dtMessage = DateTime.Now, strBlok = "Скорость 1кл(1s)=" + BitConverter.ToInt16(buffer1s, 0), strMessage="1 sec"});

            //lstStatus.ItemsSource = lstMessageApp;

            lstStatus.Items.Add(DateTime.Now + " - Скорость 1кл(1s) = " + BitConverter.ToInt16(buffer1s, 0));
        }
        #endregion

        #region SQL
        private void TicTimerSQL(object state)
        {
            this.Dispatcher.Invoke((Action)this.UpdateFormSQL); //выполняем в том же потоке.
        }

        private void UpdateFormSQL()
        {
            //Console.WriteLine(string.Format("\t\t {0} ({1}) {2}", "SQL 101mc", DateTime.Now - dtSQL, Thread.CurrentThread.ManagedThreadId));
            dtSQL = DateTime.Now;

            Thread tSQL = new Thread(BufferSQLToBufferPLC);
            tSQL.Start();


            Console.WriteLine("Скорость 1(SQL)  = " + BitConverter.ToInt16(bufferSQL, 0));
            
        }
        #endregion



        #region Messages
        private void TicTimerMessage(object state)
        {
            this.Dispatcher.Invoke((Action)this.UpdateFormMessage); //выполняем в том же потоке.
        }

        private void UpdateFormMessage()
        {
            //Console.WriteLine(string.Format("\t\t {0}  ({1})  {2}", "Message 200mc", DateTime.Now - dtMessage, Thread.CurrentThread.ManagedThreadId));
            dtMessage = DateTime.Now;

            Thread tMessage = new Thread(BufferMessageToBufferPLC);
            tMessage.Start();
            //Console.WriteLine("Скорость 1(Message)  = " + BitConverter.ToInt16(bufferMessage, 0));


        }
        #endregion



    }
}
