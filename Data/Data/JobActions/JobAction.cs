using System;
using Data.Interfaces;

namespace Data.Data.JobActions
{
    /// <summary>
    /// basic class for <see cref="JobAction"/> classes modules used by <see cref="IJobable"/>
    /// </summary>
    public abstract class JobAction
    {
        /// <summary>
        /// current Alian-Colony data
        /// </summary>
        protected DataContainer OwnerContainer { get; }

        /// <summary>
        /// get next action for sequential use
        /// <para>can be null for last in the squence element</para>
        /// </summary>
        protected JobAction NextAction { get; }

        protected JobAction(DataContainer ownerContainer, JobAction nextAction)
        {
            NextAction = nextAction;
            if (ownerContainer == null)
                throw new ArgumentNullException($"{nameof(JobAction)} must have owner colony - {nameof(DataContainer)}");
            OwnerContainer = ownerContainer;
        }
    }
}
