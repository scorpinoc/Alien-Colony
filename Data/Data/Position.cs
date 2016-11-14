namespace Data.Data
{
    public class Position
    {
        public uint X { get; set; }
        public uint Y { get; set; }

        public Position()
            : this(0, 0)
        { }

        public Position(uint x, uint y)
        {
            X = x;
            Y = y;
        }
    }
}