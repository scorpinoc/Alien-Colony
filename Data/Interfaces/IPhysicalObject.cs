using Data.Data.Common;

namespace Data.Interfaces
{
    public interface IPhysicalObject : INameble
    {
        Position Position { get; }
        ObjectType ObjectType { get; }
    }

    public enum ObjectType
    {
        Unit,
        Building,
    }
}