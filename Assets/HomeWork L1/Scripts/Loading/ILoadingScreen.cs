namespace Game.Loading
{
    public interface ILoadingScreen
    {
        bool IsShown { get; }
        void Show();
        void Hide();
    }
}