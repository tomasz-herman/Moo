using Assets.Scripts.SoundManager;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    /// <summary>
    /// Global sound music volume
    /// </summary>
    public float GlobalSoundsVolume { get; set; }

    /// <summary>
    /// Global UI sounds volume
    /// </summary>
    public float GlobalUISoundsVolume { get; set; }

    /// <summary>
    /// Global music volume
    /// </summary>
    public float GlobalMusicVolume { get; set; }

    private Dictionary<int, Audio> Sounds;
    private Dictionary<int, Audio> UISounds;
    private Dictionary<int, Audio> Music;

    private bool isInitialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Init();
    }

    private void Start()
    {
        //Play(Assets.Scripts.SoundManager.SoundType.BackgroundTheme);
    }

    #region Get Audio Functions

    /// <summary>
    /// Get first audio of given id
    /// </summary>
    public Audio GetAudio(int audioId)
    {
        var audio = GetSound(audioId);
        if (audio != null)
            return audio;

        audio = GetUISound(audioId);
        if (audio != null)
            return audio;

        audio = GetMusic(audioId);
        return audio;
    }

    /// <summary>
    /// Get first sound of given id
    /// </summary>
    public Audio GetSound(int audioId)
    {
        return Sounds.TryGetValue(audioId, out Audio audio) ? audio : null;
    }

    /// <summary>
    /// Get first ui sound of given id
    /// </summary>
    public Audio GetUISound(int audioId)
    {
        return UISounds.TryGetValue(audioId, out Audio audio) ? audio : null;
    }

    /// <summary>
    /// Get first music of given id
    /// </summary>
    public Audio GetMusic(int audioId)
    {
        return Music.TryGetValue(audioId, out Audio audio) ? audio : null;
    }

    /// <summary>
    /// Get first sound of given type
    /// </summary>
    /// <param name="soundType"></param>
    public Audio GetSound(SoundType soundType)
    {
        return Sounds.FirstOrDefault(x => x.Value.SoundType == soundType).Value;
    }

    /// <summary>
    /// Get first ui sound of given type
    /// </summary>
    /// <param name="soundType"></param>
    public Audio GetUISound(SoundType soundType)
    {
        return UISounds.FirstOrDefault(x => x.Value.SoundType == soundType).Value;
    }

    /// <summary>
    /// Get first music of given type
    /// </summary>
    /// <param name="soundType"></param>
    public Audio GetMusic(SoundType soundType)
    {
        return Music.FirstOrDefault(x => x.Value.SoundType == soundType).Value;
    }

    /// <summary>
    /// Get first sound matching given predicate
    /// </summary>
    /// <param name="predicate"></param>
    public Audio GetSound(Func<KeyValuePair<int, Audio>, bool> predicate)
    {
        return Sounds.FirstOrDefault(predicate).Value;
    }

    /// <summary>
    /// Get first ui sound matching given predicate
    /// </summary>
    /// <param name="predicate"></param>
    public Audio GetUISound(Func<KeyValuePair<int, Audio>, bool> predicate)
    {
        return UISounds.FirstOrDefault(predicate).Value;
    }

    /// <summary>
    /// Get first music matching given predicate
    /// </summary>
    /// <param name="predicate"></param>
    public Audio GetMusic(Func<KeyValuePair<int, Audio>, bool> predicate)
    {
        return Music.FirstOrDefault(predicate).Value;
    }

    #endregion

    #region Create Audio Functions

    /// <summary>
    /// Creates music in gameObject of AudioManager. If music already exists it overrides its playback settings. 
    /// If <paramref name="createNew"/> is true new object is created.
    /// </summary>
    /// <param name="soundType">Type of sound</param>
    /// <param name="playbackSettings">Playback settings</param>
    /// <param name="createNew">Force creating new audio object</param>
    /// <returns></returns>
    public Audio CreateMusic(SoundType soundType, PlaybackSettings playbackSettings, bool createNew = false)
    {
        if(createNew)
        {
            return CreateAudio(Assets.Scripts.SoundManager.AudioType.Music, soundType, playbackSettings, transform);
        }

        var music = GetMusic(soundType);
        if (music == null)
        {
            return CreateAudio(Assets.Scripts.SoundManager.AudioType.Music, soundType, playbackSettings, transform);
        }

        music.PlaybackSettings = playbackSettings;
        return music;
    }

    /// <summary>
    /// Creates sound
    /// </summary>
    /// <param name="soundType">Type of sound</param>
    /// <param name="playbackSettings">Playback settings</param>
    /// <param name="sourceTransform">Object of audio origin</param>
    public Audio CreateSound(SoundType soundType, PlaybackSettings playbackSettings, Transform sourceTransform)
    {
        return CreateAudio(Assets.Scripts.SoundManager.AudioType.Sound, soundType, playbackSettings, sourceTransform);
    }

    /// <summary>
    /// Creates ui sound
    /// </summary>
    /// <param name="soundType">Type of sound</param>
    /// <param name="playbackSettings">Playback settings</param>
    /// <param name="sourceTransform">Object of audio origin</param>
    public Audio CreateUISound(SoundType soundType, PlaybackSettings playbackSettings, Transform sourceTransform)
    {
        return CreateAudio(Assets.Scripts.SoundManager.AudioType.UISound, soundType, playbackSettings, sourceTransform);
    }

    private Audio CreateAudio(Assets.Scripts.SoundManager.AudioType audioType, SoundType soundType, PlaybackSettings playbackSettings, Transform sourceTransform)
    {
        var audioDict = GetAudioTypeDictionary(audioType);

        // Create the audioSource
        var audio = new Audio(audioType, soundType, playbackSettings, sourceTransform);
        audioDict.Add(audio.Id, audio);

        return audio;
    }

    #endregion

    #region Stop Functions

    /// <summary>
    /// Stop all audio playing
    /// </summary>
    /// <param name="forceStop">If true all audio will stop immediately. If false audio will fade out with their own fade out seconds.</param>
    public void StopAll(bool forceStop = false)
    {
        StopAllMusic(forceStop);
        StopAllSounds(forceStop);
        StopAllUISounds(forceStop);
    }

    /// <summary>
    /// Stop all audio playing
    /// </summary>
    /// <param name="fadeOutSeconds"> How many seconds it needs for all audio to fade out. It will override their own fade out seconds.</param>
    /// <param name="soundsFadeOutSeconds"> How many seconds it needs for all sounds to fade out. It will override their own fade out seconds.</param>
    /// <param name="uiSoundsFadeOutSeconds"> How many seconds it needs for all ui sounds to fade out. It will override their own fade out seconds.</param>
    public void StopAll(float fadeOutSeconds)
    {
        StopAllMusic(false, fadeOutSeconds);
        StopAllSounds(false, fadeOutSeconds);
        StopAllUISounds(false, fadeOutSeconds);
    }


    /// <summary>
    /// Stop all audio playing
    /// </summary>
    /// <param name="musicFadeOutSeconds"> How many seconds it needs for all music to fade out. It will override their own fade out seconds.</param>
    /// <param name="soundsFadeOutSeconds"> How many seconds it needs for all sounds to fade out. It will override their own fade out seconds.</param>
    /// <param name="uiSoundsFadeOutSeconds"> How many seconds it needs for all ui sounds to fade out. It will override their own fade out seconds.</param>
    public void StopAll(float musicFadeOutSeconds, float soundsFadeOutSeconds, float uiSoundsFadeOutSeconds)
    {
        StopAllMusic(false, musicFadeOutSeconds);
        StopAllSounds(false, soundsFadeOutSeconds);
        StopAllUISounds(false, uiSoundsFadeOutSeconds);
    }

    /// <summary>
    /// Stop all music playing
    /// </summary>
    /// <param name="forceStop">Should audio stop immediately</param>
    /// <param name="fadeOutSeconds"> How many seconds it needs for all music audio to fade out. It will override their own fade out seconds. If -1 is passed, all music will keep their own fade out seconds</param>
    public void StopAllMusic(bool forceStop = false, float? fadeOutSeconds = null)
    {
        StopAllAudioOfType(Music, forceStop, fadeOutSeconds);
    }

    /// <summary>
    /// Stop all sound fx playing
    /// </summary>
    /// <param name="forceStop">Should audio stop immediately</param>
    /// <param name="fadeOutSeconds"> How many seconds it needs for all sounds audio to fade out. It will override their own fade out seconds. If -1 is passed, all sounds will keep their own fade out seconds</param>
    public void StopAllSounds(bool forceStop = false, float? fadeOutSeconds = null)
    {
        StopAllAudioOfType(Sounds, forceStop, fadeOutSeconds);
    }

    /// <summary>
    /// Stop all UI sound playing
    /// </summary>
    /// <param name="forceStop">Should audio stop immediately</param>
    /// <param name="fadeOutSeconds"> How many seconds it needs for all ui sounds audio to fade out. It will override their own fade out seconds. If -1 is passed, all ui sounds will keep their own fade out seconds</param>
    public void StopAllUISounds(bool forceStop = false, float? fadeOutSeconds = null)
    {
        StopAllAudioOfType(UISounds, forceStop, fadeOutSeconds);
    }

    private static void StopAllAudioOfType(Dictionary<int, Audio> audioDict, bool forceStop = false, float? fadeOutSeconds = null)
    {
        foreach (var keyValue in audioDict)
        {
            var audio = keyValue.Value;
            if (forceStop)
            {
                audio.Stop(true);
                continue;
            }
            if (fadeOutSeconds.HasValue)
            {
                audio.Stop(fadeOutSeconds.Value);
                continue;
            }
            audio.Stop();
        }
    }

    #endregion

    #region Pause Functions

    /// <summary>
    /// Pause all audio playing
    /// </summary>
    public void PauseAll()
    {
        PauseAllMusic();
        PauseAllSounds();
        PauseAllUISounds();
    }

    /// <summary>
    /// Pause all music playing
    /// </summary>
    public void PauseAllMusic()
    {
        PauseAllAudio(Music);
    }

    /// <summary>
    /// Pause all sound fx playing
    /// </summary>
    public void PauseAllSounds()
    {
        PauseAllAudio(Sounds);
    }

    /// <summary>
    /// Pause all UI sound fx playing
    /// </summary>
    public void PauseAllUISounds()
    {
        PauseAllAudio(UISounds);
    }

    private static void PauseAllAudio(Dictionary<int, Audio> audioDict)
    {
        foreach (var keyValue in audioDict)
        {
            var audio = keyValue.Value;
            audio.Pause();
        }
    }

    #endregion

    #region Resume Functions

    /// <summary>
    /// Resume all audio playing
    /// </summary>
    public void UnPauseAll()
    {
        UnPauseAllMusic();
        UnPauseAllSounds();
        UnPauseAllUISounds();
    }

    /// <summary>
    /// Resume all music playing
    /// </summary>
    public void UnPauseAllMusic()
    {
        UnPauseAllAudio(Music);
    }

    /// <summary>
    /// Resume all sound playing
    /// </summary>
    public void UnPauseAllSounds()
    {
        UnPauseAllAudio(Sounds);
    }

    /// <summary>
    /// Resume all UI sound playing
    /// </summary>
    public void UnPauseAllUISounds()
    {
        UnPauseAllAudio(UISounds);
    }

    private static void UnPauseAllAudio(Dictionary<int, Audio> audioDict)
    {
        foreach (var keyValue in audioDict)
        {
            var audio = keyValue.Value;
            audio.UnPause();
        }
    }

    #endregion

    #region Play Functions

    /// <summary>
    /// Plays music. Does not stop any music already playing
    /// </summary>
    /// <param name="soundType"></param>
    /// <param name="playbackSettings"></param>
    public void PlayMusic(SoundType soundType, PlaybackSettings playbackSettings)
    {
        var audio = GetMusic(soundType) ?? CreateMusic(soundType, playbackSettings);

        if (!audio.IsPlaying)
        {
            audio.Play();
        }
    }

    /// <summary>
    /// Plays music. Switches currently playing music after <paramref name="fadeOutSeconds"/>
    /// </summary>
    /// <param name="soundType"></param>
    /// <param name="playbackSettings"></param>
    /// <param name="volume">Target volume</param>
    public void SwitchMusic(SoundType soundType, PlaybackSettings playbackSettings, float fadeOutSeconds, float volume)
    {
        var audio = GetMusic(soundType) ?? CreateMusic(soundType, playbackSettings);

        if (!audio.IsPlaying)
        {
            StopAllMusic(false, fadeOutSeconds);
            audio.PlayDelayed(fadeOutSeconds, volume);
        }
    }

    #endregion

    private void Update()
    {
        UpdateAllAudio(Sounds);
        UpdateAllAudio(UISounds);
        UpdateAllAudio(Music);
    }

    /// <summary>
    /// Updates the state of all audios of an audio dictionary
    /// </summary>
    /// <param name="audioDict">The audio dictionary to update</param>
    private void UpdateAllAudio(Dictionary<int, Audio> audioDict)
    {
        // Go through all audios and update them
        foreach (var keyValue in audioDict)
        {
            var audio = keyValue.Value;
            audio.Update();

            // Remove it if it is no longer active (playing)
            if (!audio.IsPlaying && !audio.IsPaused)
            {
                //TODO: think if it is good way to handle this
                Destroy(audio.Source);
            }
        }
    }

    private Dictionary<int, Audio> GetAudioTypeDictionary(Assets.Scripts.SoundManager.AudioType audioType)
    {
        return audioType switch
        {
            Assets.Scripts.SoundManager.AudioType.Music => Music,
            Assets.Scripts.SoundManager.AudioType.Sound => Sounds,
            Assets.Scripts.SoundManager.AudioType.UISound => UISounds,
            _ => null,
        };
    }

    private void Init()
    {
        if (isInitialized) return;

        //initialize audio library
        _ = AudioLibrary.Instance;

        Sounds = new Dictionary<int, Audio>();
        UISounds = new Dictionary<int, Audio>();
        Music = new Dictionary<int, Audio>();

        GlobalSoundsVolume = 1;
        GlobalUISoundsVolume = 1;
        GlobalMusicVolume = 1;

        isInitialized = true;
        DontDestroyOnLoad(this);
    }
}
