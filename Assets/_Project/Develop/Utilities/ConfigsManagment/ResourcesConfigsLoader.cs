using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Utilities.Utilities;

namespace _Project.Develop.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private const string SettingsConfigPath = "Configs/GameSettingsConfig";

        private readonly ResourcesAssetsLoader _assetsLoader;

        public ResourcesConfigsLoader(ResourcesAssetsLoader assetsLoader)
        {
            _assetsLoader = assetsLoader;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            GameSettingsConfig settingsConfig = _assetsLoader.Load<GameSettingsConfig>(SettingsConfigPath);
            Dictionary<Type, object> configs = new();

            if (settingsConfig != null)
            {
                configs.Add(typeof(GameSettingsConfig), settingsConfig);
            }

            yield return null;
            onConfigsLoaded?.Invoke(configs);
        }
    }
}