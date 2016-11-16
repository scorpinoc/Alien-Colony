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

        private List<Colonist> colonists;

        public IEnumerable<Colonist> Colonist => colonists;

        public void LoadForTest()
        {
            colonists = new List<Colonist>{
                new Colonist("A", new Position(10, 10)),
                new Colonist("B", new Position(10, 10)),
                new Colonist("C", new Position(10, 10))
            };
        }
    }
}
