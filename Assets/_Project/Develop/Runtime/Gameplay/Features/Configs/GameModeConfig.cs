using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Configs
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameModeConfig")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public GameModes Mode { get; private set; }
        [field: SerializeField] public string Characters { get; private set; }
        [field: SerializeField] public int SequenceLength { get; private set; } = 5;
    }
}