using System;
using UnityEngine;

namespace UI
{
    public abstract class EnhancedUI<TValue, TEvent, TAction> : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] protected TEvent _events;
        [SerializeField] protected bool _isActive = true;

        #region UnityEvents

        protected virtual void OnValidate()
        {
            if (_isActive)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }

        #endregion
        
        public abstract void AddListener(TAction action);
        public abstract void RemoveListener(TAction action);

        public abstract void Enable();
        public abstract void Disable();
        
        public abstract TValue GetValue();
        public abstract void SetValue(TValue value);
    }
}