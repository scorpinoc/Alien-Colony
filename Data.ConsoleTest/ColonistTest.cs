using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;

namespace Data.ConsoleTest
{
    static public class ColonistTest
    {
        static public string StringKey(this Colonist obj)
            => $"\"{obj.Name}\" job: {obj.JobName}; doing: {obj.currentDo};"
            + $"\n\tstatus - energy: {{{obj.Energy} | {obj.EnergyPersent * 100:f1}%}}; pos: {{{obj.Position.X}, {obj.Position.Y}}};";

        static public void Print(this IEnumerable<Colonist> objs)
        {
            foreach (var i in objs)
                Console.WriteLine(i.StringKey());
        }
        static public void Work(this IEnumerable<Colonist> objs)
        {
            foreach (var i in objs)
                i.Tick();
        }
    }
}
