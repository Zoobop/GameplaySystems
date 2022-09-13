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
            AudioController.PlayMenuMusic();
            
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
            
            AudioController.PlayBGM();
        }
    }
}