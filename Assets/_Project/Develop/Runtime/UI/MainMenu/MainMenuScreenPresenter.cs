using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        private readonly MainMenuService _mainMenuService;
        private readonly StatsService _statsService;
        private readonly WalletService _walletService;
        private readonly MainMenuPopupService _popupService;

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            MainMenuService mainMenuService,
            StatsService statsService,
            WalletService walletService,
            MainMenuPopupService popupService)
        {
            _screen = screen;
            _mainMenuService = mainMenuService;
            _statsService = statsService;
            _walletService = walletService;
            _popupService = popupService;
        }

        public void Initialize()
        {
            _screen.DigitsButtonClicked += OnDigitsButtonClicked;
            _screen.LettersButtonClicked += OnLettersButtonClicked;
            _screen.ResetButtonClicked += OnResetButtonClicked;

            _statsService.StatsChanged += OnStatsChanged;
            _walletService.GoldChanged += OnGoldChanged;

            UpdateStatsView();
            UpdateGoldView();
        }

        public void Dispose()
        {
            _screen.DigitsButtonClicked -= OnDigitsButtonClicked;
            _screen.LettersButtonClicked -= OnLettersButtonClicked;
            _screen.ResetButtonClicked -= OnResetButtonClicked;

            _statsService.StatsChanged -= OnStatsChanged;
            _walletService.GoldChanged -= OnGoldChanged;
        }

        private void OnDigitsButtonClicked()
        {
            _mainMenuService.StartGame(GameModes.Digits);
        }

        private void OnLettersButtonClicked()
        {
            _mainMenuService.StartGame(GameModes.Letters);
        }

        private void OnResetButtonClicked()
        {
            if (_mainMenuService.CanResetProgress())
            {
                string message = $"Сбросить прогресс за {_mainMenuService.GetResetCost()} золота?";
                _popupService.OpenConfirmPopup(message);
            }
        }

        private void OnStatsChanged(int wins, int losses)
        {
            _screen.SetStats(wins, losses);
        }

        private void OnGoldChanged(int gold)
        {
            _screen.SetGold(gold);
        }

        private void UpdateStatsView()
        {
            _screen.SetStats(_statsService.Wins, _statsService.Losses);
        }

        private void UpdateGoldView()
        {
            _screen.SetGold(_walletService.CurrentGold);
        }
    }
}