using System.Collections;
using Game.Configs;
using Game.DI;
using Game.Input;
using Game.SceneManagement;
using Game.Utilities;
using UnityEngine;

namespace Game.Gameplay
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

        private bool _isGameEnded;
        private bool _isVictory;
        private bool _isLoading;

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
            _gameplayService.Victory += OnVictory;
            _gameplayService.Defeat += OnDefeat;
            
            _gameplayService.StartGame(_currentConfig);
        }

        private void Update()
        {
            if (_isLoading) return;

            if (_isGameEnded == false)
            {
                _gameplayService?.Update();
            }
            else if (_inputService.IsSpacePressed())
            {
                _isLoading = true;
                
                if (_isVictory)
                {
                    _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
                }
                else
                {
                    _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, _inputArgs));
                }
            }
        }

        private void OnVictory()
        {
            _isGameEnded = true;
            _isVictory = true;
        }

        private void OnDefeat()
        {
            _isGameEnded = true;
            _isVictory = false;
        }

        private void OnDestroy()
        {
            if (_gameplayService != null)
            {
                _gameplayService.Victory -= OnVictory;
                _gameplayService.Defeat -= OnDefeat;
            }
        }
    }
}