using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class UIOptionElement<T> : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        protected TextMeshProUGUI _optionText;

        [SerializeField] protected Button _optionButton;

        #region UnityEvents

        protected virtual void Start()
        {
            _optionButton.onClick.AddListener(OnSelected);
        }

        protected virtual void OnDestroy()
        {
            _optionButton.onClick.RemoveListener(OnSelected);
        }

        #endregion

        public abstract void Assign(string formattedText, in T context);

        protected abstract void OnSelected();
    }
}