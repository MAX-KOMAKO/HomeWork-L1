using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MainMenuService _mainMenuService;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;
            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            yield break;
        }

        public override void Run()
        {
            _mainMenuService = _container.Resolve<MainMenuService>();

            GameObject controllerObject = new GameObject("MainMenuController");
            MainMenuController controller = controllerObject.AddComponent<MainMenuController>();
            controller.Initialize(_mainMenuService);
        }
    }
}