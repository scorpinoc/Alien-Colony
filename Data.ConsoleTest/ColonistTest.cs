using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;

namespace Data.ConsoleTest
{
    public static class ColonistTest
    {
        static public string StringKey(this Colonist obj)
            => $"name: {obj.Name} position: {{{obj.Position.Y}, {obj.Position.Y}}}";
    }
}
