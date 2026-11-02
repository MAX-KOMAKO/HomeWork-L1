using UnityEngine;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Popups;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPopupService : PopupService
    {
        private readonly Transform _popupLayer;

        public MainMenuPopupService(
            ViewsFactory viewsFactory, 
            ProjectPresentersFactory presentersFactory,
            Transform popupLayer) : base(viewsFactory, presentersFactory)
        {
            _popupLayer = popupLayer;
        }

        protected override Transform PopupLayer => _popupLayer;

        public ConfirmPopupPresenter OpenConfirmPopup(string message)
        {
            ConfirmPopupView view = ViewsFactory.Create<ConfirmPopupView>(ViewIDs.ConfirmPopup, PopupLayer);
            ConfirmPopupPresenter presenter = PresentersFactory.CreateConfirmPopupPresenter(view);
            
            presenter.SetMessage(message);
            OnPopupCreated(presenter, view);
            
            return presenter;
        }
    }
}