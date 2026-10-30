using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Stats
{
    [CreateAssetMenu(fileName = "ResetProgressConfig", menuName = "Configs/ResetProgressConfig")]
    public class ResetProgressConfig : ScriptableObject
    {
        [field: SerializeField] public int ResetCost { get; private set; } = 100;
    }
}