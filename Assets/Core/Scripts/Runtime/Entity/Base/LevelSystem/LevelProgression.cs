using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity
{
    public class LevelProgression : MonoBehaviour, ILevelProgression
    {
        [SerializeField]
        protected StatAttributeElement _xpStatElement = new(StatAttributeType.Xp, "Experience", 0, 550);

        [Min(0)] [SerializeField] private int _level;
        [SerializeField] private int _totalXp;

        public int CurrentLevel => _level;
        public int TotalXp => _totalXp;
        public int CurrentXp => _xpStatElement.Current;
        public int XpForNextLevel => _xpStatElement.Maximum;

        public event Action<ILevelProgression> OnExperienceChanged = delegate { };
        public event Action<int> OnLevelChanged = delegate { };

        #region UnityEvents

        private void OnValidate()
        {
            LevelAlgorithm(_level);
        }

        private void Update()
        {
            if (Keyboard.current.digit5Key.wasReleasedThisFrame)
            {
                AddExperience(50);
            }

            if (Keyboard.current.digit6Key.wasReleasedThisFrame)
            {
                AddExperience(100);
            }

            if (Keyboard.current.digit7Key.wasReleasedThisFrame)
            {
                AddExperience(500);
            }

            if (Keyboard.current.digit8Key.wasReleasedThisFrame)
            {
                AddExperience(1000);
            }

            if (Keyboard.current.digit8Key.wasReleasedThisFrame)
            {
                AddExperience(5000);
            }
        }

        #endregion

        public void AddExperience(int xpGain)
        {
            ComputeXp(xpGain);
        }

        public StatAttributeElement GetExperience()
        {
            return _xpStatElement;
        }

        public int GetLevel()
        {
            return _level;
        }

        #region Level Calculation

        /** Level and XP Functions
     *
     *  Functions that help automatically calculate the level of the player
     *  when loading or leveling up.
     * 
     */
        private void ComputeXp(int xpGain)
        {
            // Return if 0
            if (xpGain == 0) return;
            _totalXp += xpGain;
            _level = LevelUp(xpGain);
            //OnExperienceChanged?.Invoke(this);
        }

        private int LevelUp(int xpGain)
        {
            // Level loop
            while (true)
            {
                // Get calculated value
                var calculatedValue = _xpStatElement.Current + xpGain;

                // Check if gained xp is less than xp needed for the next level -- if true, add xpGain to currentXp and return Level
                if (calculatedValue < _xpStatElement.Maximum)
                {
                    _xpStatElement.Modify(xpGain);
                    // Debug.Log(Level + " : " + CurrentXP + "/" + NextLvlXP);
                    return _level;
                }

                // Level up -- increment level, find xp needed for next level
                _level++;
                OnLevelChanged?.Invoke(_level);

                // Store overflow
                var overflow = calculatedValue - _xpStatElement.Maximum;
                LevelAlgorithm(_level);

                // Reassign to overflow for next iteration
                xpGain = overflow;
            }
        }

        private void LevelAlgorithm(int level)
        {
            // Starting XP
            const int startXpCap = 550;

            // Algorithm
            var levelSync = level - 1f;

            var algorithm = Mathf.Rad2Deg * 2.37f + ((level * level) - level + level) * (.5f * level % 100);

            var algorithmResult = levelSync * algorithm + startXpCap;

            // Final
            var xpForNextLevel = Mathf.RoundToInt(algorithmResult);

            _xpStatElement.SetFields(0, 0, xpForNextLevel);
        }

        #endregion
    }
}
