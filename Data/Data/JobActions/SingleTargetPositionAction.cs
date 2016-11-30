using System;
using Data.Data.Common;

namespace Data.Data.JobActions
{
    public class SingleTargetPositionAction : PositionAction
    {
        private Func<DataContainer, Position> FindTargetFunc { get; }

        public SingleTargetPositionAction(DataContainer ownerContainer, Func<DataContainer, Position> findTargetFunc) : base(ownerContainer)
        {
            FindTargetFunc = findTargetFunc;
        }

        public override Position Target(Colonist worker)
            => FindTargetFunc(OwnerContainer);
    }
}