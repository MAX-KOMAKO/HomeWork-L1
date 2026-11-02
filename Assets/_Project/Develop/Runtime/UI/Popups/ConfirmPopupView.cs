using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.Popups
{
    public class ConfirmPopupView : PopupViewBase
    {
        public event Action ConfirmClicked;

        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(OnConfirm);
            _cancelButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(OnConfirm);
            _cancelButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void SetMessage(string message) => _messageText.text = message;

        private void OnConfirm() => ConfirmClicked?.Invoke();
    }
}