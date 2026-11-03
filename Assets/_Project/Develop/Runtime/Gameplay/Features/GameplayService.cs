using System;
using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Runtime.Meta.Features.Input;

namespace _Project.Develop.Runtime.Gameplay.Features
{
    public class GameplayService
    {
        public event Action Victory;
        public event Action Defeat;
        public event Action<string, string> SequenceUpdated;

        private readonly IInputService _inputService;
        private readonly SequenceGenerator _sequenceGenerator;

        private string _currentSequence;
        private string _currentUserInput;
        private int _currentIndex;
        private bool _isGameActive;

        public GameplayService(IInputService inputService, SequenceGenerator sequenceGenerator)
        {
            _inputService = inputService;
            _sequenceGenerator = sequenceGenerator;
        }

        public void StartGame(GameModeConfig config)
        {
            _currentSequence = _sequenceGenerator.Generate(config);
            _currentUserInput = string.Empty;
            _currentIndex = 0;
            _isGameActive = true;

            SequenceUpdated?.Invoke(_currentSequence, _currentUserInput);
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
                _currentUserInput += character;
                SequenceUpdated?.Invoke(_currentSequence, _currentUserInput);

                if (_currentIndex >= _currentSequence.Length)
                {
                    _isGameActive = false;
                    Victory?.Invoke();
                }
            }
            else
            {
                _isGameActive = false;
                _currentUserInput += character;
                SequenceUpdated?.Invoke(_currentSequence, _currentUserInput);
                Defeat?.Invoke();
            }
        }
    }
}