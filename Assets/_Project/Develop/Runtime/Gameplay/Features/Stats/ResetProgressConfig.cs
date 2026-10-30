using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Stats
{
    [CreateAssetMenu(fileName = "ResetProgressConfig", menuName = "Configs/ResetProgressConfig")]
    public class ResetProgressConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int ResetCost { get; private set; } = 100;

        private void OnValidate()
        {
            if (ResetCost < 0)
                ResetCost = 0;
        }
    }
}