using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace _Project.Develop.Utilities.Save
{
    public class LocalFileDataRepository : IDataRepository
    {
        private readonly string _saveFolderPath;

        public LocalFileDataRepository()
        {
            _saveFolderPath = Application.persistentDataPath;
        }

        public IEnumerator Read(string key, Action<string> onRead)
        {
            string filePath = Path.Combine(_saveFolderPath, key);
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                onRead?.Invoke(data);
            }
            else
            {
                onRead?.Invoke(null);
            }
            yield break;
        }

        public IEnumerator Write(string key, string serializedData)
        {
            string filePath = Path.Combine(_saveFolderPath, key);
            File.WriteAllText(filePath, serializedData);
            yield break;
        }

        public IEnumerator Remove(string key)
        {
            string filePath = Path.Combine(_saveFolderPath, key);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            yield break;
        }

        public IEnumerator Exists(string key, Action<bool> onExistsResult)
        {
            string filePath = Path.Combine(_saveFolderPath, key);
            onExistsResult?.Invoke(File.Exists(filePath));
            yield break;
        }
    }
}