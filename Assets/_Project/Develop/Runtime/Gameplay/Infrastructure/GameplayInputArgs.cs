using _Project.Develop.Runtime.Gameplay.Features.Configs;
using _Project.Develop.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
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