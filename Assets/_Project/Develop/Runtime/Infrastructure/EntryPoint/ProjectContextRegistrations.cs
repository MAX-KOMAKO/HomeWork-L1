using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Utilities.ConfigsManagment;
using _Project.Develop.Utilities.LoadingScreen;
using _Project.Develop.Utilities.Save;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public static class ProjectContextRegistrations
    {
        private const string CoroutinesPerformerPath = "Utilities/CoroutinesPerformer";
        private const string LoadingScreenPath = "Utilities/StandardLoadingScreen";

        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
            container.RegisterAsSingle(CreateCoroutinesPerformer);
            container.RegisterAsSingle<ICoroutinesPerformer>(c => c.Resolve<CoroutinesPerformer>());
            container.RegisterAsSingle(CreateLoadingScreen);
            container.RegisterAsSingle(CreateConfigsProviderService);
            container.RegisterAsSingle(CreateSceneLoaderService);
            container.RegisterAsSingle(CreateSceneSwitcherService);
            container.RegisterAsSingle<IInputService>(c => new UnityInputService());
            container.RegisterAsSingle<IConsoleService>(c => new UnityConsoleService());

            container.RegisterAsSingle<IDataSerializer>(c => new JsonDataSerializer());
            container.RegisterAsSingle<IDataRepository>(c => new LocalFileDataRepository());
            container.RegisterAsSingle<IDataKeysStorage>(c => new DataKeysStorage());
            container.RegisterAsSingle(CreateSaveLoadService);
            
            container.RegisterAsSingle(CreatePlayerDataProvider);
            container.RegisterAsSingle(CreateWalletService);
            container.RegisterAsSingle(c => new StatsService());
        }

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => new ResourcesAssetsLoader();

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader loader = c.Resolve<ResourcesAssetsLoader>();
            CoroutinesPerformer prefab = loader.Load<CoroutinesPerformer>(CoroutinesPerformerPath);
            return Object.Instantiate(prefab);
        }

        private static ILoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader loader = c.Resolve<ResourcesAssetsLoader>();
            StandardLoadingScreen prefab = loader.Load<StandardLoadingScreen>(LoadingScreenPath);
            return Object.Instantiate(prefab);
        }

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader loader = c.Resolve<ResourcesAssetsLoader>();
            ResourcesConfigsLoader configsLoader = new ResourcesConfigsLoader(loader);
            return new ConfigsProviderService(configsLoader);
        }

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c) => new SceneLoaderService();

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
        {
            SceneLoaderService sceneLoader = c.Resolve<SceneLoaderService>();
            ILoadingScreen loadingScreen = c.Resolve<ILoadingScreen>();
            return new SceneSwitcherService(sceneLoader, loadingScreen, c);
        }

        private static SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            return new SaveLoadService(
                c.Resolve<IDataSerializer>(),
                c.Resolve<IDataRepository>(),
                c.Resolve<IDataKeysStorage>()
            );
        }

        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
        {
            ConfigsProviderService configsProvider = c.Resolve<ConfigsProviderService>();
            WalletConfig walletConfig = configsProvider.GetConfig<WalletConfig>();
            return new PlayerDataProvider(c.Resolve<SaveLoadService>(), walletConfig);
        }

        private static WalletService CreateWalletService(DIContainer c)
        {
            ConfigsProviderService configsProvider = c.Resolve<ConfigsProviderService>();
            WalletConfig walletConfig = configsProvider.GetConfig<WalletConfig>();
            return new WalletService(walletConfig);
        }
    }
}