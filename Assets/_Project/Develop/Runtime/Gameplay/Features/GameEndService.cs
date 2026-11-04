using System;
using _Project.Develop.Utilities.DataManagment;
using _Project.Develop.Runtime.Meta.Features.Stats;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.Gameplay.Features
{
    public class GameEndService : IDisposable
    {
        public event Action<bool> GameEnded;

        private readonly GameplayService _gameplayService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly WalletService _walletService;
        private readonly StatsService _statsService;
        private readonly PlayerDataProvider _playerDataProvider;

        public GameEndService(
            GameplayService gameplayService,
            ICoroutinesPerformer coroutinesPerformer,
            WalletService walletService,
            StatsService statsService,
            PlayerDataProvider playerDataProvider)
        {
            _gameplayService = gameplayService;
            _coroutinesPerformer = coroutinesPerformer;
            _walletService = walletService;
            _statsService = statsService;
            _playerDataProvider = playerDataProvider;

            _gameplayService.Won += OnVictory;
            _gameplayService.Lost += OnDefeat;
        }

        public void Dispose()
        {
            _gameplayService.Won -= OnVictory;
            _gameplayService.Lost -= OnDefeat;
        }

        private void OnVictory()
        {
            _statsService.AddWin();
            _walletService.RewardForWin();
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
            GameEnded?.Invoke(true);
        }

        private void OnDefeat()
        {
            _statsService.AddLoss();
            _walletService.PenalizeForLoss();
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
            GameEnded?.Invoke(false);
        }
    }
}