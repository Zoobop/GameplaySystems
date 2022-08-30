using LocalizationSystem;

namespace UI
{
    public interface ITab
    {
        public LocalizedString GetTabName();
        public void Enable();
        public void Disable();
    }
}