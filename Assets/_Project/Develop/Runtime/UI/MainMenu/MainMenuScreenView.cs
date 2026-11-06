using System;
using UnityEngine;
using UnityEngine.UI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.CommonViews;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action DigitsButtonClicked;
        public event Action LettersButtonClicked;
        public event Action ResetButtonClicked;

        [field: SerializeField] public StatsView StatsView { get; private set; }
        [field: SerializeField] public WalletView WalletView { get; private set; }

        [SerializeField] private Button _digitsButton;
        [SerializeField] private Button _lettersButton;
        [SerializeField] private Button _resetButton;

        private void OnEnable()
        {
            _digitsButton.onClick.AddListener(OnDigitsClicked);
            _lettersButton.onClick.AddListener(OnLettersClicked);
            _resetButton.onClick.AddListener(OnResetClicked);
        }

        private void OnDisable()
        {
            _digitsButton.onClick.RemoveListener(OnDigitsClicked);
            _lettersButton.onClick.RemoveListener(OnLettersClicked);
            _resetButton.onClick.RemoveListener(OnResetClicked);
        }

        private void OnDigitsClicked() => DigitsButtonClicked?.Invoke();
        private void OnLettersClicked() => LettersButtonClicked?.Invoke();
        private void OnResetClicked() => ResetButtonClicked?.Invoke();
    }
}