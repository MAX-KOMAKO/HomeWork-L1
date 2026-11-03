using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Runtime.Gameplay.Features.Save;
using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Utilities.ConfigsManagment;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.Utilities;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        [SerializeField] private Transform _uiRoot;
        [SerializeField] private Transform _popupLayer;

        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameplayService _gameplayService;
        private GameplayScreenPresenter _gameplayScreenPresenter;
        private GameModeConfig _currentConfig;
        private ControllersUpdateService _controllersUpdateService;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
            {
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");
            }

            _inputArgs = gameplayInputArgs;
            GameplayContextRegistrations.Process(_container, _inputArgs, _popupLayer);
        }

        public override IEnumerator Initialize()
        {
            ConfigsProviderService configsProvider = _container.Resolve<ConfigsProviderService>();
            GameSettingsConfig settingsConfig = configsProvider.GetConfig<GameSettingsConfig>();

            foreach (GameModeConfig config in settingsConfig.GameModes)
            {
                if (config.Mode == _inputArgs.Mode)
                {
                    _currentConfig = config;
                    break;
                }
            }

            if (_currentConfig == null)
            {
                throw new InvalidOperationException($"Config for mode {_inputArgs.Mode} not found in GameSettingsConfig.");
            }

            _gameplayService = _container.Resolve<GameplayService>();
            _controllersUpdateService = _container.Resolve<ControllersUpdateService>();
            yield break;
        }

        public override void Run()
        {
            ViewsFactory viewsFactory = _container.Resolve<ViewsFactory>();
            ProjectPresentersFactory presentersFactory = _container.Resolve<ProjectPresentersFactory>();

            GameplayScreenView view = viewsFactory.Create<GameplayScreenView>(ViewIDs.GameplayScreen, _uiRoot);

            _gameplayScreenPresenter = presentersFactory.CreateGameplayScreenPresenter(view);
            _gameplayScreenPresenter.Initialize();

            _gameplayService.StartGame(_currentConfig);

            Action updateAction = () => _gameplayScreenPresenter.Update();
            _controllersUpdateService.Add(updateAction);
        }

        private void Update()
        {
            _controllersUpdateService?.Update();
        }
    }
}