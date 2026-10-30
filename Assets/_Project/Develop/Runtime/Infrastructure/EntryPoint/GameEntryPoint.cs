using System.Collections;
using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Utilities.ConfigsManagment;
using _Project.Develop.Utilities.LoadingScreen;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private const int TargetFrameRate = 60;
        private const int VSyncCount = 0;

        private DIContainer _projectContainer;

        private void Awake()
        {
            Application.targetFrameRate = TargetFrameRate;
            QualitySettings.vSyncCount = VSyncCount;

            _projectContainer = new DIContainer();
            ProjectContextRegistrations.Process(_projectContainer);
            _projectContainer.Initialize();
        }

        private IEnumerator Start()
        {
            ILoadingScreen loadingScreen = _projectContainer.Resolve<ILoadingScreen>();
            loadingScreen.Show();

            ConfigsProviderService configsProvider = _projectContainer.Resolve<ConfigsProviderService>();
            yield return configsProvider.LoadAsync();

            PlayerDataProvider playerDataProvider = _projectContainer.Resolve<PlayerDataProvider>();
            WalletService walletService = _projectContainer.Resolve<WalletService>();
            StatsService statsService = _projectContainer.Resolve<StatsService>();

            playerDataProvider.RegisterReader(walletService);
            playerDataProvider.RegisterWriter(walletService);
            playerDataProvider.RegisterReader(statsService);
            playerDataProvider.RegisterWriter(statsService);

            yield return playerDataProvider.LoadAsync();
            yield return playerDataProvider.SaveAsync();

            SceneSwitcherService sceneSwitcher = _projectContainer.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _projectContainer.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
        }
    }
}