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

        #endregion

        #region fields

        private IJobable job;

        #endregion

        #region properties

        public string Name { get; }

        public Position Position { get; }

        public string JobName => job.Name;

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
            Direction vertical = direction;
            Direction horisontal = direction;
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
        /// <see cref="Colonist"/> job tick activation
        /// </summary>
        public void Work() => job.Work(this);

        #endregion
    }
}