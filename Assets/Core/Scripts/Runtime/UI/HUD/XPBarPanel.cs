using TMPro;
using UnityEngine;

namespace UI
{
    using Entity;
    
    public class XPBarPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private ProgressBar _xpBar;

        private ILevelProgression _levelProgression;

        public void Bind(ILevelProgression levelProgression)
        {
            var stat = levelProgression.GetExperience();

            // Unbind previous
            if (_levelProgression is not null)
            {
                var experience = _levelProgression.GetExperience();

                experience.OnStatChanged -= _xpBar.Bind;
                _levelProgression.OnLevelChanged -= UpdateLevelText;
            }

            // Assign new
            _levelProgression = levelProgression;

            // Bind
            stat.OnStatChanged += _xpBar.Bind;
            _levelProgression.OnLevelChanged += UpdateLevelText;

            _xpBar.Bind(stat);
            UpdateLevelText(levelProgression.GetLevel());
        }

        private void UpdateLevelText(int level)
        {
            // Assign if not null
            if (_levelText)
            {
                _levelText.text = $"{level}";
            }
        }
    }
}