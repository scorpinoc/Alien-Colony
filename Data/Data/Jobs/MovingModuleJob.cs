using Data.Data.Common;
using Data.Data.JobActions;

namespace Data.Data.Jobs
{
    /// <summary>
    /// Job for moving <see cref="Colonist"/> from <see cref="Position"/> to <see cref="Position"/>
    /// </summary>
    public class MovingModuleJob : ModuleJob
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public MovingModuleJob(string name, MoveAction startingAction) : base(name, startingAction)
        { }

        protected override void RealWork(Colonist worker)
            => ((MoveAction)Action).Move(worker);
    }
}