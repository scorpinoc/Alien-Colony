using static System.Math;

namespace Data.Data.Common
{
    public class Position
    {
        public static double Distance(Position a, Position b)
            => a.Distance(b);

        public uint X { get; set; }
        public uint Y { get; set; }

        public Position()
            : this(0, 0)
        { }

        public Position(Position obj)
            : this(obj.X, obj.Y)
        { }

        public Position(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public double Distance(Position other)
            => Sqrt(
                Pow(Max(X, other.X) - Min(X, other.X), 2)
                +
                Pow(Max(Y, other.Y) - Min(Y, other.Y), 2));
    }
}