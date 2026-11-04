namespace _Project.Develop.Utilities
{
    public class UnityConsoleService : IConsoleService
    {
        public void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}