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
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

using Sharp7;

using System.Data.SqlClient;



namespace s7stan_BD
{
    /// <summary>
    /// Логика взаимодействия для MainStan.xaml
    /// </summary>
    public partial class MainStan : Window
    {
        private S7Client clientStan;
        private byte[] Buffer = new byte[65536];
        enum ClientStatus { cStopped, cRunning, cChannelError, cDataError};
        ClientStatus StatusStan;

        Timer TTimer100ms = null;
        Timer TTimer200ms = null;
        Timer TTimer1s = null;

        DateTime Time100ms;
        DateTime Time1s;

        float speed4kl, H5_work, h5w, Bw, D_tek_mot, B_Work, D_pred_mot = 0, Ves_Work, Dlina_Work;
        DateTime Time_Start, Time_Stop, DT;

        string strConnect = @"server=192.168.0.46;uid=report;pwd=159951;database=stanm";
        SqlConnection SQLCon;
        SqlCommand SQLCom;

        

        public MainStan()
        {
            InitializeComponent();

            clientStan = new S7Client();
            StatusStan = ClientStatus.cStopped;

        }

        
        private void Start()
        {
            // Console.WriteLine("Start");

           

            taskBarItem.ProgressValue = 100;
            taskBarItem.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            if (Connect() == true)
            {
                SQLCon = new SqlConnection();
                SQLCon.ConnectionString = strConnect;
                if (SQLCon.State == System.Data.ConnectionState.Open)
                {
                    SQLCon.Close();
                }
                SQLCon.Open();

                RunMode();
            }
            else
            {
                Stop();
            }

              
          
        }

        private bool Connect()
        {
            int ResultStan = clientStan.ConnectTo("192.168.0.11", 0, 3);
            if (ResultStan==0)
            {
                StatusStan = ClientStatus.cRunning;

                
            }
            else
            {
                StatusStan = ClientStatus.cChannelError;
                clientStan.Disconnect();
                
            }
            ShowResult(ResultStan);
            return ResultStan==0;

        }

        private void ShowResult(int resultStan)
        {
            try
            {
                label1.Content = clientStan.ErrorText(resultStan);

                if (resultStan == 0)
                {
                    label1.Content = label1.Content + " ( ExecutionTime=" + clientStan.ExecutionTime.ToString() + " ms)";
                }

                switch (StatusStan)
                {
                    case ClientStatus.cStopped:
                        label3.Content = "Stopped";
                        break;
                    case ClientStatus.cRunning:
                        label3.Content = "Runner";
                        break;
                    case ClientStatus.cChannelError:
                        label3.Content = "ChannelError";

                        break;
                    case ClientStatus.cDataError:
                        label3.Content = "DataError";
                        break;
                    default:
                        break;
                }
            }
            catch (System.InvalidOperationException)
            {

                Console.WriteLine("Error");
                EditMode();
            }
            
        }

        private void Stop()
        {
            //Console.WriteLine("Stop");


            taskBarItem.ProgressValue = 100;
            taskBarItem.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Paused;

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            if (StatusStan == ClientStatus.cChannelError)
            {
                StatusStan = ClientStatus.cChannelError;
                ShowResult(clientStan.Disconnect());
            }
            else
            {
                StatusStan = ClientStatus.cStopped;
                ShowResult(clientStan.Disconnect());
                EditMode();
            }

            SQLCon.Close();

            
        }


        

        private void Start_Click(object sender, EventArgs e)
        {
            Start();
            //RunMode();
            
        }

