using Data.Data.Jobs;
using System;
using Data.Data.Common;
using Data.Interfaces;

namespace Data.Data
{
    #region Direction

    /// <summary>
    /// Directions for <see cref="Colonist.Move(Direction)"/>
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
        Stop
    }

    #endregion

    public class Colonist : INameble
    {
        #region static

        private static readonly Random Rand = new Random();

        #endregion

        #region constants

        /// <summary>
        /// _current status - what <see cref="Colonist"/> doing
        /// </summary>
        public enum Doing
        {
            Sleep,
            Work,
        }

        #endregion

        #region fields

        private readonly IJobable _job;
        private readonly ColonistEnergy _energy;

        #endregion

        #region properties

        #region auto

        public string Name { get; }
        public Position Position { get; }
        public Doing CurrentDoing { get; private set; }

        #endregion

        #region delegate

        public string JobName => _job.Name;

        public uint Energy => _energy.Energy;
        public double EnergyPersent => _energy.EnergyPersent;

        #endregion

        #endregion

        #region constructors

        public Colonist(string name) : this(name, new Position())
        {
        }

        public Colonist(string name, Position position) : this(name, position, new MoveTest() /* TODO : change to basic _job*/)
        {
        }

        public Colonist(string name, Position position, IJobable job) : this(name, position, job, new ColonistEnergy())
        {
        }

        /// <summary>
        /// child generation constructor
        /// </summary>
        public Colonist(string name, Colonist parentA, Colonist parentB)
            : this(name: name, position: new Position(parentA.Position),
                  job: Rand.Next(1) == 0 ? parentA._job : parentB._job,
                  energy: new ColonistEnergy(parentA._energy, parentB._energy))
        {
        }

        /// <summary>
        /// full constructor
        /// </summary>
        private Colonist(string name, Position position, IJobable job, ColonistEnergy energy)
        {
            #region check

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(Name));
            if (position == null)
                throw new ArgumentNullException(nameof(Position));
            if (job == null)
                throw new ArgumentNullException(nameof(_job));
            if (energy == null)
                throw new ArgumentNullException(nameof(_energy));

            #endregion

            Name = name;
            Position = position;
            _job = job;
            _energy = energy;

            CurrentDoing = Doing.Work;
        }

        #endregion

        #region methods

        /// <summary>
        /// Method for colonist moving - 1 point move speed.
        /// </summary>
        /// <param name="direction"><see cref="Direction"/> of moving</param>
        public void Move(Direction direction)
        {
            /* TODO : may be rework 
             and what to do if no moving (using random move) - invalid direction */
            while (true)
            {
                switch (direction)
                {
                    case Direction.North:
                        if (Position.Y > 0) --Position.Y;
                        break;
                    case Direction.NorthEast:
                        Move(Direction.North);
                        direction = Direction.East;
                        continue;
                    case Direction.East:
                        if (Position.X > 0) --Position.X;
                        break;
                    case Direction.SouthEast:
                        Move(Direction.South);
                        direction = Direction.East;
                        continue;
                    case Direction.South:
                        if (Position.Y < 1000) ++Position.Y;
                        break;
                    case Direction.SouthWest:
                        Move(Direction.South);
                        direction = Direction.West;
                        continue;
                    case Direction.West:
                        if (Position.X < 1000) ++Position.X;
                        break;
                    case Direction.NorthWest:
                        Move(Direction.North);
                        direction = Direction.West;
                        continue;
                    case Direction.Stop:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }

                #region Old version

                //Direction vertical;
                //Direction horisontal;
                //switch (direction)
                //{
                //    case Direction.NorthEast:
                //        vertical = Direction.North;
                //        horisontal = Direction.East;
                //        break;
                //    case Direction.SouthEast:
                //        vertical = Direction.South;
                //        horisontal = Direction.East;
                //        break;
                //    case Direction.SouthWest:
                //        vertical = Direction.South;
                //        horisontal = Direction.West;
                //        break;
                //    case Direction.NorthWest:
                //        vertical = Direction.North;
                //        horisontal = Direction.West;
                //        break;
                //    default:
                //        vertical = direction;
                //        horisontal = direction;
                //        break;
                //}

                //switch (vertical)
                //{
                //    case Direction.North:
                //        if (Position.Y > 0) --Position.Y;
                //        break;
                //    case Direction.South:
                //        if (Position.Y < 1000) ++Position.Y;
                //        break;
                //}
                //switch (horisontal)
                //{
                //    case Direction.East:
                //        if (Position.X > 0) --Position.X;
                //        break;
                //    case Direction.West:
                //        if (Position.X < 1000) ++Position.X;
                //        break;
                //} 

                #endregion

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
            if (CurrentDoing == Doing.Sleep && !_energy.Tick(true))
                CurrentDoing = Doing.Work;
            else if (CurrentDoing == Doing.Work && !_energy.Tick(false))
                CurrentDoing = Doing.Sleep;
        }

        #endregion
    }
}