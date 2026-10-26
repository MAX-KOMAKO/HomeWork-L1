using UnityEngine;

namespace Game.Input
{
    public class UnityInputService : IInputService
    {
        public string GetTypedInput()
        {
            return UnityEngine.Input.inputString;
        }

        public bool IsSpacePressed()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space);
        }
    }
}