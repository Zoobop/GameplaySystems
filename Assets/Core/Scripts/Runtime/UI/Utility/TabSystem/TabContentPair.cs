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
            button.AddListener(tab.Enable);
        }

        public void Unbind()
        {
            button.RemoveListener(tab.Enable);
        }

        public void Deconstruct(out EnhancedButton button, out ITab tab)
        {
            button = this.button;
            tab = this.tab;
        }
    }
}