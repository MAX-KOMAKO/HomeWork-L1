using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private Transform _popupLayer;

        private DIContainer _container;
        private MainMenuScreenPresenter _mainMenuPresenter;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;
            MainMenuContextRegistrations.Process(_container, _popupLayer);
        }

        public override IEnumerator Initialize()
        {
            yield break;
        }

        public override void Run()
        {
            ViewsFactory viewsFactory = _container.Resolve<ViewsFactory>();
            ProjectPresentersFactory presentersFactory = _container.Resolve<ProjectPresentersFactory>();

            MainMenuScreenView view = viewsFactory.Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, _uiRoot);
            
            _mainMenuPresenter = presentersFactory.CreateMainMenuScreenPresenter(view);
            _mainMenuPresenter.Initialize();
        }
    }
}