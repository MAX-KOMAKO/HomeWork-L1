using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameEndPopupView : PopupViewBase
    {
        public event Action MenuClicked;
        public event Action RestartClicked;

        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(OnMenuClicked);
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(OnMenuClicked);
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }

        public void SetMessage(string message) => _messageText.text = message;

        private void OnMenuClicked() => MenuClicked?.Invoke();
        private void OnRestartClicked() => RestartClicked?.Invoke();
    }
}