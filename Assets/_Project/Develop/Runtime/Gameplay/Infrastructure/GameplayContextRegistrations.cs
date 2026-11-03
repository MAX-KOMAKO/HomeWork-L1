using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public static class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args, Transform popupLayer)
        {
            container.RegisterAsSingle<IInputSceneArgs>(c => args);
            container.RegisterAsSingle(c => new SequenceGenerator());
            container.RegisterAsSingle(c => new GameplayService(
                c.Resolve<IInputService>(),
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
        }
    }
}