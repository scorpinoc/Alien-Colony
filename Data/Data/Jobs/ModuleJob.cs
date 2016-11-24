using System;
using Data.Data.JobActions;
using Data.Interfaces;

namespace Data.Data.Jobs
{
    public abstract class ModuleJob : IJobable
    {
        protected JobAction Action { get; }

        public string Name { get; }

        protected ModuleJob(string name, JobAction startingAction)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(Name));
            if (startingAction == null)
                throw new ArgumentNullException(nameof(JobAction));

            Name = name;
            Action = startingAction;
        }

        public void Work(Colonist worker)
        {
            if (worker == null)
                throw new ArgumentNullException(nameof(worker));
            RealWork(worker);
        }

        /// <summary>
        /// Activation of modules
        /// </summary>
        protected abstract void RealWork(Colonist worker);
    }
}