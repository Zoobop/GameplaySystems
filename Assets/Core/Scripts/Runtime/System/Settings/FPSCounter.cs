using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Settings
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField, Min(0.00001f)] private float _tickSpeed = 0.1f;
        
        private Coroutine _coroutine;
        private WaitForSeconds _tickDelay;
        
        #region UnityEvents

        private void Awake()
        {
            _tickDelay = new WaitForSeconds(_tickSpeed);
        }

        private void Start()
        {
            _coroutine = StartCoroutine(StartFPSCount());
        }

        #endregion

        private IEnumerator StartFPSCount()
        {
            while (true)
            {
                if (!SettingsController.Instance.DisplayFPS)
                {
                    _fpsText.enabled = false;
                }
                else
                {
                    _fpsText.enabled = true;
                    _fpsText.text = $"{(int)SettingsController.Instance.GetFPS()} FPS";
                }
                
                yield return _tickDelay;
            }
        }
    }
}
