using Data.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Data.Colonist;

namespace Data.Data.Jobs
{
    /// <summary>
    /// <see cref="Random"/> moving 
    /// </summary>
    public class MoveTest : IJobable
    {
        static Random rand = new Random();

        public string Name => "MoveTestJob";

        public void Work(Colonist worker)
        {
            Direction dir = (Direction)rand.Next((int)Direction.North, (int)Direction.NorthWest);
            worker.Move(dir);
        }
    }

}
