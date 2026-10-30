using UnityEngine;
using _Project.Develop.Runtime.Gameplay.Features;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayController : MonoBehaviour
    {
        private GameEndService _gameEndService;

        public void Initialize(GameEndService gameEndService)
        {
            _gameEndService = gameEndService;
        }

        private void Update()
        {
            _gameEndService?.Update();
        }

        private void OnDestroy()
        {
            _gameEndService?.Dispose();
        }
    }
}