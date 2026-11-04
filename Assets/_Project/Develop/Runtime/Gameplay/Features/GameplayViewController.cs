using _Project.Develop.Runtime.Gameplay.Infrastructure;

namespace _Project.Develop.Runtime.Gameplay.Features
{
    public class GameplayViewController
    {
        private readonly ControllersUpdateService _controllersUpdateService;

        public GameplayViewController(ControllersUpdateService controllersUpdateService)
        {
            _controllersUpdateService = controllersUpdateService;
        }

        public void Update()
        {
            _controllersUpdateService.Update();
        }
    }
}