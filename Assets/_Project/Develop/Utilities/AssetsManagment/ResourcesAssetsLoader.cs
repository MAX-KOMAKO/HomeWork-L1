using UnityEngine;

namespace _Project.Develop.Utilities.AssetsManagment
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string resourcePath) where T : Object
        {
            return Resources.Load<T>(resourcePath);
        }
    }
}