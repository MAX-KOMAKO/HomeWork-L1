using UnityEngine;

namespace _Project.Develop.Utilities.LoadingScreen
{
    public class StandardLoadingScreen : MonoBehaviour, ILoadingScreen
    {
        public bool IsShown => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}