using System;
using Data.Data.Common;

namespace Data.Data.JobActions
{
    /// <summary>
    /// Action wich generates targets for <see cref="MoveAction"/>s
    /// </summary>
    public abstract class PositionAction : JobAction
    {
        protected DataContainer OwnerContainer { get; }

        protected PositionAction(DataContainer ownerContainer) : base(null)
        {
            if (ownerContainer == null)
                throw new ArgumentNullException(nameof(ownerContainer));
            OwnerContainer = ownerContainer;
        }

        public abstract Position Target(Colonist worker);
    }
}