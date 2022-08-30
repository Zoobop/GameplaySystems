using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private List<TabContentPair> _tabs = new();

        #region UnityEvents

        private void Start()
        {
            MapTabActions();
        }
        
        #endregion

        private void MapTabActions()
        {
            foreach (var pair in _tabs)
            {
                pair.tab.Bind(this);
                pair.Bind();
            }
            
            SetActiveTab(_tabs[0].tab);
        }

        public void SetActiveTab(ITab tab)
        {
            var (activeButton, _) = _tabs.Find(pair => ReferenceEquals(tab, pair.tab));
            activeButton.Disable();

            foreach (var (button, otherTab) in _tabs)
            {
                if (otherTab == tab) continue;
                
                button.Enable();
                otherTab.Disable();
            }
        }
    }
}
