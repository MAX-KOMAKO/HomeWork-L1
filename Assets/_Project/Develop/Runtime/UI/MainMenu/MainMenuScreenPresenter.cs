using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.CommonViews;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        private readonly MainMenuService _mainMenuService;
        private readonly MainMenuPopupService _popupService;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            MainMenuService mainMenuService,
            MainMenuPopupService popupService,
            ProjectPresentersFactory presentersFactory)
        {
            _screen = screen;
            _mainMenuService = mainMenuService;
            _popupService = popupService;
            _presentersFactory = presentersFactory;
        }

        public void Initialize()
        {
            _screen.DigitsButtonClicked += OnDigitsButtonClicked;
            _screen.LettersButtonClicked += OnLettersButtonClicked;
            _screen.ResetButtonClicked += OnResetButtonClicked;

            CreateChildPresenters();
        }

        public void Dispose()
        {
            _screen.DigitsButtonClicked -= OnDigitsButtonClicked;
            _screen.LettersButtonClicked -= OnLettersButtonClicked;
            _screen.ResetButtonClicked -= OnResetButtonClicked;

            foreach (IPresenter presenter in _childPresenters)
            {
                presenter.Dispose();
            }

            _childPresenters.Clear();
        }

        private void CreateChildPresenters()
        {
            WalletPresenter walletPresenter = _presentersFactory.CreateWalletPresenter(_screen.WalletView);
            StatsPresenter statsPresenter = _presentersFactory.CreateStatsPresenter(_screen.StatsView);

            _childPresenters.Add(walletPresenter);
            _childPresenters.Add(statsPresenter);

            foreach (IPresenter presenter in _childPresenters)
            {
                presenter.Initialize();
            }
        }

        private void OnDigitsButtonClicked() => _mainMenuService.StartGame(GameModes.Digits);
        private void OnLettersButtonClicked() => _mainMenuService.StartGame(GameModes.Letters);

        private void OnResetButtonClicked()
        {
            if (_mainMenuService.CanResetProgress())
            {
                string message = $"Сбросить прогресс за {_mainMenuService.GetResetCost()} золота?";
                _popupService.OpenConfirmPopup(message);
            }
        }
    }
}