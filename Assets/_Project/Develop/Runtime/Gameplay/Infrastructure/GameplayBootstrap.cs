using System.Collections;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Utilities.ConfigsManagment;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameplayService _gameplayService;
        private SceneSwitcherService _sceneSwitcher;
        private ICoroutinesPerformer _coroutinesPerformer;
        private IInputService _inputService;
        private GameModeConfig _currentConfig;

        private GameplayViewController _viewController;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
            {
                throw new System.ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");
            }

            _inputArgs = gameplayInputArgs;
            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            _sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            _inputService = _container.Resolve<IInputService>();
            
            ConfigsProviderService configsProvider = _container.Resolve<ConfigsProviderService>();
            GameSettingsConfig settingsConfig = configsProvider.GetConfig<GameSettingsConfig>();
            
            foreach (GameModeConfig config in settingsConfig.GameModes)
            {
                if (config.Mode == _inputArgs.Mode)
                {
                    _currentConfig = config;
                    break;
                }
            }

            if (_currentConfig == null)
            {
                throw new System.InvalidOperationException($"Config for mode {_inputArgs.Mode} not found in GameSettingsConfig.");
            }

            _gameplayService = _container.Resolve<GameplayService>();
            
            yield break;
        }

        public override void Run()
        {
            _viewController = new GameplayViewController(
                _gameplayService,
                _inputService,
                _sceneSwitcher,
                _coroutinesPerformer,
                _inputArgs
            );

            _gameplayService.StartGame(_currentConfig);
        }

        private void Update()
        {
            _viewController?.Update();
        }

        private void OnDestroy()
        {
            _viewController?.Dispose();
        }
    }
}