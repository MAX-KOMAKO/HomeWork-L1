using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Configs
{
    [CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "Configs/GameSettings")]
    public class GameSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public GameModeConfig[] GameModes { get; private set; }
    }
}