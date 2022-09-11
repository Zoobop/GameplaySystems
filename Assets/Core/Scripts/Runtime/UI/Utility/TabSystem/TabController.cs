using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    
    public class TabController : MonoBehaviour
    {
        [SerializeField] private List<TabContentPair> _tabs = new();
        private int _activeTabIndex = 0;

        #region UnityEvents

        private void Awake()
        {
            LocalizationSystem.OnLanguageChanged += OnLanguageChangedCallback;
        }

        private void Start()
        {

            MapTabActions();
        }
        
        #endregion

        private void OnLanguageChangedCallback(LocalizationSystem.Language language)
        {
            foreach (var pair in _tabs)
            {
                pair.UpdateText();
            }
        }
        
        private void MapTabActions()
        {
            foreach (var pair in _tabs)
            {
                pair.tab.Bind(this);
                pair.Bind();
            }
            
            SetActiveTab(0);
        }

        private void SetActiveTab(int index)
        {
            _activeTabIndex = index;
            
            var (button, tab) = _tabs[index];
            button.Disable();

            foreach (var (otherButton, otherTab) in _tabs)
            {
                if (otherTab == tab) continue;
                
                otherButton.Enable();
                otherTab.Disable();
            }
        }
        
        public void SetActiveTab(ITab tab)
        {
            var index = _tabs.FindIndex(pair => ReferenceEquals(tab, pair.tab));
            SetActiveTab(index);
        }

        public void TabLeft()
        {
            var nextIndex = _activeTabIndex - 1;
            if (nextIndex < 0)
            {
                nextIndex = _tabs.Count - 1;
            }

            // Set tab at index to be active
            _tabs[nextIndex].tab.Enable();
        }

        public void TabRight()
        {
            var nextIndex = _activeTabIndex + 1;
            if (nextIndex > _tabs.Count - 1)
            {
                nextIndex = 0;
            }
            
            // Set tab at index to be active
            _tabs[nextIndex].tab.Enable();
        }
    }
}
