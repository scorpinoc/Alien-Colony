using System;
using System.Collections.Generic;
using Data.Data;

namespace Data.ConsoleTest
{
    public static class ColonistTest
    {
        public static string StringKey(this Colonist obj)
            => $"\"{obj.Name}\" job: {obj.JobName}; doing: {obj.CurrentDoing};"
               +
               $"\n\tstatus - energy: {{{obj.Energy} | {obj.EnergyPersent:f1}%}}; pos: {{{obj.Position.X}, {obj.Position.Y}}};";

        public static void Print(this IEnumerable<Colonist> objs)
        {
            foreach (var i in objs)
                Console.WriteLine(i.StringKey());
        }

        public static void Work(this IEnumerable<Colonist> objs)
        {
            foreach (var i in objs)
                i.Tick();
        }
    }
}
