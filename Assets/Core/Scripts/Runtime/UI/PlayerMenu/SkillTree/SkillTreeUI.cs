using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    using Entity;
    
    public class SkillTreeUI : MonoBehaviour
    {
        [Header("Slot Status Colors")] [SerializeField]
        private Color _defaultColor;

        [SerializeField] private Color _unavailableColor;
        [SerializeField] private Color _purchasableColor;
        [SerializeField] private Color _ownColor;

        [Header("UI Details")] [SerializeField]
        private SkillSlotDetails _skillSlotDetails;

        [SerializeField] private PlayerDetails _playerDetails;

        [Header("Slots")] [SerializeField] private GameObject _slotHolder;
        [SerializeField] private SkillSlot _startSlot;

        private static ICharacter _player;
        private static SkillBag _skillBag;
        private SkillSlot _selectedSlot;
        private SkillSlot[] _slots;

        private void Start()
        {
            _player = GameManager.Player;
            _skillBag = _player.GetSkillBag();

            if (_startSlot) _startSlot.SetStatus(SlotState.Own);
            _slots = _slotHolder.GetComponentsInChildren<SkillSlot>(true);

            Register();
            UpdatePlayerDetails();
        }

        private void OnDestroy()
        {
            Unregister();
        }

        private void Register()
        {
            foreach (var slot in _slots)
            {
                _skillBag.OnPointsChanged += slot.RefreshSlot;
            }

            _skillBag.OnPointsChanged += UpdatePlayerDetails;
        }

        private void Unregister()
        {
            foreach (var slot in _slots)
            {
                _skillBag.OnPointsChanged -= slot.RefreshSlot;
            }

            _skillBag.OnPointsChanged -= UpdatePlayerDetails;
        }

        private void UpdatePlayerDetails()
        {
            _playerDetails.SetPointValue(_skillBag.SkillPoints);
        }

        public void UpdateDetails(SkillSlot slot)
        {
            _selectedSlot = slot;
            _skillSlotDetails.SetDetails(slot);
        }

        public ColorBlock GetCorrespondingColor(SlotState state)
        {
            // Get color based on state
            var color = state switch
            {
                SlotState.Own => _ownColor,
                SlotState.Purchasable => _purchasableColor,
                SlotState.Unavailable => _unavailableColor,
                _ or SlotState.Default => _defaultColor
            };

            // Create color block and set colors
            return ColorUtils.ApplyColor(color);
        }

        public void PurchaseSkill()
        {
            // If is null or not purchasable, return
            if (!_selectedSlot || !_selectedSlot.IsPurchasable()) return;

            // Set update UI and player SP
            _selectedSlot.SetStatus(SlotState.Own);
            _skillBag.ModifySkillPoints(-_selectedSlot.Skill.Cost);
            _playerDetails.SetPointValue(_skillBag.SkillPoints);
            _skillSlotDetails.SetDetails(_selectedSlot);
        }
    }
}
