using Data.Basic;
using Data.Data.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class Colonist : INameble
    {
        #region static
        static Random rand = new Random();
        #endregion

        #region constants

        /// <summary>
        /// Directions for <see cref="Move(Direction)"/>
        /// </summary>
        public enum Direction
        {
            North,
            NorthEast,
            East,
            SouthEast,
            South,
            SouthWest,
            West,
            NorthWest,
        }

        /// <summary>
        /// current status - what <see cref="Colonist"/> doing
        /// </summary>
        public enum Doing
        {
            Sleep,
            Work,
        }

        const int minimalEnergy = 10;
        const int minimalEnergyX2 = minimalEnergy * 2;
        const int maximumEnergy = 100;
        #endregion

        #region fields

        #region active
        private IJobable job;

        private uint energy;

        private Doing current;
        #endregion

        #region constant - limits
        private readonly uint maxEnergy;
        #endregion

        #region dynamic - can be changed on each tick
        private int energyUp = 0;
        private int energyDown = 0;
        #endregion

        #endregion

        #region properties

        public string Name { get; }

        public Position Position { get; }

        public string JobName => job.Name;

        public uint Energy => energy;
        public double EnergyPersent => energy / (double)maxEnergy;

        public Doing currentDo => current;

        #endregion

        #region constructors

        public Colonist(string name)
            : this(name, new Position())
        { }

        public Colonist(string name, Position position)
            : this(name, position, new MoveTest() /* TODO : change to basic job*/)

        { }

        public Colonist(string name, Position position, IJobable job)
        {
            Name = name;
            Position = position;
            this.job = job;

            energy = maxEnergy = (uint)rand.Next(minimalEnergyX2, maximumEnergy);
            current = Doing.Work;
        }

        #endregion

        #region methods

        /// <summary>
        /// Method for colonist moving - 1 point move speed.
        /// </summary>
        /// <param name="direction"><see cref="Direction"/> of moving</param>
        public void Move(Direction direction)
        {
            // TODO : may be rework
            // TODO : what to do if no moving (using random move) - invalid direction
            Direction vertical;
            Direction horisontal;
            switch (direction)
            {
                case Direction.NorthEast:
                    vertical = Direction.North;
                    horisontal = Direction.East;
                    break;
                case Direction.SouthEast:
                    vertical = Direction.South;
                    horisontal = Direction.East;
                    break;
                case Direction.SouthWest:
                    vertical = Direction.South;
                    horisontal = Direction.West;
                    break;
                case Direction.NorthWest:
                    vertical = Direction.North;
                    horisontal = Direction.West;
                    break;
                default:
                    vertical = direction;
                    horisontal = direction;
                    break;
            }

            switch (vertical)
            {
                case Direction.North:
                    if (Position.Y > 0) --Position.Y;
                    break;
                case Direction.South:
                    if (Position.Y < 1000) ++Position.Y;
                    break;
                default:
                    break;
            }
            switch (horisontal)
            {
                case Direction.East:
                    if (Position.X > 0) --Position.X;
                    break;
                case Direction.West:
                    if (Position.X < 1000) ++Position.X;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// <see cref="Colonist"/> tick activation
        /// parameters changes + job activation
        /// </summary>
        public void Tick()
        {
            EnergyTick();

            job.Work(this);
        }

        private void EnergyTick()
        {
            if (current == Doing.Sleep)
            {
                energy += (uint)rand.Next(0, ++energyUp);
                if (energy >= maxEnergy)
                {
                    energy = maxEnergy;
                    current = Doing.Work;
                    energyUp = 0;
                }
            }
            else
            {
                try { checked { energy -= (uint)rand.Next(0, ++energyDown); } }
                catch { energy = 0; }
                if (energy < minimalEnergy)
                {
                    current = Doing.Sleep;
                    energyDown = 0;
                }
            }
        }

        #endregion
    }
}