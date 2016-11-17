using Data.Data;
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
            LoadForTest(data);
            
            var test = data.Colonist;

            WriteLine("starting Colonists status");
            test.Print();

            WriteLine("\nmoving");
            for (int i = 0, n = new Random().Next(10, 25); i < n; ++i)
            {
                WriteLine("\tTick {0}", i);
                test.Work();
                test.Print();
            }

            ReadKey(true);
        }

        private static void LoadForTest(DataContainer data)
        {
            char c = 'A';
            for (int i = 0; i < 5; ++i)
                data.Add(new Colonist((c++).ToString(), new Position(10, 10)));
        }
    }
}