        private void btnTestSQLWrite_Click(object sender, RoutedEventArgs e)
        {
                SQLCon = new SqlConnection();
                SQLCon.ConnectionString = strConnect;
                if (SQLCon.State == System.Data.ConnectionState.Open)
                {
                    SQLCon.Close();
                }
                SQLCon.Open();

            string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //string dtNow = DateTime.Now.ToString("s");
            //string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            

            string sql_1secQuery = "INSERT INTO stanm_proiz (dtcontrollerstart) VALUES ('" + dtNow +"')";
            Console.WriteLine(sql_1secQuery);

            SQLCom = new SqlCommand(sql_1secQuery, SQLCon);
            int number = SQLCom.ExecuteNonQuery();
            Console.WriteLine("Добавлено объектов {0}", number);
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Stop();

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Start();
            //RunMode();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void EditMode()
        {
            StatusStan = ClientStatus.cStopped;

            // Таймер остановлен
            TTimer100ms.Change(Timeout.Infinite, Timeout.Infinite);
            TTimer1s.Change(Timeout.Infinite, Timeout.Infinite);
            TTimer200ms.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void RunMode()
        {
            MessageBox.Show("при задержке менеe 200 мс вылетает error");
            // Таймер запущен
            TTimer100ms = new Timer(new TimerCallback(TicTimer100ms), null, 0, 100);
            //TTimer1s = new Timer(new TimerCallback(TicTimer1s), null, 0, 1000);

            //TTimer200ms = new Timer(new TimerCallback(TicTimer200ms), null, 0, 1000);


        }

        private bool CheckConnecton()
        {
            if (StatusStan == ClientStatus.cChannelError)
            {
                clientStan.Disconnect();
                return Connect();
            }
            else
                return true;
        }

        private void TicTimer1s(object state)
        {

            Stopwatch stopwatch1s = new Stopwatch();
            stopwatch1s.Start();
            
            if (CheckConnecton())
            {
                if (Execute1s())
                {
                    //Console.WriteLine("1s - OK");
                    
                }
                else
                {
                    Console.WriteLine("1s - Error ");
                }
            }

            TimeSpan Difference1s = DateTime.UtcNow.Subtract(Time1s);
            //Время между циклами таймера
            //Console.WriteLine("1s "+ Thread.CurrentThread.ManagedThreadId.ToString()+ " ----- " + Difference1s.Milliseconds.ToString());
            Time1s = DateTime.UtcNow;

            stopwatch1s.Stop();
            TimeSpan ts = stopwatch1s.Elapsed;
            string elapsedTime = String.Format("{0:00}", ts.Milliseconds);
            //Время выполнения команд внутри TicTimer1s
            //Console.WriteLine("Runtime 1s - "+ elapsedTime);



        }

        private void TicTimer100ms(object state)
        {
            Stopwatch stopwatch100ms = new Stopwatch();
            stopwatch100ms.Start();

            if (CheckConnecton())
            {
                
               Execute100ms();
            }

            TimeSpan Difference100ms = DateTime.UtcNow.Subtract(Time100ms);
            //Время между циклами таймера
            //Console.WriteLine("100ms " + Thread.CurrentThread.ManagedThreadId.ToString() + " ----- " + Difference100ms.Milliseconds.ToString());
            Time100ms = DateTime.UtcNow;

            stopwatch100ms.Stop();
            TimeSpan ts = stopwatch100ms.Elapsed;
            string elapsedTime = String.Format("{0:00}", ts.Milliseconds);
            //Время выполнения команд внутри TicTimer1s
            //Console.WriteLine("Runtime 100ms - " + elapsedTime);
        }


        private void Execute100ms()
        {
            //Console.WriteLine("100ms");
            //DateTime DT100ms = new DateTime();
            //if (clientStan.GetPlcDateTime(ref DT100ms) == 0)
            //{
            //    //Console.WriteLine("Текущее время PLC -> " + DT.ToLongDateString() + " - " + DT.ToLongTimeString());
            //    //Console.WriteLine("Текущее время PLC -> " + DT.ToShortDateString() + " - " + DT.ToLongTimeString());
            //    Console.WriteLine("Текущее время 100ms PLC -> " + DT100ms.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //}
            #region чтение меркерной области с 30000 длинной 315
            var buffer = new byte[315];
            int readMW3000 = clientStan.MBRead(3000, buffer.Length, buffer);
            if (readMW3000 == 0)
            {
                //Console.WriteLine("readDB100 read ok");

                //float db100dbb82_s4 = S7.GetRealAt(buffer, 0);
                //float db100dbb86_s5 = S7.GetRealAt(buffer, 4);
                //float db100dbb90_s3 = S7.GetRealAt(buffer, 8);
                //float db100dbb94_s2 = S7.GetRealAt(buffer, 12);
                //speed4kl = db100dbb82_s4;
                //float db100dbb98_s1 = S7.GetRealAt(buffer, 16);

                ////Console.WriteLine("1 = " + db100dbb98_s1.ToString("F"));
                ////Console.WriteLine("2 = " + db100dbb94_s2.ToString("F"));
                ////Console.WriteLine("3 = " + db100dbb90_s3.ToString("F"));
                ////Console.WriteLine("4 = " + db100dbb82_s4.ToString("F"));
                ////Console.WriteLine("5 = " + db100dbb86_s5.ToString("F"));

                ////System.Diagnostics.Trace.WriteLine("4 кл = " + speed4kl.ToString("F"));
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +" readMW30000 read error");
                //System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + " -- DB100 read error");
                
            }

            #endregion


        }

        private bool Execute1s()
        {
            //Console.WriteLine("1s");
            DT = new DateTime();
            if (clientStan.GetPlcDateTime(ref DT) == 0)
            {
                //Console.WriteLine("Текущее время PLC -> " + DT.ToLongDateString() + " - " + DT.ToLongTimeString());
                //Console.WriteLine("Текущее время PLC -> " + DT.ToShortDateString() + " - " + DT.ToLongTimeString());
                Console.WriteLine("Текущее время PLC -> " + DT.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }

            #region DB100
            var buffer = new byte[20];
            int readDB100 = clientStan.DBRead(100, 82, buffer.Length, buffer);
            if (readDB100 == 0)
            {
                //Console.WriteLine("readDB100 read ok");

                float db100dbb82_s4 = S7.GetRealAt(buffer, 0);
                float db100dbb86_s5 = S7.GetRealAt(buffer, 4);
                float db100dbb90_s3 = S7.GetRealAt(buffer, 8);
                float db100dbb94_s2 = S7.GetRealAt(buffer, 12);
                speed4kl = db100dbb82_s4;
                float db100dbb98_s1 = S7.GetRealAt(buffer, 16);

                //Console.WriteLine("1 = " + db100dbb98_s1.ToString("F"));
                //Console.WriteLine("2 = " + db100dbb94_s2.ToString("F"));
                //Console.WriteLine("3 = " + db100dbb90_s3.ToString("F"));
                //Console.WriteLine("4 = " + db100dbb82_s4.ToString("F"));
                //Console.WriteLine("5 = " + db100dbb86_s5.ToString("F"));

                //System.Diagnostics.Trace.WriteLine("4 кл = " + speed4kl.ToString("F"));
            }
            else
            {
                Console.WriteLine("readDB100 read error");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString()+" -- DB100 read error");
                return false;
            }
            #endregion

            #region DB101
            var bufferDB101 = new byte[4];
            int readDB101 = clientStan.DBRead(101, 4, bufferDB101.Length, bufferDB101);
            if (readDB101 == 0)
            {
                //Console.WriteLine("readDB101 read ok");

                float db101dbd4_h5Work = S7.GetRealAt(bufferDB101, 0);
                h5w = db101dbd4_h5Work;
                // Console.WriteLine("H5 work = " + db101dbd4_h5Work.ToString("F"));

                //System.Diagnostics.Trace.WriteLine("H5 work = " + H5_work.ToString("F"));
            }
            else
            {
                Console.WriteLine("readDB101 read error");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + " -- DB101 read error");
                return false;
            }
            #endregion

            #region MD316
            var bufferMD316 = new byte[4];
            int readMD316 = clientStan.MBRead(316, bufferMD316.Length, bufferMD316);
            if (readMD316 == 0)
            {
                //Console.WriteLine("readMD316 read ok");

                float md316_D_tek_mot = S7.GetRealAt(bufferMD316, 0);
                D_tek_mot = md316_D_tek_mot;
                //Console.WriteLine("H5 work = " + md316_D_tek_mot.ToString("F"));

                //System.Diagnostics.Trace.WriteLine("D_tek_mot= " + D_tek_mot.ToString("F"));
            }
            else
            {
                Console.WriteLine("readMD316 read error");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + " -- MD316 read error");
                return false;
            }
            #endregion

