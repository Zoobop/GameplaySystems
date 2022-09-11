using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _audioSource;

    private const string MasterVolume = "MasterVolume";
    private const string MainMenuVolume = "MainMenuVolume";
    private const string BGMVolume = "BGMVolume";
    private const string SfxVolume = "SFXVolume";

    private AudioClip _mainMenuTheme;
    private IList<AudioClip> _bgmMusic;
    
    #region UnityEvents

    private void Awake()
    {
        Instance = this;

        _mainMenuTheme = Resources.Load<AudioClip>("Audio/Music/MainMenu");
        _bgmMusic = Resources.LoadAll<AudioClip>("Audio/Music/BGM");
    }

    private void Start()
    {
        SetMasterVolume(.5f);
        
        PlayBGM();
    }

    #endregion

    public static void SetMasterVolume(float volume)
    {
        Instance._audioMixer.SetFloat(MasterVolume, ConvertToDecibels(volume));
    }

    public static void SetMainMenuVolume(float volume)
    {
        Instance._audioMixer.SetFloat(MainMenuVolume, ConvertToDecibels(volume));
    }
    
    public static void SetBGMVolume(float volume)
    {
        Instance._audioMixer.SetFloat(BGMVolume, ConvertToDecibels(volume));
    }
    
    public static void SetSfxVolume(float volume)
    {
        Instance._audioMixer.SetFloat(SfxVolume, ConvertToDecibels(volume));
    }

    private static float ConvertToDecibels(float volume)
    {
        var safeVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        return Mathf.Log10(safeVolume) * 20f;
    }

    private void PlayBGM()
    {
        var musicToPlay = _bgmMusic[Random.Range(0, _bgmMusic.Count)];
        _audioSource.PlayOneShot(musicToPlay);
    }
}