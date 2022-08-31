using System;
using TMPro;
using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    
    public class SectionUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _sectionTitleText;
        
        [Header("Section Info")]
        [SerializeField] private LocalizedString _sectionTitle;

        #region UnityEvents

        private void Start()
        {
            _sectionTitleText.text = _sectionTitle;
        }

        #endregion
    }
}