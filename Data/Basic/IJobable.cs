using Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Basic
{
    public interface IJobable : INameble
    {
        void Work(Colonist worker);
    }
}
