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
                        
            clStan = new ClassStan(stan101ms: true, stan1s: false, stan200ms: false, NetSend: false);
            Thread StanThreadMain = new Thread(new ThreadStart(clStan.Start));
            StanThreadMain.Start();

            //clStan = new ClassStan(stan101ms: false, stan1s: false, stan200ms: false, NetSend: false);
            //clStan.Stop();

            ClassDresStan clDsStan;
            clDsStan = new ClassDresStan(ds101ms: false, ds1s:false, ds200ms:false, NetSend:false);
            Thread DressStanThreadMain = new Thread(new ThreadStart(clDsStan.Start));
            DressStanThreadMain.Start();


            Console.ReadKey();


        }
    }
}
