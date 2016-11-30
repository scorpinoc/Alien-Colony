using System;
using static Data.Data.Colonist.StatusType;

namespace Data.Data.Common
{
    public class ColonistEnergy
    {
        #region static
        private static readonly Random Rand = new Random();
        #endregion

        #region constants
        private const int MinimalEnergy = 10;
        private const int CriticalEnergy = MinimalEnergy * 2;
        private const int InNeedEnergy = CriticalEnergy + MinimalEnergy;
        private const int MaximumEnergy = 200;
        #endregion

        #region fields
        private readonly uint _maxEnergy;
        private readonly uint _minEnergyTick;

        private uint _energyChanger;
        #endregion
        
        #region properties
        public uint Energy { get; private set; }
        public double EnergyPersent => Energy / (double)_maxEnergy * 100;
        #endregion

        #region constructors
        public ColonistEnergy()
            : this(InNeedEnergy, MaximumEnergy)
        { }

        /// <summary>
        /// child generation constructor - part of <see cref="Colonist"/>
        /// <para/> 
        /// generates <see cref="Random"/> energy consist on <paramref name="a"/> and <paramref name="b"/> data
        /// </summary>
        public ColonistEnergy(ColonistEnergy a, ColonistEnergy b)
            : this(min: (int)Math.Max(Math.Min(a._maxEnergy, b._maxEnergy) * 0.9, InNeedEnergy),
                   max: (int)(Math.Max(a._maxEnergy, b._maxEnergy) * 1.1))
        { }

        private ColonistEnergy(int min, int max)
        {
            #region while ctor private

            //if (min < 0 || max < 0)
            //    throw new ArgumentOutOfRangeException($"{nameof(min)} or {nameof(max)} can't be less than 0");

            #endregion

            Energy = _maxEnergy = (uint)Rand.Next(min, max);
            _minEnergyTick = (uint)(_maxEnergy * 0.1);
            _energyChanger = _minEnergyTick;
        }
        #endregion

        #region methods
        /// <summary>
        /// Changing <see cref="Colonist"/> energy consist to <paramref name="sleeping"/> status.
        /// </summary>
        /// <param name="sleeping">true if currently colonist sleeping</param>
        /// <returns><see cref="Normal"/> if all ok, <see cref="InNeed"/> on low energy and
        /// <see cref="Critical"/> if sleeping already</returns>
        public Colonist.StatusType Tick(bool sleeping)
        {
            if (sleeping)
            {
                Energy += (uint)Rand.Next((int)++_energyChanger);
                if (Energy < CriticalEnergy) return Critical;
                if (Energy < InNeedEnergy) return Warning;
                if (Energy < _maxEnergy) return InNeed;
                Energy = _maxEnergy;
                _energyChanger = _minEnergyTick;
                return Normal;
            }

            try { checked { Energy -= (uint)Rand.Next((int)++_energyChanger); } }
            catch { Energy = 0; }
            if (Energy > InNeedEnergy) return Normal;
            if (Energy > CriticalEnergy) return InNeed;
            if (Energy > MinimalEnergy) return Warning;
            _energyChanger = _minEnergyTick;
            return Critical;
        }
        #endregion
    }
}
