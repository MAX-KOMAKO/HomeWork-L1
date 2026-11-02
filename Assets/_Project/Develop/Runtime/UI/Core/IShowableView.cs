using System.Collections;

namespace _Project.Develop.Runtime.UI.Core
{
    public interface IShowableView : IView
    {
        IEnumerator Hide();
        IEnumerator Show();
    }
}