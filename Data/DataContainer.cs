using System;
using Data.Data;
using System.Collections.Generic;
using System.Linq;
using Data.Data.Common;
using Data.Interfaces;

namespace Data
{
    public class DataContainer : INameble
    {
        #region fields
        private readonly List<Colonist> _colonists = new List<Colonist>();
        private readonly List<IJobable> _jobs = new List<IJobable>();
        private readonly List<Building> _buildings = new List<Building>();
        #endregion

        #region properties

        #region auto
        public string Name { get; }
        public Position Size { get; }
        #endregion

        #region delegate
        public IEnumerable<Colonist> Colonists => _colonists;
        public IEnumerable<IJobable> Jobs => _jobs;
        public IEnumerable<Building> Buildings => _buildings;
        public IEnumerable<IPhysicalObject> PhysicalObjects => Colonists.AsEnumerable<IPhysicalObject>().Union(Buildings);
        #endregion

        #endregion

        #region constructors
        public DataContainer(Position size)
            : this("Unnamed", size)
        { }

        public DataContainer(string colonyName, Position size)
        {
            if (size == null)
                throw new ArgumentNullException(nameof(Size));
            if (size.X == 0 || size.Y == 0)
                throw new ArgumentOutOfRangeException($"{nameof(Size)} can't be less than 0 in any of edges");
            if (string.IsNullOrWhiteSpace(colonyName))
                throw new ArgumentNullException(nameof(colonyName));

            Name = colonyName;
            Size = size;
        }
        #endregion

        #region methods

        /// <summary>
        /// Add new <see cref="Colonists"/> to the colony
        /// </summary>
        /// <param name="obj">new <see cref="Colonists"/></param>
        public void Add(Colonist obj)
            => _colonists.Add(obj);

        /// <summary>
        /// Add new <see cref="IJobable"/> job to the colony
        /// </summary>
        /// <param name="obj">new <see cref="IJobable"/> job</param>
        public void Add(IJobable obj)
            => _jobs.Add(obj);

        /// <summary>
        /// Add new <see cref="Buildings"/>  to the colony
        /// </summary>
        /// <param name="obj">new <see cref="Buildings"/> job</param>
        public void Add(Building obj)
            => _buildings.Add(obj);
        #endregion
    }
}
