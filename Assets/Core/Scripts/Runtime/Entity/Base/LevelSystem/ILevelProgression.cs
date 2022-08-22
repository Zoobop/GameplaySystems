using System;

namespace Entity
{
    public interface ILevelProgression
    {
        public event Action<ILevelProgression> OnExperienceChanged;
        public event Action<int> OnLevelChanged;
        public void AddExperience(int xpGain);
        public StatAttributeElement GetExperience();
        public int GetLevel();
    }
}
