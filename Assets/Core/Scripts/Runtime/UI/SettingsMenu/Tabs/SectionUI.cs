using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    using InputSystem;
    
    public class SectionUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _sectionTitleText;
        [SerializeField] private List<TextMeshProUGUI> _inputLabelTexts = new();

        [Header("Section Info")]
        [SerializeField] private LocalizedString _sectionTitle;
        [SerializeField] private List<LocalizedString> _inputLabels = new();

        #region UnityEvents

        private void Awake()
        {
            LocalizationSystem.OnLanguageChanged += MapTexts;
        }

        #endregion

        private void MapTexts(LocalizationSystem.Language language)
        {
            _sectionTitleText.text = _sectionTitle;
            
            var size = _inputLabelTexts.Count;
            for (var i = 0; i < size; i++)
            {
                _inputLabelTexts[i].text = _inputLabels[i];
            }
        }
    }
}