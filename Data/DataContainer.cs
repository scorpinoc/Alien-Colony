using Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContainer
    {
        #region fields

        private readonly List<Colonist> colonists = new List<Colonist>();

        #endregion

        #region properties

        public IEnumerable<Colonist> Colonist => colonists;

        #endregion

        #region methods

        /// <summary>
        /// Add new <see cref="Colonist"/> to the colony
        /// </summary>
        /// <param name="obj">new <see cref="Colonist"/></param>
        public void Add(Colonist obj) => colonists.Add(obj);

        #endregion
    }
}
