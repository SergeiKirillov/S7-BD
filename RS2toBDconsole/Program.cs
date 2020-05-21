using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS2toBDconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassStan clStan = new ClassStan(stan101ms: true, stan1s: true, stan200ms: true, NetSend: false);
            clStan.Start();
            Console.ReadKey();
        }
    }
}
