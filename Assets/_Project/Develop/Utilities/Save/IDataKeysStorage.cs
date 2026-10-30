namespace _Project.Develop.Utilities.Save
{
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}