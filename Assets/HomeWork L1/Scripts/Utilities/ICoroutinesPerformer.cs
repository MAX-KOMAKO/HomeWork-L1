using System.Collections;
using UnityEngine;

namespace Game.Utilities
{
    public interface ICoroutinesPerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);
        void StopPerform(Coroutine coroutine);
    }
}