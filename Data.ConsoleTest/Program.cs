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
            var tests = new Action[]
                {
                    BasicTest,
                    ChildTest,
                    RandomModuleMoveTest,
                    SignleTargetModuleMoveTest,
                    MultiTargetModuleMoveTest,
                    MultiModuleJobTest,
                };
            while (true)
            {

                Clear();
                WriteLine($"enter test number 1 to {tests.Length} (anithing else to exit)");
                int testIndex;
                int.TryParse(ReadLine(), out testIndex);
                if (testIndex > 0 && --testIndex < tests.Length)
                    tests[testIndex]();
                else
                {
                    WriteLine("No such test number - exit");
                    return;
                }
            }
        }

        #region Tests

        private static void MultiModuleJobTest()
        {

            var obj = new DataContainer(new Position(100, 100));
            var c = 'A';
            var job = new MultiModuleJob("test", obj, new MovingModuleJob("test",
                new MoveAction(new SingleTargetPositionAction(obj, data => new Position(50, 50)))));
            obj.Add(job);
            var rand = new Random();
            for (var i = 0; i < 4; ++i)
                obj.Add(new Colonist((c++).ToString(), new Position((uint)rand.Next(20, 80), (uint)rand.Next(20, 80)), job));
            Test(obj.Colonists, $"{nameof(MultiModuleJob)} -> {nameof(MovingModuleJob)} -> {nameof(SingleTargetPositionAction)} to {{50;50}}");
        }

        private static void MultiTargetModuleMoveTest()
        {
            var obj = new DataContainer(new Position(100, 100));
            var c = 'A';
            var job = new MovingModuleJob("test",
                new MoveAction(new MultiTargetPositionAction(obj,
                data => new[] { new Position(25, 25), new Position(25, 75), new Position(75, 75), new Position(75, 25), })));
            obj.Add(job);
            var rand = new Random();
            for (var i = 0; i < 4; ++i)
                obj.Add(new Colonist((c++).ToString(), new Position((uint)rand.Next(20, 80), (uint)rand.Next(20, 80)), job));
            Test(obj.Colonists, $"{nameof(MovingModuleJob)} -> {nameof(MultiTargetPositionAction)} to {{{{25;25}}, {{25;75}}, {{75;75}}, {{75;25}}}}");
        }

        private static void SignleTargetModuleMoveTest()
        {
            var obj = new DataContainer(new Position(100, 100));
            var c = 'A';
            var job = new MovingModuleJob("test",
                new MoveAction(new SingleTargetPositionAction(obj, data => new Position(50, 50))));
            obj.Add(job);
            var rand = new Random();
            for (var i = 0; i < 3; ++i)
                obj.Add(new Colonist((c++).ToString(), new Position((uint)rand.Next(15, 85), (uint)rand.Next(25, 75)), job));
            Test(obj.Colonists, $"{nameof(MovingModuleJob)} -> {nameof(SingleTargetPositionAction)} to {{50;50}}");
        }

        private static void RandomModuleMoveTest()
        {
            var obj = new DataContainer(new Position(100, 100));
            var c = 'A';
            var job = new MovingModuleJob("test", new MoveAction(/*5,*/ new RandomPositionAction(obj)));
            obj.Add(job);
            for (var i = 0; i < 5; ++i)
                obj.Add(new Colonist((c++).ToString(), new Position(10, 10), job));
            Test(obj.Colonists, $"{nameof(MovingModuleJob)} -> {nameof(RandomPositionAction)}");
        }

        private static void ChildTest()
        {
            var data = new DataContainer(new Position(1, 1));
            var c = 'A';
            var a = new Colonist((c++).ToString(), new Position(10, 10), new MoveTest());
            var b = new Colonist((c).ToString(), new Position(10, 10), new MoveTest());
            var child = new Colonist($"{a.Name}&{b.Name} child", a, b);
            data.Add(a);
            data.Add(b);
            data.Add(child);
            Test(data.Colonists, $"child generation");
        }

        private static void BasicTest()
        {
            var data = new DataContainer(new Position(1, 1));
            var c = 'A';
            for (var i = 0; i < 5; ++i)
                data.Add(new Colonist((c++).ToString(), new Position(10, 10), new MoveTest()));
            Test(data.Colonists, $"Basic {nameof(MoveTest)}");
        }

        #endregion

        private static void Test(IEnumerable<Colonist> testData, string testName)
        {
            Clear();

            WriteLine("starting Colonists status for \"{0}\" test", testName);
            var colonists = testData as IList<Colonist> ?? testData.ToList();
            colonists.Print();

            WriteLine();
            WriteLine("moving");

            for (int i = 0, n = new Random().Next(10, 25); i < n; ++i)
            {
                WriteLine("\tTick {0}", i);
                colonists.Work();
                colonists.Print();
            }
            ReadKey(true);
        }
    }
}
