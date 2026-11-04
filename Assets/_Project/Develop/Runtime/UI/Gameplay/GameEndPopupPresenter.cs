using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Utilities.SceneManagement;
using _Project.Develop.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameEndPopupPresenter : PopupPresenterBase
    {
        private const string VictoryMessage = "Победа!";
        private const string DefeatMessage = "Поражение!";

        private readonly GameEndPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly IInputSceneArgs _inputArgs;
        private readonly bool _isVictory;

        protected override PopupViewBase PopupView => _view;

        public GameEndPopupPresenter(
            GameEndPopupView view,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            IInputSceneArgs inputArgs,
            bool isVictory) : base(coroutinesPerformer)
        {
            _view = view;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _inputArgs = inputArgs;
            _isVictory = isVictory;
        }

        public override void Initialize()
        {
            base.Initialize();
            _view.MenuClicked += OnMenuClicked;
            _view.RestartClicked += OnRestartClicked;

            string message = _isVictory ? VictoryMessage : DefeatMessage;
            _view.SetMessage(message);
        }

        public override void Dispose()
        {
            base.Dispose();
            _view.MenuClicked -= OnMenuClicked;
            _view.RestartClicked -= OnRestartClicked;
        }

        private void OnMenuClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
        }

        private void OnRestartClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, _inputArgs));
        }
    }
}