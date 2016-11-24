using System;
using Data.Data.Common;
using Data.Interfaces;

namespace Data.Data.JobActions
{
    /// <summary>
    /// basic class for <see cref="JobAction"/> classes modules used by <see cref="IJobable"/>
    /// </summary>
    public abstract class JobAction
    {
        /// <summary>
        /// get next action for sequential use
        /// <para>can be null for last in the squence element</para>
        /// </summary>
        protected JobAction NextAction { get; }

        protected JobAction(JobAction nextAction)
        {
            NextAction = nextAction;
        }
    }
}
