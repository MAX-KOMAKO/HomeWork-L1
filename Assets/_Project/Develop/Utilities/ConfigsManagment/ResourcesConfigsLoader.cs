using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Runtime.Meta.Features.Stats;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Utilities.AssetsManagment;

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
                
                if (settingsConfig.WalletConfig != null)
                    configs.Add(typeof(WalletConfig), settingsConfig.WalletConfig);
                    
                if (settingsConfig.ResetProgressConfig != null)
                    configs.Add(typeof(ResetProgressConfig), settingsConfig.ResetProgressConfig);
            }

            yield return null;
            onConfigsLoaded?.Invoke(configs);
        }
    }
}