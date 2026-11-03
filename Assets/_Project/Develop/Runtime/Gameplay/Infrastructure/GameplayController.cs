using UnityEngine;
using _Project.Develop.Runtime.UI.Gameplay;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayController : MonoBehaviour
    {
        private GameplayScreenPresenter _presenter;

        public void Initialize(GameplayScreenPresenter presenter)
        {
            _presenter = presenter;
        }

        private void Update()
        {
            _presenter?.Update();
        }

        private void OnDestroy()
        {
            _presenter?.Dispose();
        }
    }
}