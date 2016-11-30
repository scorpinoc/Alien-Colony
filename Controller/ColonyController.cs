using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Timers;
using Data;
using Data.Data;
using Data.Interfaces;
using Timer = System.Timers.Timer;

namespace Controller
{
    public class ColonyController : INameble, INotifyCollectionChanged
    {

        #region static
        #endregion

        #region constants
        #endregion

        #region events
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region fields
        #endregion

        #region properties

        #region auto
        public string Name { get; }
        private DataContainer ColonyContainer { get; }
        private Timer Timer { get; set; }
        #endregion

        #region delegate
        public IEnumerable<Colonist> Colonists => ColonyContainer.Colonists;
        #endregion

        #endregion

        #region constructors

        public ColonyController(DataContainer colonyContainer)
        {
            ColonyContainer = colonyContainer;
            Name = ColonyContainer.Name;
        }
        #endregion

        #region methods
        public void Start()
        {
            Timer = new Timer(1000);
            Timer.Elapsed += TickOnElapsed;
            Timer.Start();
        }

        private void TickOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            ColonyContainer.Colonists.ToList().ForEach(colonist => colonist.Tick());
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion
    }
}