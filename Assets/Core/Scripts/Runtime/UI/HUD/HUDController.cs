using UnityEngine;

namespace UI
{
    using Entity;
    
    public class HUDController : Controller
    {
        [Header("HUD")] [SerializeField] private StatBarsPanel _statBarsPanel;
        [SerializeField] private XPBarPanel _xpBarPanel;

        private IStats _stats;
        private ILevelProgression _levelProgression;

        public void Bind(ICharacter player)
        {
            // Bind if not null
            if (_statBarsPanel)
            {
                _stats = player.GetStats();
                _statBarsPanel.Bind(_stats);
            }

            // Bind if not null
            if (_xpBarPanel)
            {
                _levelProgression = player.GetLevelProgression();
                _xpBarPanel.Bind(_levelProgression);
            }
        }
    }
}