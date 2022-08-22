using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    using Entity;

    public class PlayerInfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;

        #region UnityEvents

        private void Awake()
        {
            GameManager.OnPlayerChanged += OnPlayerChangedCallback;
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            _nameText.text = player.GetName();
        }
    }
}
