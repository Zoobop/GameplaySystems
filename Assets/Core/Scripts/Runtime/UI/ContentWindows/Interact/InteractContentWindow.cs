using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    using Entity;
    using InputSystem;
    using InteractionSystem;
    using LocalizationSystem;
    
    public class InteractContentWindow : ContentWindow<IInteractionTarget>
    {
        public static InteractContentWindow Instance { get; private set; }

        [Header("References")] [SerializeField]
        private InteractionsPanel _interactionsPanel;

        [SerializeField] private Canvas _interactTriggerCanvas;
        [SerializeField] private Canvas _interactionsCanvas;
        [SerializeField] private TextMeshProUGUI _pressText;
        [SerializeField] private TextMeshProUGUI _interactionText;
        [SerializeField] private TextMeshProUGUI _keyboardKeyText;
        [SerializeField] private Image _keyBindImage;

        [Header("Text")]
        [SerializeField] private LocalizedString _press = string.Empty;
        [SerializeField] private LocalizedString _interact = string.Empty;
        
        private IInteractionTarget _interactionTarget;
        private IInteractionInitiator _interactionInitiator;

        #region UnityEvents

        protected override void Awake()
        {
            // Parent implementation
            base.Awake();

            Instance = this;

            GameManager.OnPlayerChanged += OnPlayerChangedCallback;
        }

        private void Start()
        {
            _pressText.text = _press;
            _interactionText.text = _interact;
            _pressText.gameObject.SetActive(!string.IsNullOrEmpty(_pressText.text));
            
            InputController.Instance.OnKeyBindsChanged += Bind;
            Bind(InputController.Instance.CurrentKeyBinds);
        }

        private void OnDestroy()
        {
            InputController.Instance.OnKeyBindsChanged -= Bind;
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            _interactionInitiator = player.GetInteractionInitiator();
        }

        #region WindowUtility

        private void Bind(KeyBindings keyBindings)
        {
            var key = keyBindings.InteractKeyBind;

            // Not keyboard
            if (InputController.Instance.InputType != InputType.Keyboard)
            {
                _keyboardKeyText.enabled = false;
                _keyBindImage.sprite = InputActionRegistry.Instance.KeyCodeToImage(key);
                return;
            }

            // Keyboard
            _keyBindImage.sprite = InputActionRegistry.Instance.GetKeyboardKeyImage();
            _keyboardKeyText.enabled = true;
            _keyboardKeyText.text = InputActionRegistry.EnumToString(key);
        }

        public void SetTarget(in IInteractionTarget target)
        {
            // Assign
            _interactionTarget = target;
        }

        #endregion

        #region ContentWindow

        protected override IEnumerator Show(IInteractionTarget target)
        {
            SetWindowOpen(true);
            SetCanvas(true);

            // Show trigger panel
            _interactTriggerCanvas.enabled = true;
            _interactionsCanvas.enabled = false;

            // Parent implementation
            yield return base.Show(target);
        }

        protected override IEnumerator Hide()
        {
            // Parent implementation
            yield return base.Hide();

            SetWindowOpen(false);
            SetCanvas(false);
        }

        #endregion

        #region InteractionWindow

        public void CancelInteraction()
        {
            _interactTriggerCanvas.enabled = true;
            _interactionsCanvas.enabled = false;
            _interactionInitiator.StopInteracting();
        }

        private void SetInteractionsPanel()
        {
            _interactionsCanvas.enabled = true;
            _interactTriggerCanvas.enabled = false;
        }

        public void ShowInteractions(IEnumerable<IInteraction> interactions)
        {
            _interactionsPanel.Bind(interactions);
            SetInteractionsPanel();
        }

        #endregion
    }
}