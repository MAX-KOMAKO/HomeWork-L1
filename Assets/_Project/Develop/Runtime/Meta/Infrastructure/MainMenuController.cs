using UnityEngine;
using _Project.Develop.Runtime.Meta.Features;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuController : MonoBehaviour
    {
        private MainMenuService _mainMenuService;

        public void Initialize(MainMenuService mainMenuService)
        {
            _mainMenuService = mainMenuService;
        }

        private void Update()
        {
            _mainMenuService?.Update();
        }
    }
}