using System;
using Data.Data.Common;

namespace Data.Data.JobActions
{
    /// <summary>
    /// Generates <see cref="Random"/> <see cref="Position"/> as target for <see cref="MoveAction"/>
    /// </summary>
    public class RandomPositionAction : PositionAction
    {
        private static readonly Random Rand = new Random();

        public RandomPositionAction(DataContainer ownerContainer) : base(ownerContainer, null)
        { }

        public override Position Target(Colonist worker)
            => new Position((uint)Rand.Next((int)OwnerContainer.Size.X), (uint)Rand.Next((int)OwnerContainer.Size.Y));
    }
}