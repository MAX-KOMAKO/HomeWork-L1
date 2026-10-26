using UnityEngine;

namespace Game.Utilities
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string resourcePath) where T : Object
        {
            return Resources.Load<T>(resourcePath);
        }
    }
}