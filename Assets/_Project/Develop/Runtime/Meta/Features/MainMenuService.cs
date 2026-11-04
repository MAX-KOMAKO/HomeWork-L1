using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Utilities.DataManagment;
using _Project.Develop.Runtime.Meta.Features.Stats;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.Meta.Features
{
    public class MainMenuService
    {
        public event Action ProgressReset;

        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly WalletService _walletService;
        private readonly StatsService _statsService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ResetProgressConfig _resetConfig;

        private bool _isLoading;

        public MainMenuService(
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            WalletService walletService,
            StatsService statsService,
            PlayerDataProvider playerDataProvider,
            ResetProgressConfig resetConfig)
        {
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _walletService = walletService;
            _statsService = statsService;
            _playerDataProvider = playerDataProvider;
            _resetConfig = resetConfig;
        }

        public void StartGame(GameModes mode)
        {
            if (_isLoading) return;
            _isLoading = true;

            GameplayInputArgs args = new GameplayInputArgs(mode);
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, args));
        }

        public void TryResetProgress()
        {
            if (_isLoading) return;

            int cost = _resetConfig.ResetCost;

            if (_walletService.Enough(CurrencyTypes.Coins, cost))
            {
                _walletService.Spend(CurrencyTypes.Coins, cost);
                _isLoading = true;
                _coroutinesPerformer.StartPerform(ResetProgressProcess());
            }
        }

        public bool CanResetProgress()
        {
            return _walletService.Enough(CurrencyTypes.Coins, _resetConfig.ResetCost);
        }

        public int GetResetCost()
        {
            return _resetConfig.ResetCost;
        }

        private IEnumerator ResetProgressProcess()
        {
            _statsService.ResetStats();
            
            yield return _playerDataProvider.SaveAsync();
            
            _isLoading = false;
            ProgressReset?.Invoke();
        }
    }
}