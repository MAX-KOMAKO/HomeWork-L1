namespace _Project.Develop.Utilities.Utilities
{
    public class UnityConsoleService : IConsoleService
    {
        public void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}