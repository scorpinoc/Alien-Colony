using Data.Data.Common;

namespace Data.Data.JobActions
{
    /// <summary>
    /// Action wich generates targets for <see cref="MoveAction"/>s
    /// </summary>
    public abstract class PositionAction : JobAction
    {
        protected PositionAction(DataContainer ownerContainer, JobAction nextAction) : base(ownerContainer, nextAction)
        { }

        public abstract Position Target(Colonist worker);
    }
}