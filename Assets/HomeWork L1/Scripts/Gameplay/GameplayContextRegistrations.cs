using Game.DI;
using Game.Input;
using Game.Utilities;

namespace Game.Gameplay
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
        }
    }
}