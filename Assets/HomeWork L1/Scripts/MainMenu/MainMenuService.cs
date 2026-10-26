using Game.Configs;
using Game.Gameplay;
using Game.Input;
using Game.SceneManagement;
using Game.Utilities;

namespace Game.MainMenu
{
    public class MainMenuService
    {
        private const char DigitsModeKey = '1';
        private const char LettersModeKey = '2';

        private readonly IInputService _inputService;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private bool _isLoading;

        public MainMenuService(
            IInputService inputService,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _inputService = inputService;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Update()
        {
            if (_isLoading) return;

            string input = _inputService.GetTypedInput();

            if (string.IsNullOrEmpty(input) == false)
            {
                char character = input[0];

                if (character == DigitsModeKey)
                {
                    StartGame(GameMode.Digits);
                }
                else if (character == LettersModeKey)
                {
                    StartGame(GameMode.Letters);
                }
            }
        }

        private void StartGame(GameMode mode)
        {
            _isLoading = true;
            GameplayInputArgs args = new GameplayInputArgs(mode);
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, args));
        }
    }
}