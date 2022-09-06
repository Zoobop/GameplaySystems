using System;
using System.Collections;
using UnityEngine;

using Entity;
using Entity.Groups;
using InputSystem;
using Settings;
using UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private GameObject _player;
    
    [FormerlySerializedAs("sceneController")] [FormerlySerializedAs("_sceneLoader")] [SerializeField] private SystemController systemController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private SettingsController settingsController;
    [SerializeField] private PlayerPartyController _playerPartyController;
    [SerializeField] private GameEventLog _gameEventLog;

    public static Character Player { get; private set; }

    public static event Action<ICharacter> OnPlayerChanged = delegate {  };

    #region UnityEvents

    private void Awake()
    {
        Player = _player.GetComponent<ICharacter>() as Character;
    }

    #endregion
    
    public static void SetCurrentPlayer(ICharacter player)
    {
        Player = player as Character;
        
        // Invoke event
        OnPlayerChanged?.Invoke(player);
    }

    public static void Play()
    {
        SystemController.LoadScenesOnPlay();
    }
}
