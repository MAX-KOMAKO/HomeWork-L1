using System;
using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
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
        private readonly IConsoleService _consoleService;
        
        private readonly WalletService _walletService;
        private readonly StatsService _statsService;
        private readonly PlayerDataProvider _playerDataProvider;

        private bool _isGameEnded;
        private bool _isVictory;
        private bool _isLoading;

        public GameEndService(
            GameplayService gameplayService,
            IInputService inputService,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            GameplayInputArgs inputArgs,
            IConsoleService consoleService,
            WalletService walletService,
            StatsService statsService,
            PlayerDataProvider playerDataProvider)
        {
            _gameplayService = gameplayService;
            _inputService = inputService;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _inputArgs = inputArgs;
            _consoleService = consoleService;
            _walletService = walletService;
            _statsService = statsService;
            _playerDataProvider = playerDataProvider;

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
            
            _statsService.AddWin();
            _walletService.RewardForWin();
            _consoleService.Log("Победа! Нажмите пробел для выхода в меню.");
            
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
        }

        private void OnDefeat()
        {
            _isGameEnded = true;
            _isVictory = false;
            
            _statsService.AddLoss();
            _walletService.PenalizeForLoss();
            _consoleService.Log("Поражение! Нажмите пробел для перезапуска.");
            
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
        }

        public void Dispose()
        {
            _gameplayService.Victory -= OnVictory;
            _gameplayService.Defeat -= OnDefeat;
        }
    }
}