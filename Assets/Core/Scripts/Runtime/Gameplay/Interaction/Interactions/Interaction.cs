using System;
using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem
{
    using Entity;
    
    public abstract class Interaction : MonoBehaviour, IInteraction
    {
        [Header("Events")] [SerializeField] private UnityEvent _onStarted;
        [SerializeField] private UnityEvent _onEnded;

        private bool _isActive = true;

        public event Action OnStarted = delegate { };
        public event Action OnEnded = delegate { };

        #region IInteraction

        public abstract bool MeetsCondition(in ICharacter interactionInitiator);

        public bool IsActive()
        {
            return _isActive;
        }

        public abstract string GetPrimaryInteractionText();

        public string GetSecondaryInteractionText()
        {
            return GetPrimaryInteractionText();
        }

        public virtual void OnStart()
        {
            InvokeStart();
        }

        public abstract void OnInteract(in ICharacter character);

        public virtual void OnEnd()
        {
            InvokeEnd();
        }

        public virtual void PostInteraction()
        {
        }

        #endregion

        protected void SetActive(bool state)
        {
            _isActive = state;
        }

        protected void InvokeStart()
        {
            OnStarted?.Invoke();
            _onStarted?.Invoke();
        }

        protected void InvokeEnd()
        {
            _onEnded?.Invoke();
            OnEnded?.Invoke();
        }

        protected void StopInteraction()
        {
            InteractionController.Instance.StopInteraction();
        }
    }
}