using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;

namespace _Project.Develop.Runtime.Meta.Infrastructure
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