using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.Save;

namespace _Project.Develop.Utilities.Save
{
    public class DataKeysStorage : IDataKeysStorage
    {
        private readonly Dictionary<Type, string> _keys = new()
        {
            { typeof(PlayerData), "PlayerData.json" }
        };

        public string GetKeyFor<TData>() where TData : ISaveData
        {
            return _keys[typeof(TData)];
        }
    }
}