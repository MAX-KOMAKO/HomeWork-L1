using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Utilities.ConfigsManagment;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container, Transform popupLayer)
        {
            container.RegisterAsSingle(c => new MainMenuService(
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<WalletService>(),
                c.Resolve<StatsService>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<ConfigsProviderService>().GetConfig<ResetProgressConfig>()
            ));

            container.RegisterAsSingle(c => new ProjectPresentersFactory(c));

            container.RegisterAsSingle(c => new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                popupLayer));
        }
    }
}