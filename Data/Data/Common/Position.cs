namespace Data.Data.Common
{
    public class Position
    {
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
    }
}