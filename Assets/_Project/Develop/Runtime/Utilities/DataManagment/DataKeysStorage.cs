using System;
using System.Collections.Generic;

namespace _Project.Develop.Utilities.DataManagment
{
    public class DataKeysStorage : IDataKeysStorage
    {
        private const string PlayerDataKey = "PlayerData.json";

        private readonly Dictionary<Type, string> _keys = new()
        {
            { typeof(PlayerData), PlayerDataKey }
        };

        public string GetKeyFor<TData>() where TData : ISaveData
        {
            return _keys[typeof(TData)];
        }
    }
}