            #region DB105
            var bufferDB105 = new byte[4];
            int readDB105 = clientStan.DBRead(105, 20, bufferDB105.Length, bufferDB105);
            if (readDB105 == 0)
            {
                //Console.WriteLine("readDB105 read ok");

                float db105dbd20_B_Work = S7.GetRealAt(bufferDB105, 0);
                Bw = db105dbd20_B_Work;
                //Console.WriteLine("H5 work = " + db105dbd20_B_Work.ToString("F"));

                //System.Diagnostics.Trace.WriteLine("B work = " + B_Work.ToString("F"));
            }
            else
            {
                Console.WriteLine("readDB105 read error");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + " -- DB105 read error");
                return false;
            }
            #endregion

            #region Расчет параметров прокатанного рулона после окончания прокатки

            if (D_tek_mot>D_pred_mot)
            {
                if (D_pred_mot<0.615) 
                {
                    Time_Start = DT;
                }
            }

            if ((Time_Start != new DateTime()) && (H5_work==0) && (D_tek_mot>0.7) && (speed4kl>2))
            {
                H5_work = h5w;
                B_Work = Bw;
            }

            //Console.WriteLine("Time_Start="+ Time_Start.ToString());
            //Console.WriteLine("Time_Stop=" + Time_Stop.ToString());
            //Console.WriteLine("D_tek_mot=" + D_tek_mot);
            //Console.WriteLine("Speed 4kl=" + speed4kl);
            //Console.WriteLine("H5_work=" + H5_work);
            //Console.WriteLine("B_Work=" + B_Work);



