using System;
using _Project.Develop.Runtime.Meta.Features;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.UI.Popups
{
    public class ConfirmPopupPresenter : PopupPresenterBase
    {
        private readonly ConfirmPopupView _view;
        private readonly MainMenuService _mainMenuService;

        protected override PopupViewBase PopupView => _view;

        public ConfirmPopupPresenter(
            ConfirmPopupView view, 
            MainMenuService mainMenuService,
            ICoroutinesPerformer coroutinesPerformer) : base(coroutinesPerformer)
        {
            _view = view;
            _mainMenuService = mainMenuService;
        }

        public override void Initialize()
        {
            base.Initialize();
            _view.ConfirmClicked += OnConfirmClicked;
            _mainMenuService.ProgressReset += OnProgressReset;
        }

        public override void Dispose()
        {
            base.Dispose();
            _view.ConfirmClicked -= OnConfirmClicked;
            _mainMenuService.ProgressReset -= OnProgressReset;
        }

        public void SetMessage(string message) => _view.SetMessage(message);

        private void OnConfirmClicked()
        {
            _mainMenuService.TryResetProgress();
        }

        private void OnProgressReset()
        {
            Hide();
        }
    }
}