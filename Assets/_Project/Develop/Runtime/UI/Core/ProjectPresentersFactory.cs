using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Meta.Features.Stats;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Runtime.UI.Popups;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.UI.Core
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreenPresenter(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<MainMenuService>(),
                _container.Resolve<MainMenuPopupService>(),
                this);
        }

        public ConfirmPopupPresenter CreateConfirmPopupPresenter(ConfirmPopupView view)
        {
            return new ConfirmPopupPresenter(
                view,
                _container.Resolve<MainMenuService>(),
                _container.Resolve<ICoroutinesPerformer>());
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(
                view,
                _container.Resolve<GameplayService>(),
                _container.Resolve<GameEndService>(),
                _container.Resolve<GameplayPopupService>());
        }

        public GameEndPopupPresenter CreateGameEndPopupPresenter(GameEndPopupView view, bool isVictory)
        {
            return new GameEndPopupPresenter(
                view,
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<IInputSceneArgs>(),
                isVictory);
        }

        public WalletPresenter CreateWalletPresenter(WalletView view)
        {
            return new WalletPresenter(
                view,
                _container.Resolve<WalletService>());
        }

        public StatsPresenter CreateStatsPresenter(StatsView view)
        {
            return new StatsPresenter(
                view,
                _container.Resolve<StatsService>());
        }
    }
}