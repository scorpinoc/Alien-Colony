using System;
using System.Collections.Generic;
using System.Linq;
using Data.Data.Common;

namespace Data.Data.JobActions
{
    public class TargeredPositionAction : PositionAction
    {
        private Func<DataContainer, IEnumerable<Position>> FindTargetsFunc { get; }

        public TargeredPositionAction(DataContainer ownerContainer, Func<DataContainer, IEnumerable<Position>> findTargetsFunc) : base(ownerContainer)
        {
            FindTargetsFunc = findTargetsFunc;
        }

        public override Position Target(Colonist worker)
            => FindTargetsFunc(OwnerContainer).OrderByDescending(p => p.Distance(worker.Position)).First();
    }
}