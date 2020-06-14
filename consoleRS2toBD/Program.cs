using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace consoleRS2toBD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetWindowSize(200, 40);

            clStan1700  stan1700 = new clStan1700();
            Thread task1 = new Thread(stan1700.goStart);
            task1.Start();

            //clStanDress stanDs = new clStanDress();
            //Thread task2 = new Thread(stanDs.goStart);
            //task2.Start();

        }
    }
}
