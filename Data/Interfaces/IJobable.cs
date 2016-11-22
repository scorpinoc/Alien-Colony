using Data.Data;

namespace Data.Interfaces
{
    public interface IJobable : INameble
    {
        void Work(Colonist worker);
    }
}
