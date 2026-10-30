using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Wallet
{
    [CreateAssetMenu(fileName = "WalletConfig", menuName = "Configs/WalletConfig")]
    public class WalletConfig : ScriptableObject
    {
        [field: SerializeField] public int StartGold { get; private set; } = 100;
        [field: SerializeField] public int RewardForWin { get; private set; } = 50;
        [field: SerializeField] public int PenaltyForLoss { get; private set; } = 20;

        private void OnValidate()
        {
            if (StartGold < 0) StartGold = 0;
            if (RewardForWin < 0) RewardForWin = 0;
            if (PenaltyForLoss < 0) PenaltyForLoss = 0;
        }
    }
}