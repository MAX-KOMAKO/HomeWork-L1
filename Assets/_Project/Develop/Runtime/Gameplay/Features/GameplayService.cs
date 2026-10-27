using System;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Runtime.Meta.Features.Input;
using _Project.Develop.Utilities.Utilities;

namespace _Project.Develop.Runtime.Gameplay.Features
{
    public class GameplayService
    {
        private const string VictoryMessage = "Победа! Нажмите пробел для выхода в меню.";
        private const string DefeatMessage = "Поражение! Нажмите пробел для перезапуска.";

        public event Action Victory;
        public event Action Defeat;

        private readonly IInputService _inputService;
        private readonly IConsoleService _consoleService;
        private readonly SequenceGenerator _sequenceGenerator;

        private string _currentSequence;
        private int _currentIndex;
        private bool _isGameActive;

        public GameplayService(
            IInputService inputService,
            IConsoleService consoleService,
            SequenceGenerator sequenceGenerator)
        {
            _inputService = inputService;
            _consoleService = consoleService;
            _sequenceGenerator = sequenceGenerator;
        }

        public void StartGame(GameModeConfig config)
        {
            _currentSequence = _sequenceGenerator.Generate(config);
            _currentIndex = 0;
            _isGameActive = true;
            
            _consoleService.Log($"Сгенерирована последовательность: {_currentSequence}");
        }

        public void Update()
        {
            if (_isGameActive == false) return;

            string input = _inputService.GetTypedInput();

            if (string.IsNullOrEmpty(input) == false)
            {
                foreach (char character in input)
                {
                    ProcessInput(character);
                    if (_isGameActive == false) break;
                }
            }
        }

        private void ProcessInput(char character)
        {
            char normalizedCharacter = char.ToLowerInvariant(character);
            char targetCharacter = char.ToLowerInvariant(_currentSequence[_currentIndex]);

            if (normalizedCharacter == targetCharacter)
            {
                _currentIndex++;

                if (_currentIndex >= _currentSequence.Length)
                {
                    _isGameActive = false;
                    _consoleService.Log(VictoryMessage);
                    Victory?.Invoke();
                }
            }
            else
            {
                _isGameActive = false;
                _consoleService.Log(DefeatMessage);
                Defeat?.Invoke();
            }
        }
    }
}