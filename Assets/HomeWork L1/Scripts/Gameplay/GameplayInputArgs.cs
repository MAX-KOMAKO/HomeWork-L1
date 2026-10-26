using Game.Configs;
using Game.SceneManagement;

namespace Game.Gameplay
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameMode mode)
        {
            Mode = mode;
        }

        public GameMode Mode { get; }
    }
}