using UnityEngine;

namespace UI
{
    using Entity;
    
    public class StatBarsPanel : MonoBehaviour
    {
        [SerializeField] private ProgressBar _healthBar;
        [SerializeField] private ProgressBar _staminaBar;

        private IStats _stats;

        public void Bind(IStats stats)
        {
            var statAttributes = stats.AsDictionary();

            // Unbind previous
            if (_stats is not null)
            {
                var oldStats = _stats.AsDictionary();

                oldStats[StatAttributeType.Health].OnStatChanged -= _healthBar.Bind;
                oldStats[StatAttributeType.Stamina].OnStatChanged -= _staminaBar.Bind;
            }

            // Assign new
            _stats = stats;

            // Bind stats
            statAttributes[StatAttributeType.Health].OnStatChanged += _healthBar.Bind;
            statAttributes[StatAttributeType.Stamina].OnStatChanged += _staminaBar.Bind;

            _healthBar.Bind(statAttributes[StatAttributeType.Health]);
            _staminaBar.Bind(statAttributes[StatAttributeType.Stamina]);
        }
    }
}
