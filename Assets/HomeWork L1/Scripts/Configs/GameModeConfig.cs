using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameModeConfig")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public GameMode Mode { get; private set; }
        [field: SerializeField] public string Characters { get; private set; }
        [field: SerializeField] public int SequenceLength { get; private set; } = 5;
    }
}