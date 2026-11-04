namespace _Project.Develop.Utilities.DataManagment
{
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}