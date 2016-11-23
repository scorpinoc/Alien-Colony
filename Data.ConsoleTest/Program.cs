using Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Data.Common;
using Data.Data.Jobs;
using static System.Console;

namespace Data.ConsoleTest
{
    internal class Program
    {
        private static void Main()
        {
            var data = new DataContainer();
            BasicTest(data);
            Test(data.Colonists, "basic");

            ReadKey(true);

            data = new DataContainer();
            ChildTest(data);
            Test(data.Colonists, "child");

            ReadKey(true);
        }

        private static void Test(IEnumerable<Colonist> testData, string testName)
        {
            Clear();

            WriteLine("starting Colonists status for \"{0}\" test", testName);
            var colonists = testData as IList<Colonist> ?? testData.ToList();
            colonists.Print();

            WriteLine("\nmoving");
            for (int i = 0, n = new Random().Next(10, 25); i < n; ++i)
            {
                WriteLine("\tTick {0}", i);
                colonists.Work();
                colonists.Print();
            }
        }

        private static void BasicTest(DataContainer data)
        {
            var c = 'A';
            for (var i = 0; i < 5; ++i)
                data.Add(new Colonist((c++).ToString(), new Position(10, 10), new MoveTest()));
        }

        private static void ChildTest(DataContainer data)
        {
            var c = 'A';
            var a = new Colonist((c++).ToString(), new Position(10, 10), new MoveTest());
            var b = new Colonist((c).ToString(), new Position(10, 10), new MoveTest());
            var child = new Colonist($"{a.Name}&{b.Name} child", a, b);
            data.Add(a);
            data.Add(b);
            data.Add(child);
        }
    }
}
