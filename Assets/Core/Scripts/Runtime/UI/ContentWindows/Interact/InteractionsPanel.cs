using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    using Entity;
    using InteractionSystem;
    using Core.LocalizationSystem;
    
    public class InteractionsPanel : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform _contentHolder;

        [SerializeField] private GameObject _leaveOption;
        [SerializeField] private TextMeshProUGUI _leaveText;
        [SerializeField] private GameObject _interactionOptionPrefab;
        [SerializeField] private LocalizedString _leave = string.Empty;

        private ICharacter _player;
        private readonly IList<InteractionOptionUI> _interactions = new List<InteractionOptionUI>();

        private static readonly string OptionFormat = "{0}. [{1}]";

        #region UnityEvents

        private void Awake()
        {
            GameManager.OnPlayerChanged += OnPlayerChangedCallback;

            _leaveText.text = $"   [{_leave}]";
        }

        #endregion

        private void OnPlayerChangedCallback(ICharacter player)
        {
            _player = player;
        }

        public void Bind(IEnumerable<IInteraction> interactions)
        {
            GenerateOptions(interactions);
        }

        private void GenerateOptions(IEnumerable<IInteraction> interactions)
        {
            // Remove current options
            foreach (var option in _interactions)
            {
                Destroy(option.gameObject);
            }

            _interactions.Clear();

            // Add new options
            var optionNumber = 1;
            foreach (var option in interactions)
            {
                if (!option.MeetsCondition(_player)) continue;

                var optionUI = Instantiate(_interactionOptionPrefab, _contentHolder)
                    .GetComponent<InteractionOptionUI>();
                optionUI.Assign(string.Format(OptionFormat, optionNumber++, option.GetSecondaryInteractionText()),
                    option);
                _interactions.Add(optionUI);
            }

            _leaveOption.transform.SetAsLastSibling();
        }
    }
}