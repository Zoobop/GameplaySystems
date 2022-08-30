using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    
    public class Tab : MonoBehaviour, ITab
    {
        [Header("Tab")]
        [SerializeField] private LocalizedString _tabName;
        [SerializeField] private GameObject _tabContent;

        private TabController _tabController;

        public void Bind(TabController tabController)
        {
            _tabController = tabController;
        }
        
        public LocalizedString GetTabName()
        {
            return _tabName;
        }

        public void Enable()
        {
            _tabContent.SetActive(true);
            _tabController.SetActiveTab(this);
        }

        public void Disable()
        {
            _tabContent.SetActive(false);
        }
    }
}