using UnityEngine;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPopupService : PopupService
    {
        private readonly Transform _popupLayer;

        public GameplayPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory presentersFactory,
            Transform popupLayer) : base(viewsFactory, presentersFactory)
        {
            _popupLayer = popupLayer;
        }

        protected override Transform PopupLayer => _popupLayer;

        public void OpenGameEndPopup(bool isVictory)
        {
            GameEndPopupView view = ViewsFactory.Create<GameEndPopupView>(ViewIDs.GameEndPopup, PopupLayer);
            GameEndPopupPresenter presenter = PresentersFactory.CreateGameEndPopupPresenter(view, isVictory);
            OnPopupCreated(presenter, view);
        }
    }
}