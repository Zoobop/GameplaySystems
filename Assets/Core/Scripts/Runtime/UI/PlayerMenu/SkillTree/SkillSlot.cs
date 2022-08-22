using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    using Entity;
    
    public enum SlotState
    {
        Default,
        Purchasable,
        Unavailable,
        Own,
    }

    public class SkillSlot : MonoBehaviour
    {
        private SkillTreeUI _skillTreeUI;

        [Header("General")] [SerializeField] private Skill _skill;
        [SerializeField] private Image _skillIcon;
        [SerializeField] private Button _skillSlotBorder;
        [SerializeField] private SlotState _slotState;

        [Header("Unlocked Skills")] [SerializeField]
        private List<SkillSlot> _precedingSkills;

        public Skill Skill => _skill;
        public SlotState SlotState => _slotState;

        private SkillBag _skillBag;

        private void Start()
        {
            UpdateSlot();
        }

        private void OnEnable()
        {
            _skillBag = GameManager.Player.GetSkillBag();
            _skillTreeUI = PlayerMenuController.SkillTreeUI;
        }

        public void SetStatus(SlotState state)
        {
            _slotState = state;
            UpdateSlot();
        }

        public void Select() => _skillTreeUI.UpdateDetails(this);

        public void UpdateSlot()
        {
            // Assign corresponding colors based on slot state
            _skillSlotBorder.colors = _skillTreeUI.GetCorrespondingColor(_slotState);

            // Update unlocked skills status
            if (_slotState == SlotState.Own)
            {
                foreach (var slot in _precedingSkills.Where(slot => slot.SlotState is not SlotState.Own))
                {
                    if (slot.IsPurchasable())
                    {
                        slot.SetStatus(SlotState.Purchasable);
                        continue;
                    }

                    slot.SetStatus(SlotState.Unavailable);
                }
            }

            if (!_skill) return;

            // Assign icon if valid
            _skillIcon.sprite = _skill.Icon;
        }

        public void RefreshSlot()
        {
            if (_slotState is SlotState.Unavailable or SlotState.Purchasable)
            {
                if (IsPurchasable())
                {
                    SetStatus(SlotState.Purchasable);
                    return;
                }

                SetStatus(SlotState.Unavailable);
            }
        }

        public bool IsPurchasable()
        {
            return _skill.Cost <= _skillBag.SkillPoints && _slotState is not SlotState.Own;
        }
    }
}