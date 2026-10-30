using _Project.Develop.Utilities.Save;

namespace _Project.Develop.Runtime.Gameplay.Features.Save
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}