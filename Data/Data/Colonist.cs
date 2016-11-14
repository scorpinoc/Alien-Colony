using Data.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class Colonist : INameble
    {
        #region fields

        private string name;

        private Position position;

        #endregion

        #region properties

        public string Name => name;

        public Position Position => position;

        #endregion

        public Colonist(string name)
            : this(name, new Position())
        { }

        public Colonist(string name, Position position)
        {
            this.name = name;
            this.position = position;
        }
    }
}