using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    
    public class Tab : MonoBehaviour, ITab
    {
        [Header("Tab")]
        [SerializeField] private LocalizedString _tabName;

        private TabController _tabController;

        public void Bind(TabController tabController)
        {
            _tabController = tabController;
        }
        
        public LocalizedString GetTabName()
        {
            return _tabName;
        }

        public void SetActive()
        {
            _tabController.SetActiveTab(this);
        }
    }
}