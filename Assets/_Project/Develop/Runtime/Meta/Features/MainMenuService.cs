using System.Collections;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;

namespace _Project.Develop.Runtime.Meta.Features
{
    public class MainMenuService
    {
        private const char DigitsModeKey = '1';
        private const char LettersModeKey = '2';
        private const char StatsKey = '3';
        private const char ResetKey = '4';

        private readonly IInputService _inputService;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        
        private readonly WalletService _walletService;
        private readonly StatsService _statsService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ResetProgressConfig _resetConfig;
        private readonly IConsoleService _consoleService;

        private bool _isLoading;

        public MainMenuService(
            IInputService inputService,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            WalletService walletService,
            StatsService statsService,
            PlayerDataProvider playerDataProvider,
            ResetProgressConfig resetConfig,
            IConsoleService consoleService)
        {
            _inputService = inputService;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _walletService = walletService;
            _statsService = statsService;
            _playerDataProvider = playerDataProvider;
            _resetConfig = resetConfig;
            _consoleService = consoleService;
        }

        public void Update()
        {
            if (_isLoading) return;

            string input = _inputService.GetTypedInput();

            if (string.IsNullOrEmpty(input) == false)
            {
                char character = char.ToLowerInvariant(input[0]);

                if (character == DigitsModeKey)
                {
                    StartGame(GameModes.Digits);
                }
                else if (character == LettersModeKey)
                {
                    StartGame(GameModes.Letters);
                }
                else if (character == StatsKey)
                {
                    PrintStats();
                }
                else if (character == ResetKey)
                {
                    TryResetProgress();
                }
            }
        }

        private void StartGame(GameModes mode)
        {
            _isLoading = true;
            GameplayInputArgs args = new GameplayInputArgs(mode);
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, args));
        }

        private void PrintStats()
        {
            _consoleService.Log($"Статистика: Победы - {_statsService.Wins}, Поражения - {_statsService.Losses}, Золото - {_walletService.CurrentGold}");
        }

        private void TryResetProgress()
        {
            int cost = _resetConfig.ResetCost;
            
            if (_walletService.CanSpend(cost))
            {
                _isLoading = true;
                _coroutinesPerformer.StartPerform(ResetProgressProcess(cost));
            }
            else
            {
                _consoleService.Log($"Недостаточно золота для сброса. Требуется: {cost}, у вас: {_walletService.CurrentGold}");
            }
        }

        private IEnumerator ResetProgressProcess(int cost)
        {
            _walletService.SpendGold(cost);
            _statsService.ResetStats();
            
            yield return _playerDataProvider.SaveAsync();
            
            _isLoading = false;
            _consoleService.Log($"Прогресс сброшен! Списано {cost} золота.");
        }
    }
}