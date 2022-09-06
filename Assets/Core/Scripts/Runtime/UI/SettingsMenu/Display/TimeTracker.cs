using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    using ExternalEnvironment;
    using Settings;
    using LocalizationSystem;

    public class TimeTracker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private LocalizedString _dayText = string.Empty;
        [SerializeField] private List<LocalizedString> _timeRegionNames = new();

        #region UnityEvents

        private void Update()
        {
            if (DayCycle.Instance == null) return;
            
            if (!SettingsController.Instance.DisplayTime)
            {
                _timeText.enabled = false;
            }
            else
            {
                var index = (int) DayCycle.Instance.TimeRegion;
                
                _timeText.enabled = true;
                _timeText.text = $"{_dayText} {DayCycle.Instance.Day}: {(int)DayCycle.Instance.Time} - ({_timeRegionNames[index]})";
            }
        }

        #endregion
    }
}
