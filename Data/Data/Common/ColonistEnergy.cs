using System;

namespace Data.Data.Common
{
    public class ColonistEnergy
    {
        #region static
        private static readonly Random Rand = new Random();
        #endregion

        #region constants
        private const int MinimalEnergy = 10;
        private const int MinimalEnergyX2 = MinimalEnergy * 2;
        private const int MaximumEnergy = 100;
        #endregion

        #region fields
        private readonly uint _maxEnergy;
        private readonly uint _minEnergyTick;

        private uint _energyUp;
        private uint _energyDown;
        #endregion


        #region properties
        public uint Energy { get; private set; }
        public double EnergyPersent => Energy / (double)_maxEnergy * 100;
        #endregion

        #region constructors
        public ColonistEnergy()
            : this(MinimalEnergyX2, MaximumEnergy)
        { }

        /// <summary>
        /// child generation constructor - part of <see cref="Colonist"/>
        /// <para/> 
        /// generates <see cref="Random"/> energy consist on <paramref name="a"/> and <paramref name="b"/> data
        /// </summary>
        public ColonistEnergy(ColonistEnergy a, ColonistEnergy b)
            : this(min: (int)Math.Max(Math.Min(a._maxEnergy, b._maxEnergy) * 0.9, MinimalEnergyX2),
                   max: (int)(Math.Max(a._maxEnergy, b._maxEnergy) * 1.1))
        { }

        public ColonistEnergy(int min, int max)
        {
            if(min < 0 || max < 0)
                throw new ArgumentOutOfRangeException($"{nameof(min)} or {nameof(max)} can't be less than 0");
            Energy = _maxEnergy = (uint)Rand.Next(min, max);
            _minEnergyTick = (uint)(_maxEnergy * 0.1);
            _energyUp = _energyDown = _minEnergyTick;
        }
        #endregion

        #region methods
        /// <summary>
        /// Changing <see cref="Colonist"/> energy consist to <paramref name="sleeping"/> status.
        /// </summary>
        /// <param name="sleeping">true if currently colonist sleeping</param>
        /// <returns>true if colonist status left unchanged (still sleeping or still not sleeping)</returns>
        public bool Tick(bool sleeping)
        {
            if (sleeping)
            {
                Energy += (uint)Rand.Next((int)++_energyUp);
                if (Energy < _maxEnergy) return true;
                Energy = _maxEnergy;
                _energyUp = _minEnergyTick;
                return false;
            }

            try { checked { Energy -= (uint)Rand.Next((int)++_energyDown); } }
            catch { Energy = 0; }
            if (Energy >= MinimalEnergy) return true;
            _energyDown = _minEnergyTick;
            return false;
        }
        #endregion
    }
}
