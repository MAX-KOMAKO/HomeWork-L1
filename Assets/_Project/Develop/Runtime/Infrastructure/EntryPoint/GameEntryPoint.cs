using System.Collections;
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