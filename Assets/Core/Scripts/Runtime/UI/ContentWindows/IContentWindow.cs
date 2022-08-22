using System;

namespace UI
{
    public interface IContentWindow<in TContent> : IWindow
    {
        public event Action OnWindowClosed;
        public void OpenWindow(TContent content);
        public void CloseWindow();
    }
}
