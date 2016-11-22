using System;
using Data.Interfaces;
using static Data.Data.Colonist;

namespace Data.Data.Jobs
{
    /// <summary>
    /// <see cref="Random"/> moving 
    /// </summary>
    public class MoveTest : IJobable
    {
        private static readonly Random Rand = new Random();

        public string Name => "MoveTestJob";

        public void Work(Colonist worker)
            => worker?.Move((Direction)Rand.Next((int)Direction.North, (int)Direction.NorthWest));
    }

}
