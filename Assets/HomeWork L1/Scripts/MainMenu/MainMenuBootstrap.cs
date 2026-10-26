using System.Collections;
using Game.DI;
using Game.SceneManagement;
using UnityEngine;

namespace Game.MainMenu
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
        }

        private void Update()
        {
            _mainMenuService?.Update();
        }
    }
}