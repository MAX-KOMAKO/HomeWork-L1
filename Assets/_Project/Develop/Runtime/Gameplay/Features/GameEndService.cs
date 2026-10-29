using System;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;

namespace _Project.Develop.Runtime.Gameplay.Features
{
    public class GameEndService : IDisposable
    {
        private readonly GameplayService _gameplayService;
        private readonly IInputService _inputService;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayInputArgs _inputArgs;

        private bool _isGameEnded;
        private bool _isVictory;
        private bool _isLoading;

        public GameEndService(
            GameplayService gameplayService,
            IInputService inputService,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            GameplayInputArgs inputArgs)
        {
            _gameplayService = gameplayService;
            _inputService = inputService;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _inputArgs = inputArgs;

            _gameplayService.Victory += OnVictory;
            _gameplayService.Defeat += OnDefeat;
        }

        public void Update()
        {
            if (_isLoading) return;

            if (_isGameEnded == false)
            {
                _gameplayService.Update();
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

        public void Dispose()
        {
            _gameplayService.Victory -= OnVictory;
            _gameplayService.Defeat -= OnDefeat;
        }
    }
}