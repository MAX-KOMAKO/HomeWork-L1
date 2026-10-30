using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Configs
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameModeConfig")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public GameModes Mode { get; private set; }
        [field: SerializeField, TextArea(1, 5)] public string Characters { get; private set; }
        [field: SerializeField, Min(1)] public int SequenceLength { get; private set; } = 5;

        private void OnValidate()
        {
            if (SequenceLength < 1)
                SequenceLength = 1;
        }
    }
}