using System.Collections;
using Game.Configs;
using Game.DI;
using Game.Loading;
using Game.SceneManagement;
using Game.Utilities;
using UnityEngine;

namespace Game.GameEntryPoint
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
        }

        private IEnumerator Start()
        {
            ILoadingScreen loadingScreen = _projectContainer.Resolve<ILoadingScreen>();
            loadingScreen.Show();

            ConfigsProviderService configsProvider = _projectContainer.Resolve<ConfigsProviderService>();
            yield return configsProvider.LoadAsync();

            SceneSwitcherService sceneSwitcher = _projectContainer.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _projectContainer.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
        }
    }
}