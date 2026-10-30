using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Utilities.Save;

namespace _Project.Develop.Runtime.Gameplay.Features.Save
{
    public abstract class DataProvider<TData> where TData : ISaveData
    {
        private readonly SaveLoadService _saveLoadService;
        private readonly List<IDataReader<TData>> _readers = new();
        private readonly List<IDataWriter<TData>> _writers = new();

        protected TData Data { get; private set; }

        protected DataProvider(SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void RegisterReader(IDataReader<TData> reader)
        {
            _readers.Add(reader);
            if (Data != null)
            {
                reader.ReadFrom(Data);
            }
        }

        public void RegisterWriter(IDataWriter<TData> writer)
        {
            _writers.Add(writer);
        }

        public IEnumerator LoadAsync()
        {
            yield return _saveLoadService.Load<TData>(loadedData =>
            {
                Data = loadedData ?? GetOriginData();
            });

            foreach (IDataReader<TData> reader in _readers)
            {
                reader.ReadFrom(Data);
            }
        }

        public IEnumerator SaveAsync()
        {
            UpdateDataFromWriters();
            yield return _saveLoadService.Save(Data);
        }

        public IEnumerator ResetAsync()
        {
            Data = GetOriginData();
            
            foreach (IDataReader<TData> reader in _readers)
            {
                reader.ReadFrom(Data);
            }
            
            yield return _saveLoadService.Save(Data);
        }

        private void UpdateDataFromWriters()
        {
            foreach (IDataWriter<TData> writer in _writers)
            {
                writer.WriteTo(Data);
            }
        }

        protected abstract TData GetOriginData();
    }
}