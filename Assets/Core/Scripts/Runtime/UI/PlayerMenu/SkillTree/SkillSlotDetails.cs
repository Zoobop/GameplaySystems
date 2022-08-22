using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillSlotDetails : MonoBehaviour
    {
        [SerializeField] private SkillTreeUI _skillTreeUI;

        [SerializeField] private TMP_Text _skillNameText;
        [SerializeField] private TMP_Text _skillDescriptionText;

        [SerializeField] private Image _skillIconBorder;
        [SerializeField] private Image _skillIconHolder;

        [SerializeField] private Button _purchaseButton;
        [SerializeField] private TMP_Text _buttonText;

        private void Start()
        {
            SetDefaultDetails();
        }

        public void SetDetails(SkillSlot slot)
        {
            if (slot.Skill)
            {
                _skillNameText.text = slot.Skill.Name;
                _skillDescriptionText.text = slot.Skill.Description;
                _skillIconHolder.sprite = slot.Skill.Icon;
            }
            else
            {
                _skillNameText.text = "Null Skill";
                _skillDescriptionText.text = "Null Skill";
                _skillIconHolder.sprite = null;
            }

            switch (slot.SlotState)
            {
                case SlotState.Own:
                    _buttonText.text = "Purchased";
                    _purchaseButton.interactable = true;
                    break;
                case SlotState.Purchasable:
                    _buttonText.text = $"Purchase: {slot.Skill.Cost} SP";
                    _purchaseButton.interactable = true;
                    break;
                case SlotState.Unavailable:
                    _buttonText.text = $"{slot.Skill.Cost} SP Needed";
                    _purchaseButton.interactable = true;
                    break;
                case SlotState.Default:
                    _buttonText.text = "Unavailable";
                    _purchaseButton.interactable = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _purchaseButton.colors = _skillTreeUI.GetCorrespondingColor(slot.SlotState);
            _skillIconBorder.color = _purchaseButton.colors.normalColor;
        }

        private void SetDefaultDetails()
        {
            _skillNameText.text = "-";
            _skillDescriptionText.text = "Select a skill to view.";
            _skillIconHolder.sprite = null;
            _buttonText.text = "Unavailable";
            _purchaseButton.interactable = false;
        }
    }
}
