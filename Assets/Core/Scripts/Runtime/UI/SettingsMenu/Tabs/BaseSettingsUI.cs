using UnityEngine;

namespace UI
{
    using LocalizationSystem;
    
    public abstract class BaseSettingsUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] protected Canvas _canvas;
    }
}
