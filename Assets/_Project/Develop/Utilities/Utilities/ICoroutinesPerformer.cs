using System.Collections;
using UnityEngine;

namespace _Project.Develop.Utilities.Utilities
{
    public interface ICoroutinesPerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);
        void StopPerform(Coroutine coroutine);
    }
}