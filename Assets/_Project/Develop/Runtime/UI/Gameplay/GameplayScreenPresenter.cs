using _Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _view;
        private readonly GameplayService _gameplayService;
        private readonly GameEndService _gameEndService;
        private readonly GameplayPopupService _popupService;

        public GameplayScreenPresenter(
            GameplayScreenView view,
            GameplayService gameplayService,
            GameEndService gameEndService,
            GameplayPopupService popupService)
        {
            _view = view;
            _gameplayService = gameplayService;
            _gameEndService = gameEndService;
            _popupService = popupService;
        }

        public void Initialize()
        {
            _gameplayService.SequenceUpdated += OnSequenceUpdated;
            _gameEndService.GameEnded += OnGameEnded;
        }

        public void Dispose()
        {
            _gameplayService.SequenceUpdated -= OnSequenceUpdated;
            _gameEndService.GameEnded -= OnGameEnded;
        }

        public void Update()
        {
            _gameplayService.Update();
        }

        private void OnSequenceUpdated(string targetSequence, string userInput)
        {
            _view.SetTargetSequence(targetSequence);
            _view.SetUserInput(userInput);
        }

        private void OnGameEnded(bool isVictory)
        {
            _view.HideText();
            _popupService.OpenGameEndPopup(isVictory);
        }
    }
}