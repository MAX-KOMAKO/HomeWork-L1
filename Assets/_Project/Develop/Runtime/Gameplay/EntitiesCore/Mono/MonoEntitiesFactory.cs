using UnityEngine;
using Object = UnityEngine.Object;
using _Project.Develop.Utilities.AssetsManagment;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntitiesFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;

        public MonoEntitiesFactory(ResourcesAssetsLoader resourcesAssetsLoader)
        {
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }

        public MonoEntity Create(Entity entity, Vector3 position, string path)
        {
            MonoEntity prefab = _resourcesAssetsLoader.Load<MonoEntity>(path);
            
            if (prefab == null)
            {
                Debug.LogError($"Prefab not found at path: {path}");
                return null;
            }

            MonoEntity instance = Object.Instantiate(prefab, position, Quaternion.identity);
            instance.Setup(entity);
            
            return instance;
        }
    }
}