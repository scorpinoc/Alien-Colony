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
            => $"colonist name: {obj.Name} job: {obj.JobName} position: {{{obj.Position.Y}, {obj.Position.Y}}}";

        static public void Print(this IEnumerable<Colonist> objs)
        {
            foreach (var i in objs)
                Console.WriteLine(i.StringKey());
        }
        static public void Work(this IEnumerable<Colonist> objs)
        {
            foreach (var i in objs)
                i.Work();
        }
    }
}
