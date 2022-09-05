using UnityEngine;

namespace UI
{
    using Settings;
    
    public class KeyBindModificationWindow : MonoBehaviour
    {
        private KeyBindUI _currentKeyBind;
        
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            SetKeyBindButton(null);
        }

        public void SetKeyBindButton(KeyBindUI keyBindUI)
        {
            _currentKeyBind = keyBindUI;
            if (_currentKeyBind != null)
            {
                print(keyBindUI.name);
                _currentKeyBind.RebindKeyBind(Disable);
            }
        }
    }
}