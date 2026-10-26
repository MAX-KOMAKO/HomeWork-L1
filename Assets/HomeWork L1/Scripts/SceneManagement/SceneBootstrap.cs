using System.Collections;
using Game.DI;
using UnityEngine;

namespace Game.SceneManagement
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null);
        public abstract IEnumerator Initialize();
        public abstract void Run();
    }
}