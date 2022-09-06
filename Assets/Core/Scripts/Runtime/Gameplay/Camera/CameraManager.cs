using UnityEngine;

namespace CameraSystem
{
    using Entity;
    
    public class CameraManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Camera _main;
        [SerializeField] private CharacterCamera _characterCamera;

        public static CameraManager Instance { get; private set; }
        public static Camera Main { get; private set; }

        #region UnityEvents

        private void Awake()
        {
            Instance = this;
            Main = _main;

            GameManager.OnPlayerChanged += OnPlayerChangedCallBack;
        }

        #endregion

        #region CameraUtility

        private void OnPlayerChangedCallBack(ICharacter player)
        {
            // Assign camera focus
            _characterCamera.SetPlayer(player);
            _characterCamera.SetFocus(player.GetTransform());
            _characterCamera.gameObject.SetActive(true);
        }

        #endregion
    }
}
