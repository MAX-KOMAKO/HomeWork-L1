using _Project.Develop.Runtime.Gameplay.Features.Stats;
using _Project.Develop.Runtime.Gameplay.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Configs
{
    [CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "Configs/GameSettings")]
    public class GameSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public GameModeConfig[] GameModes { get; private set; }
        [field: SerializeField] public WalletConfig WalletConfig { get; private set; }
        [field: SerializeField] public ResetProgressConfig ResetProgressConfig { get; private set; }
    }
}