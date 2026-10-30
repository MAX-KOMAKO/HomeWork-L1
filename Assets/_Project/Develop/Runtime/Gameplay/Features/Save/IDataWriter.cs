using _Project.Develop.Utilities.Save;

namespace _Project.Develop.Runtime.Gameplay.Features.Save
{
    public interface IDataWriter<TData> where TData : ISaveData
    {
        void WriteTo(TData data);
    }
}