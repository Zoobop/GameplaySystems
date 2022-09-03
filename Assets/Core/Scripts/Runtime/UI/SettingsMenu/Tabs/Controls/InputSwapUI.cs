using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    using InputSystem;
    
    public class InputSwapUI : MonoBehaviour
    {
        [SerializeField] private List<SectionUI> _sections = new();

        #region UnityEvents

        private void Start()
        {
            InputController.Instance.OnInputChanged += OnInputChangedCallback;
        }

        #endregion
        
        private void OnInputChangedCallback(InputType inputType)
        {
            var index = (int)inputType;
            _sections[index].gameObject.SetActive(true);

            for (var i = 0; i < _sections.Count; i++)
            {
                if (i == index) continue;
                
                _sections[i].gameObject.SetActive(false);
            }
        }
    }
}