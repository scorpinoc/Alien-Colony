using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var i in new DataContainer().Colonist)
                Console.WriteLine(i.StringKey());

            Console.ReadKey(true);
        }
    }
}
