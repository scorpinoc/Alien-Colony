using Data.Data;
using System.Collections.Generic;

namespace Data
{
    public class DataContainer
    {
        #region fields

        private readonly List<Colonist> _colonists = new List<Colonist>();

        #endregion

        #region properties

        public IEnumerable<Colonist> Colonist => _colonists;

        #endregion

        #region methods

        /// <summary>
        /// Add new <see cref="Colonist"/> to the colony
        /// </summary>
        /// <param name="obj">new <see cref="Colonist"/></param>
        public void Add(Colonist obj) => _colonists.Add(obj);

        #endregion
    }
}
