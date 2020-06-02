using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace RS2toBDconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassStan clStan;

            Console.WriteLine("Для выбора стана нажмите - s");
            Console.WriteLine("     Для запуска стана нажмите - s");
            Console.WriteLine("     Для остановки стана нажмите - p");

            if (ConsoleKey.S == Console.ReadKey().Key)
            {
                if (Console.ReadKey().Key == ConsoleKey.S)
                {
                    clStan = new ClassStan(stan101ms: true, stan1s: false, stan200ms: false, NetSend: false);
                    Thread StanThreadMain = new Thread(new ThreadStart(clStan.Start));
                    StanThreadMain.Start();

                    
                }

                if (Console.ReadKey().Key == ConsoleKey.P)
                {
                    clStan = new ClassStan(stan101ms: false, stan1s: false, stan200ms: false, NetSend: false);
                    clStan.Stop();
                }

            }

            Console.ReadKey();


        }
    }
}
