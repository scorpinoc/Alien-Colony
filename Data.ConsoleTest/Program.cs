using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Data.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new DataContainer();
            data.LoadForTest();

            var test = data.Colonist;

            WriteLine("starting status");
            test.Print();

            WriteLine("moving");
            for (int i = 0, n = new Random().Next(10, 20); i < n; ++i)
            {
                WriteLine("\tTick {0}", i);
                test.Work();
                test.Print();
            }

            ReadKey(true);
        }
    }
}
