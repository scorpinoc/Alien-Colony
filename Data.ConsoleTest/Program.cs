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
            BasicTest(data);
            Test(data.Colonist, "basic");

            ReadKey(true);

            data = new DataContainer();
            ChildTest(data);
            Test(data.Colonist, "child");

            ReadKey(true);
        }

        private static void Test(IEnumerable<Colonist> test, string testName)
        {
            Clear();

            WriteLine("starting Colonists status for \"{0}\" test", testName);
            test.Print();

            WriteLine("\nmoving");
            for (int i = 0, n = new Random().Next(10, 25); i < n; ++i)
            {
                WriteLine("\tTick {0}", i);
                test.Work();
                test.Print();
            }
        }

        private static void BasicTest(DataContainer data)
        {
            char c = 'A';
            for (int i = 0; i < 5; ++i)
                data.Add(new Colonist((c++).ToString(), new Position(10, 10)));
        }

        private static void ChildTest(DataContainer data)
        {
            char c = 'A';
            var a = new Colonist((c++).ToString(), new Position(10, 10));
            var b = new Colonist((c++).ToString(), new Position(10, 10));
            var child = new Colonist($"{a.Name}&{b.Name} child", a, b);
            data.Add(a);
            data.Add(b);
            data.Add(child);
        }
    }
}
