using System;
using Data.Data.Common;
using Data.Data.JobActions;
using Data.Interfaces;

namespace Data.Data.Jobs
{
    /// <summary>
    /// represents full <see cref="Colonist"/> <see cref="IJobable"/> Jobs in one class
    /// <para>Considered to <see cref="Colonist.Doing"/> choose wich <see cref="ModuleJob"/> to use </para> 
    /// </summary>
    public class MultiModuleJob : IJobable
    {
        #region static
        #endregion

        #region constants
        #endregion

        #region fields
        #endregion

        #region properties

        #region auto
        public string Name { get; }
        private DataContainer OwnerContainer { get; }
        private ModuleJob CriticalNeedsStatusJob { get; } 
        private ModuleJob NormalNeedsStatusJob { get; } 
        private ModuleJob BasicJob { get; }
        #endregion

        #region delegate
        #endregion

        #endregion

        #region constructors
        public MultiModuleJob(string name, DataContainer ownerContainer, ModuleJob basicJob)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(Name));
            if (basicJob == null)
                throw new ArgumentNullException(nameof(basicJob));

            Name = name;
            OwnerContainer = ownerContainer;
            // todo CriticalNeedsStatusJob
            CriticalNeedsStatusJob = new MovingModuleJob("Critical", new MoveAction(new SingleTargetPositionAction(OwnerContainer, container => new Position())));
            // todo NormalNeedsStatusJob
            NormalNeedsStatusJob = new MovingModuleJob("Normal", new MoveAction(new SingleTargetPositionAction(OwnerContainer, container => container.Size)));

            BasicJob = basicJob;
        }
        #endregion

        #region methods
        public void Work(Colonist worker)
        {
            if (worker == null)
                throw new ArgumentNullException(nameof(worker));
            switch (worker.CurrentDoing)
            {
                case Colonist.Doing.Sleeping:
                    break;
                case Colonist.Doing.CriticalNeedSleep:
                    CriticalNeedsStatusJob.Work(worker);
                    break;
                case Colonist.Doing.NeedSleep:
                    NormalNeedsStatusJob.Work(worker);
                    break;
                case Colonist.Doing.Work:
                    BasicJob.Work(worker);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}