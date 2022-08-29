using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private List<TabContentPair> _tabs = new();
        [SerializeField] private string _activeTab;

        #region UnityEvents

        private void Awake()
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
        }

        public void SetActiveTab(ITab tab)
        {
            _activeTab = tab.GetTabName();
            ((Tab) tab).gameObject.SetActive(true);

            foreach (var (_, otherTab) in _tabs)
            {
                if (otherTab == tab) continue;
                
                ((Tab)otherTab).gameObject.SetActive(false);
            }
        }
    }
}
