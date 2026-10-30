using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Utilities.Utilities;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public static class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(c => new SequenceGenerator());
            container.RegisterAsSingle(c => new GameplayService(
                c.Resolve<IInputService>(),
                c.Resolve<IConsoleService>(),
                c.Resolve<SequenceGenerator>()
            ));
        }
    }
}