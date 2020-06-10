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

            Thread task1 = new Thread(clStan1700.goStart);
            task1.Start();

            Thread task2 = new Thread(clStanDress.goStart);
            task2.Start();

        }
    }
}
