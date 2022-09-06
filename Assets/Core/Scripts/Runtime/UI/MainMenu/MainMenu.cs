using System;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private EnhancedButton _playButton;

        #region UnityEvents

        private void Start()
        {
            _playButton.AddListener(GameManager.Play);
        }

        private void OnDestroy()
        {
            _playButton.RemoveListener(GameManager.Play);
        }

        #endregion
    }
}