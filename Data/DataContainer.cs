using System;
using Data.Data;
using System.Collections.Generic;
using Data.Data.Common;
using Data.Interfaces;

namespace Data
{
    public class DataContainer : INameble
    {
        #region fields
        private readonly List<Colonist> _colonists = new List<Colonist>();
        private readonly List<IJobable> _jobs = new List<IJobable>();
        #endregion

        #region properties
        public string Name { get; }
        public Position Size { get; }
        public IEnumerable<Colonist> Colonists => _colonists;
        public IEnumerable<IJobable> Jobs => _jobs;
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
        public void Add(Colonist obj) => _colonists.Add(obj);

        /// <summary>
        /// Add new <see cref="IJobable"/> job to the colony
        /// </summary>
        /// <param name="obj">new <see cref="IJobable"/> job</param>
        public void Add(IJobable obj) => _jobs.Add(obj);

        #endregion

    }
}
