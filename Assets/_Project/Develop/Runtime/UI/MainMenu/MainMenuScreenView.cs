using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action DigitsButtonClicked;
        public event Action LettersButtonClicked;
        public event Action ResetButtonClicked;

        [SerializeField] private TMP_Text _winsText;
        [SerializeField] private TMP_Text _lossesText;
        [SerializeField] private TMP_Text _goldText;

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

        public void SetStats(int wins, int losses)
        {
            _winsText.text = $"Победы: {wins}";
            _lossesText.text = $"Поражения: {losses}";
        }

        public void SetGold(int gold)
        {
            _goldText.text = $"Золото: {gold}";
        }

        private void OnDigitsClicked() => DigitsButtonClicked?.Invoke();
        private void OnLettersClicked() => LettersButtonClicked?.Invoke();
        private void OnResetClicked() => ResetButtonClicked?.Invoke();
    }
}