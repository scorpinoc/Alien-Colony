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
        private IEnumerable<Colonist> colonist = new Colonist[]{
            new Colonist("colonist A"),
            new Colonist("colonist B")
        };

        public IEnumerable<Colonist> Colonist => colonist;
    }
}