            if ((Time_Start != new DateTime()) && (H5_work != 0) && (D_tek_mot < 0.610) && (D_tek_mot < D_pred_mot))
            {
               
                Ves_Work = (((((D_pred_mot * D_pred_mot)-0.36F)*3.141593F)/4)*(B_Work/1000))*7.85F;
                Time_Stop = DT;
                
                Dlina_Work = ((Ves_Work / 7.85F)/(B_Work / 1000))/(H5_work/1000);


                //System.Diagnostics.Trace.WriteLine("Начало прокатки рулона = " + Time_Start.ToString());
                //System.Diagnostics.Trace.WriteLine("Окончание прокатки рулона = " + Time_Stop.ToString());
                //System.Diagnostics.Trace.WriteLine("Bес рулона = " + Ves_Work.ToString("F"));
                //System.Diagnostics.Trace.WriteLine("Длинна рулона = " + Dlina_Work.ToString("F"));


                #region Передаем в Базу Данных

                //yyyy - MM - dd HH: mm: ss.fff
                string strTimeStart = Time_Start.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string strTimeStop = Time_Stop.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string sql_1secQuery = "INSERT INTO stanm_proiz (dtcontrollerstart, dtcontrollerstop, ves, dlinna, h5, b, speed4kl) VALUES ('" + strTimeStart + "', '" + strTimeStop + "', " + Ves_Work + ", " + Dlina_Work + ", " + H5_work + ", " + B_Work + ", " + speed4kl + ")";
                Console.WriteLine(sql_1secQuery);

                SQLCom = new SqlCommand(sql_1secQuery, SQLCon);
                int number = SQLCom.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов {0}", number);

                #endregion



                H5_work = 0; B_Work = 0; Ves_Work = 0; Dlina_Work = 0;
                Time_Start = new DateTime();
                Time_Stop = new DateTime();

            }

            D_pred_mot = D_tek_mot;

            #endregion

            return true;


        }


        private void TicTimer200ms(object state)
        {
            Stopwatch stopwatch200ms = new Stopwatch();
            stopwatch200ms.Start();

            if (CheckConnecton())
            {

                Execute200ms();
            }

            TimeSpan Difference100ms = DateTime.UtcNow.Subtract(Time100ms);
            //Время между циклами таймера
            //Console.WriteLine("100ms " + Thread.CurrentThread.ManagedThreadId.ToString() + " ----- " + Difference100ms.Milliseconds.ToString());
            Time100ms = DateTime.UtcNow;

            stopwatch200ms.Stop();
            TimeSpan ts = stopwatch200ms.Elapsed;
            string elapsedTime = String.Format("{0:00}", ts.Milliseconds);
            //Время выполнения команд внутри TicTimer1s
            //Console.WriteLine("Runtime 100ms - " + elapsedTime);
        }

        private void Execute200ms()
        {
            Console.WriteLine("200ms");

            #region MD316
            var bufferMD316 = new byte[4];
            int readMD316 = clientStan.MBRead(316, bufferMD316.Length, bufferMD316);
            if (readMD316 == 0)
            {
                //Console.WriteLine("readMD316 read ok");

                float md316_D_tek_mot = S7.GetRealAt(bufferMD316, 0);
               //Console.WriteLine("H5 work = " + md316_D_tek_mot.ToString("F"));

                //System.Diagnostics.Trace.WriteLine("D_tek_mot= " + D_tek_mot.ToString("F"));
            }
            else
            {
                Console.WriteLine("readMD316 read error");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToString() + " -- MD316 read error");
                
            }
            #endregion


        }
    }
}
