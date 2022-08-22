
namespace UI
{
    public interface IWindow
    {
        public void Select(IWindowElement element);
        public void Deselect();
        public bool IsWindowOpen();
    }
}