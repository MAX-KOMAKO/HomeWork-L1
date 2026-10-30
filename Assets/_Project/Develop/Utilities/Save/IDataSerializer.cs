namespace _Project.Develop.Utilities.Save
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);
        TData Deserialize<TData>(string serializedData);
    }
}