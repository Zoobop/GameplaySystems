using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class SkillBag : MonoBehaviour
    {
        [SerializeField] private List<PassiveSkill> _passiveSkills;
        [Min(0)] [SerializeField] private int _skillPoints;

        public int SkillPoints => _skillPoints;

        public event Action OnPointsChanged = delegate { };

        public void ModifySkillPoints(int modification)
        {
            _skillPoints += modification;

            OnPointsChanged?.Invoke();
        }

        public void AddSkill(Skill skill)
        {
            if (skill.GetType() == typeof(PassiveSkill))
            {
                AddPassiveSkill(skill as PassiveSkill);
            }
        }

        public void AddPassiveSkill(PassiveSkill passiveSkill)
        {
            _passiveSkills.Add(passiveSkill);
        }
    }
}
