using TMPro;
using UnityEngine;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class StatsView : MonoBehaviour, IView
    {
        private const string WinsFormat = "Победы: {0}";
        private const string LossesFormat = "Поражения: {0}";

        [SerializeField] private TMP_Text _winsText;
        [SerializeField] private TMP_Text _lossesText;

        public void SetStats(int wins, int losses)
        {
            _winsText.text = string.Format(WinsFormat, wins);
            _lossesText.text = string.Format(LossesFormat, losses);
        }
    }
}