using System.Collections;
using UnityEngine;

namespace Game.Utilities
{
    public class CoroutinesPerformer : MonoBehaviour, ICoroutinesPerformer
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public Coroutine StartPerform(IEnumerator coroutineFunction)
        {
            return StartCoroutine(coroutineFunction);
        }

        public void StopPerform(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}