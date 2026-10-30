using System;
using System.Collections;

namespace _Project.Develop.Utilities.Save
{
    public class SaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataRepository _repository;
        private readonly IDataKeysStorage _keysStorage;

        public SaveLoadService(
            IDataSerializer serializer,
            IDataRepository repository,
            IDataKeysStorage keysStorage)
        {
            _serializer = serializer;
            _repository = repository;
            _keysStorage = keysStorage;
        }

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(data);
            string key = _keysStorage.GetKeyFor<TData>();
            yield return _repository.Write(key, serializedData);
        }

        public IEnumerator Load<TData>(Action<TData> onLoaded) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();
            yield return _repository.Read(key, serializedData =>
            {
                if (string.IsNullOrEmpty(serializedData))
                {
                    onLoaded?.Invoke(default);
                    return;
                }

                TData data = _serializer.Deserialize<TData>(serializedData);
                onLoaded?.Invoke(data);
            });
        }

        public IEnumerator Delete<TData>() where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();
            yield return _repository.Remove(key);
        }
    }
}