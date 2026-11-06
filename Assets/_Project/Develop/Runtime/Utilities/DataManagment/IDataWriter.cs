namespace _Project.Develop.Utilities.DataManagment
{
    public interface IDataWriter<TData> where TData : ISaveData
    {
        void WriteTo(TData data);
    }
}