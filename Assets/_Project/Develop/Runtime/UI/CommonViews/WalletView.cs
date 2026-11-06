using TMPro;
using UnityEngine;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.CommonViews
{
    public class WalletView : MonoBehaviour, IView
    {
        private const string GoldFormat = "Золото: {0}";

        [SerializeField] private TMP_Text _goldText;

        public void SetValue(int value)
        {
            _goldText.text = string.Format(GoldFormat, value);
        }
    }
}