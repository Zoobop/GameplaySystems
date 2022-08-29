using System;

namespace UI
{
    [Serializable]
    public struct TabContentPair
    {
        public EnhancedButton button;
        public Tab tab;

        public void Bind()
        {
            button.SetText(tab.GetTabName());
            button.AddEvent(SetActive);
        }

        public void Unbind()
        {
            button.RemoveEvent(SetActive);
        }

        private void SetActive()
        {
            tab.SetActive();
        }

        public void Deconstruct(out EnhancedButton button, out ITab tab)
        {
            button = this.button;
            tab = this.tab;
        }
    }
}