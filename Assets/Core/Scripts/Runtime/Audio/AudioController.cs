using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [Header("References")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _mainMenuSource;
    [SerializeField] private AudioSource _bgmSource;

    private const string MasterVolume = "MasterVolume";
    private const string MainMenuVolume = "MainMenuVolume";
    private const string BGMVolume = "BGMVolume";
    private const string SfxVolume = "SFXVolume";

    private static AudioClip _mainMenuTheme;
    private static IList<AudioClip> _bgmMusic;
    
    #region UnityEvents

    private void Awake()
    {
        Instance = this;

        _mainMenuTheme = Resources.Load<AudioClip>("Audio/Music/MainMenu/Unseen Journey - Beyond Rome Theme");
        _bgmMusic = Resources.LoadAll<AudioClip>("Audio/Music/BGM");
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

    #region PlayMusic

    public static void PlayMenuMusic()
    {
        if (Instance._bgmSource.isPlaying)
            Instance._bgmSource.Stop();
        
        Instance._mainMenuSource.clip = _mainMenuTheme;
        Instance._mainMenuSource.Play();
    }
    
    public static void PlayBGM()
    {
        if (Instance._mainMenuSource.isPlaying)
            Instance._mainMenuSource.Stop();
        
        var musicToPlay = _bgmMusic[Random.Range(0, _bgmMusic.Count)];
        Instance._bgmSource.PlayOneShot(musicToPlay);
    }

    #endregion

    #region Helpers

    public static AudioMixerGroup GetMenuChannel()
    {
        return Instance._audioMixer.FindMatchingGroups("Master")[1];
    }
    
    public static AudioMixerGroup GetBGMChannel()
    {
        return Instance._audioMixer.FindMatchingGroups("Master")[2];
    }
    
    public static AudioMixerGroup GetSfxChannel()
    {
        return Instance._audioMixer.FindMatchingGroups("Master")[3];
    }
    
    private static float ConvertToDecibels(float volume)
    {
        var safeVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        return Mathf.Log10(safeVolume) * 20f;
    }

    #endregion
}