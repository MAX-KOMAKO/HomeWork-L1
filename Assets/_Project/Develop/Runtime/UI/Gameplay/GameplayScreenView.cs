using TMPro;
using UnityEngine;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _targetSequenceText;
        [SerializeField] private TMP_Text _userInputText;

        public void SetTargetSequence(string sequence) => _targetSequenceText.text = sequence;
        public void SetUserInput(string input) => _userInputText.text = input;

        public void HideText()
        {
            _targetSequenceText.text = string.Empty;
            _userInputText.text = string.Empty;
        }
    }
}