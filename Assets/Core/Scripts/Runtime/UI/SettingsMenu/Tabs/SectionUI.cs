using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    
    public class SectionUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _sectionTitleText;
        [SerializeField] private List<TextMeshProUGUI> _inputLabelTexts = new();

        [Header("Section Info")]
        [SerializeField] private LocalizedString _sectionTitle;
        [SerializeField] private List<LocalizedString> _inputLabels = new();

        #region UnityEvents

        private void Start()
        {
            _sectionTitleText.text = _sectionTitle;
        }

        #endregion
    }
}