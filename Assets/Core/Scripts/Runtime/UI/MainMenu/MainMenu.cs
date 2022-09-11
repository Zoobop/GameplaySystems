using System;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private EnhancedButton _playButton;
        [SerializeField] private EnhancedButton _optionsButton;

        #region UnityEvents

        private void Start()
        {
            _playButton.AddListener(Play);
        }

        private void OnDestroy()
        {
            _playButton.RemoveListener(Play);
        }

        #endregion

        private static void Play()
        {
            GameManager.Play();
        }

        private void Options()
        {
            
        }
    }
}