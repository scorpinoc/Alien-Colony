using Data.Data.Jobs;
using System;
using Data.Data.Common;
using Data.Interfaces;

namespace Data.Data
{
    public class Colonist : INameble
    {
        #region static
        private static readonly Random Rand = new Random();
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
        /// _current status - what <see cref="Colonist"/> doing
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
        private readonly IJobable _job;

        #endregion

        #region constant - limits
        private uint maxEnergy;     // readonly
        private int minEnergyTick; // readonly
        #endregion

        #region dynamic - can be changed on each tick
        private int energyUp;
        private int energyDown;

        #endregion

        #endregion

        #region properties
        
        public string Name { get; }

        public Position Position { get; }

        public string JobName => _job.Name;

        public uint Energy { get; private set; }

        public double EnergyPersent => Energy / (double)maxEnergy;

        public Doing CurrentDoing { get; private set; }

        #endregion

        #region constructors

        public Colonist(string name)
            : this(name, new Position())
        { }

        public Colonist(string name, Position position)
            : this(name, position, new MoveTest() /* TODO : change to basic _job*/)

        { }

        /// <summary>
        /// child generation constructor
        /// </summary>
        public Colonist(string name, Colonist parentA, Colonist parentB)
            : this(name, new Position(parentA.Position), Rand.Next(1) == 0 ? parentA._job : parentB._job)
        {
            #region energy
            var min = (int)Math.Max(Math.Min(parentA.maxEnergy, parentB.maxEnergy) * 0.9, minimalEnergyX2);
            var max = (int)(Math.Max(parentA.maxEnergy, parentB.maxEnergy) * 1.1);
            InitializeEnergy(min, max);
            #endregion
        }

        public Colonist(string name, Position position, IJobable job)
        {
            Name = name;
            Position = position;
            this._job = job;

            #region energy
            InitializeEnergy(minimalEnergyX2, maximumEnergy);
            #endregion

            CurrentDoing = Doing.Work;
        }
        
        // TODO : rework to energy class
        private void InitializeEnergy(int min, int max)
        {
            Energy = maxEnergy = (uint)Rand.Next(min, max);
            minEnergyTick = (int)(maxEnergy * 0.1);
            energyUp = energyDown = minEnergyTick;
            throw new NotImplementedException();
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
            }
            switch (horisontal)
            {
                case Direction.East:
                    if (Position.X > 0) --Position.X;
                    break;
                case Direction.West:
                    if (Position.X < 1000) ++Position.X;
                    break;
            }
        }

        /// <summary>
        /// <see cref="Colonist"/> tick activation
        /// parameters changes + _job activation
        /// </summary>
        public void Tick()
        {
            EnergyTick();

            _job.Work(this);
        }

        private void EnergyTick()
        {
            if (CurrentDoing == Doing.Sleep)
            {
                Energy += (uint)Rand.Next(0, ++energyUp);
                if (Energy < maxEnergy) return;
                Energy = maxEnergy;
                energyUp = minEnergyTick;
                CurrentDoing = Doing.Work;
            }
            else
            {
                try { checked { Energy -= (uint)Rand.Next(0, ++energyDown); } }
                catch { Energy = 0; }
                if (Energy >= minimalEnergy) return;
                energyDown = minEnergyTick;
                CurrentDoing = Doing.Sleep;
            }
        }

        #endregion
    }
}