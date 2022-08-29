using LocalizationSystem;

namespace UI
{
    public interface ITab
    {
        public LocalizedString GetTabName();
        public void SetActive();
    }
}