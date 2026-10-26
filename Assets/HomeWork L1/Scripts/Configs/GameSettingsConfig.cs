using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "Configs/GameSettings")]
    public class GameSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public GameModeConfig[] GameModes { get; private set; }
    }
}