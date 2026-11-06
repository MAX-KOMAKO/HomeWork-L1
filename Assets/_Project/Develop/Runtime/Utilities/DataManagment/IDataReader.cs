namespace _Project.Develop.Utilities.DataManagment
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}