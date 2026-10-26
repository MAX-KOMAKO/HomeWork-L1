using System;
using System.Collections;
using System.Collections.Generic;
using Game.Utilities;

namespace Game.Configs
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