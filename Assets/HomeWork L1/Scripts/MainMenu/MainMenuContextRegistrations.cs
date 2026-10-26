using Game.DI;
using Game.Input;
using Game.SceneManagement;
using Game.Utilities;

namespace Game.MainMenu
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(c => new MainMenuService(
                c.Resolve<IInputService>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>()
            ));
        }
    }
}