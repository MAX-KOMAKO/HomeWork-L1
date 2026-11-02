using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Runtime.UI.Popups;
using _Project.Develop.Utilities.Utilities;

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
                _container.Resolve<StatsService>(),
                _container.Resolve<WalletService>(),
                _container.Resolve<MainMenuPopupService>());
        }

        public ConfirmPopupPresenter CreateConfirmPopupPresenter(ConfirmPopupView view)
        {
            return new ConfirmPopupPresenter(
                view,
                _container.Resolve<MainMenuService>(),
                _container.Resolve<ICoroutinesPerformer>());
        }
    }
}