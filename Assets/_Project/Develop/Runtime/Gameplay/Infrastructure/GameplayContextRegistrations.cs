using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Utilities.Utilities;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Utilities.AssetsManagment;
using _Project.Develop.Utilities.DataManagment;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.CoroutinesManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public static class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(c => new SequenceGenerator());
            container.RegisterAsSingle<IConsoleService>(c => new UnityConsoleService());
            container.RegisterAsSingle(c => new GameplayService(
                c.Resolve<IInputService>(),
                c.Resolve<IConsoleService>(),
                c.Resolve<SequenceGenerator>()
            ));
            
            container.RegisterAsSingle(c => new GameEndService(
                c.Resolve<GameplayService>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<WalletService>(),
                c.Resolve<StatsService>(),
                c.Resolve<PlayerDataProvider>()
            ));

            container.RegisterAsSingle(c => new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                popupLayer
            ));

            container.RegisterAsSingle(c => new ProjectPresentersFactory(c));
            container.RegisterAsSingle(c => new ControllersUpdateService());

            container.RegisterAsSingle(c => new EntitiesLifeContext());
            container.RegisterAsSingle(c => new MonoEntitiesFactory(c.Resolve<ResourcesAssetsLoader>()));
            container.RegisterAsSingle(c => new EntitiesFactory(c));
        }
    }
}