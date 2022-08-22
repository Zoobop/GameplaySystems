using System;
using System.Collections;
using UnityEngine;


namespace InteractionSystem
{
    using Entity;
    
    public class InteractionController : MonoBehaviour
    {
        // Singleton
        public static InteractionController Instance { get; private set; }

        [Header("Interaction")] [SerializeField, Min(0.001f)]
        private float _tickSpeed = 0.01f;

        [SerializeField, Min(0)] private float _interactionCooldown = 1f;

        private ICharacter _player;
        private IInteraction _currentInteraction;
        private IInteractionInitiator _interactionInitiator;

        private Coroutine _coroutine;
        private WaitForSeconds _tickDelay;
        private WaitForSeconds _cooldownDelay;
        private bool _isInteracting;

        public static event Action OnStart = delegate { };
        public static event Action OnEnd = delegate { };

        #region UnityEvents

        private void Awake()
        {
            Instance = this;
            GameManager.OnPlayerChanged += OnPlayerChangedCallback;

            _tickDelay = new WaitForSeconds(_tickSpeed);
            _cooldownDelay = new WaitForSeconds(_interactionCooldown);
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            _player = player;
            _interactionInitiator = _player.GetInteractionInitiator();
        }

        #region Interaction

        public void StartInteraction(in IInteraction interaction)
        {
            // Assign
            _currentInteraction = interaction;
            _isInteracting = true;

            if (_coroutine != null)
            {
                StopCoroutine(Interact());
            }

            _coroutine = StartCoroutine(Interact());
        }

        public void StopInteraction()
        {
            _isInteracting = false;
        }

        private void OnInteractionStart()
        {
            OnStart?.Invoke();

            _interactionInitiator.StartInteracting();

            _currentInteraction.OnStart();
        }

        private void OnInteractionEnd()
        {
            _currentInteraction.OnEnd();

            OnEnd?.Invoke();
        }

        private IEnumerator Interact()
        {
            // On Start
            OnInteractionStart();

            // Interact
            _currentInteraction.OnInteract(_player);

            // Wait til finished
            while (_isInteracting)
            {
                yield return _tickDelay;
            }

            // On End
            OnInteractionEnd();

            // Interaction cooldown
            yield return _cooldownDelay;

            // Post interaction
            _currentInteraction.PostInteraction();
            _interactionInitiator.StopInteracting();
        }

        #endregion
    }
}