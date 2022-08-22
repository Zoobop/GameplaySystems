using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TabController : MonoBehaviour
    {
        [Header("Buttons")] [SerializeField] private List<EnhancedButton> _tabButtons = new();

        [Header("Tabs")] [SerializeField] private List<GameObject> _tabList = new();

        private readonly IDictionary<EnhancedButton, GameObject> _mapper = new Dictionary<EnhancedButton, GameObject>();

        #region UnityEvents

        private void Awake()
        {
            SetMapper();
        }

        private void OnValidate()
        {
            SetMapper();
        }

        #endregion

        private void SetMapper()
        {
            // Reset mappings
            _mapper.Clear();

            // Set mappings
            for (var i = 0; i < _tabButtons.Count; i++)
            {
                _mapper.Add(_tabButtons[i], _tabList[i]);
            }
        }

        public void SetActiveTab(int tab)
        {

        }
    }
}
