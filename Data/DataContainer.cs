using Data.Data;
using System.Collections.Generic;
using Data.Interfaces;

namespace Data
{
    public class DataContainer
    {
        #region fields
        private readonly List<Colonist> _colonists = new List<Colonist>();
        private readonly List<IJobable> _jobs = new List<IJobable>();
        #endregion

        #region properties
        public IEnumerable<Colonist> Colonists => _colonists;
        public IEnumerable<IJobable> Jobs => _jobs;
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
