using Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Data.Common;
using Data.Data.JobActions;
using Data.Data.Jobs;
using static System.Console;

namespace Data.ConsoleTest
{
    internal static class Program
    {
        private static void Main()
        {
            int testIndex;
            WriteLine("enter test number 0 to 4");
            if (!int.TryParse(ReadLine(), out testIndex))
                return;
            switch (testIndex)
            {
                case 0:
                    FullTestColnists(new DataContainer(new Position(1, 1)), "basic MoveTest use", BasicTest);
                    break;
                case 1:
                    FullTestColnists(new DataContainer(new Position(1, 1)), "child", ChildTest);
                    break;
                case 2:
                    FullTestColnists(new DataContainer(new Position(100, 100)), "Random MovingModuleJob", RandomModuleMove);
                    break;
                case 3:
                    FullTestColnists(new DataContainer(new Position(100, 100)), "SingleTargeted MovingModuleJob - to {{50;50}}", SignleTargetModuleMoveTestInitializer);
                    break;
                case 4:
                    FullTestColnists(new DataContainer(new Position(100, 100)),
                        "MultiTargeted MovingModuleJob - to {{25;25}, {25;75}, {75;75}, {75;25}}", MultiTargetModuleMoveTestInitializer);
                    break;
                default:
                    WriteLine("No such test number");
                    break;
            }

            ReadKey(true);
        }


        #region Tests

        private static void MultiTargetModuleMoveTestInitializer(DataContainer obj)
        {
            var c = 'A';
            var job = new MovingModuleJob("Targered Position Moving", new MoveAction(new MultiTargetPositionAction(obj,
                data => new[] { new Position(25, 25), new Position(25, 75), new Position(75, 75), new Position(75, 25), })));
            obj.Add(job);
            var rand = new Random();
            for (var i = 0; i < 3; ++i)
                obj.Add(new Colonist((c++).ToString(), new Position((uint)rand.Next(25, 75), (uint)rand.Next(25, 75)), job));
        }

        private static void SignleTargetModuleMoveTestInitializer(DataContainer obj)
        {
            var c = 'A';
            var job = new MovingModuleJob("Targered Position Moving", new MoveAction(new SingleTargetPositionAction(obj, data => new Position(50, 50))));
            obj.Add(job);
            var rand = new Random();
            for (var i = 0; i < 3; ++i)
                obj.Add(new Colonist((c++).ToString(), new Position((uint)rand.Next(15, 85), (uint)rand.Next(25, 75)), job));
        }

        private static void RandomModuleMove(DataContainer obj)
        {
            var c = 'A';
            var job = new MovingModuleJob("Random Position Moving", new MoveAction(/*5,*/ new RandomPositionAction(obj)));
            obj.Add(job);
            for (var i = 0; i < 5; ++i)
                obj.Add(new Colonist((c++).ToString(), new Position(10, 10), job));
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

        private static void BasicTest(DataContainer data)
        {
            var c = 'A';
            for (var i = 0; i < 5; ++i)
                data.Add(new Colonist((c++).ToString(), new Position(10, 10), new MoveTest()));
        }

        #endregion

        private static void FullTestColnists(DataContainer data, string testName, Action<DataContainer> testInitializer)
        {
            if (data == null) return;
            testInitializer?.Invoke(data);
            Test(data.Colonists, testName);
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
    }
}
