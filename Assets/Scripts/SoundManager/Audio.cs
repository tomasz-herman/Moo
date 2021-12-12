using Assets.Scripts.SoundManager;
using UnityEngine;

public class Audio
{
    private static int _audioCounter = 0;
    private static readonly float Eps = 1e-3f;

    public int Id { get; private set; }

    public AudioClip Clip { get; private set; }

    public double ClipDuration { get; private set; }

    public SoundType SoundType
    {
        get => soundType;
        set
        {
            if (_audioLibrary.Sounds.TryGetValue(value, out AudioClip audioClip))
            {
                soundType = value;
                Clip = audioClip;
                ClipDuration = (double)Clip.samples / Clip.frequency;
                if (Source != null)
                {
                    Source.clip = Clip;
                }
            }
            else
            {
                Debug.LogWarning($"There is not sound of type {value}");
            }
        }
    }

    public Assets.Scripts.SoundManager.AudioType AudioType { get; private set; }

    public Transform SourceTransform { get; private set; }

    public AudioSource Source { get; private set; }

    public PlaybackSettings PlaybackSettings { get; set; }

    /// <summary>
    /// Whether the audio is currently playing.
    /// </summary>
    public bool IsPlaying { get; private set; } = false;

    /// <summary>
    /// Whether the audio is paused.
    /// </summary>
    public bool IsPaused { get; private set; } = false;

    /// <summary>
    /// Whether the audio is stopping.
    /// </summary>
    public bool Stopping { get; private set; } = false;

    /// <summary>
    /// Whether the audio is muted.
    /// </summary>
    public bool Mute
    {
        get => mute;
        set
        {
            mute = value;
            if (Source != null)
            {
                Source.mute = mute;
            }
        }
    }

    /// <summary>
    /// Whether the audio is initialized - created and updated at least once.
    /// </summary>
    public bool IsInitialized { get; private set; } = false;

    private readonly AudioLibrary _audioLibrary;

    public Audio(Assets.Scripts.SoundManager.AudioType audioType, SoundType soundType, PlaybackSettings playbackSettings, Transform sourceTransform)
    {
        _audioLibrary = AudioLibrary.Instance;
        //Set ID
        this.Id = _audioCounter;
        _audioCounter++;

        this.AudioType = audioType;
        this.SoundType = soundType;
        this.SourceTransform = sourceTransform;
        this.PlaybackSettings = playbackSettings;
        CreateAudiosource();
    }

    /// <summary>
    /// Start playing audio clip from the beginning
    /// </summary>
    public void Play()
    {
        Play(PlaybackSettings.Volume);
    }

    /// <summary>
    /// Start playing audio clip from the beginning
    /// </summary>
    /// <param name="volume">The target volume.</param>
    public void Play(float volume)
    {
        // Recreate audiosource if it does not exist
        if (Source == null)
        {
            CreateAudiosource();
        }

        Source.Play();
        IsPlaying = true;

        fadeInterpolater = 0f;

        onFadeStartVolume = PlaybackSettings.Volume;
        targetVolume = Mathf.Clamp(volume, 0f, 1f);
    }

    /// <summary>
    /// Start playing audio clip from the beggining, it does not cancel already played audio.
    /// </summary>
    public void PlayOneShot()
    {
        PlayOneShot(1f);
    }

    /// <summary>
    /// Start playing audio clip from the beggining, it does not cancel already played audio.
    /// </summary>
    /// <param name="volumeScale">The scale of the volume (0-1).</param>
    public void PlayOneShot(float volumeScale)
    {
        // Recreate audiosource if it does not exist
        if (Source == null)
        {
            CreateAudiosource();
        }

        Source.PlayOneShot(Clip, volumeScale);
        IsPlaying = true;
        IsPaused = false;

        fadeInterpolater = 0f;

        onFadeStartVolume = PlaybackSettings.Volume;
        targetVolume = PlaybackSettings.Volume;
    }

    /// <summary>
    /// Start playing audio clip from the beggining with delay
    /// </summary>
    /// <param name="delay">Delay in seconds</param>
    public void PlayDelayed(float delay)
    {
        PlayDelayed(delay, PlaybackSettings.Volume);
    }

    /// <summary>
    /// Start playing audio clip from the beggining with delay
    /// </summary>
    /// <param name="delay">Delay in seconds</param>
    /// <param name="volume">The target volume.</param>
    public void PlayDelayed(float delay, float volume)
    {
        // Recreate audiosource if it does not exist
        if (Source == null)
        {
            CreateAudiosource();
        }

        Source.PlayDelayed(delay);
        IsPlaying = true;
        IsPaused = false;

        fadeInterpolater = 0f;

        onFadeStartVolume = PlaybackSettings.Volume;
        targetVolume = Mathf.Clamp(volume, 0f, 1f);

        //TODO: check if isplaying is true right after calling play delayed
        tempFadeSeconds = targetVolume > PlaybackSettings.Volume ? delay + PlaybackSettings.FadeInSeconds : delay + PlaybackSettings.FadeOutSeconds;
    }

    /// <summary>
    /// Start playing audio clip at scheduled time
    /// </summary>
    /// <param name="time">Time in seconds on the absolute time-line that AudioSettings.dspTime refers to for when the sound should start playing.</param>
    public void PlayScheduled(double time)
    {
        PlayScheduled(time, PlaybackSettings.Volume);
    }

    /// <summary>
    /// Start playing audio clip at scheduled time
    /// </summary>
    /// <param name="time">Time in seconds on the absolute time-line that AudioSettings.dspTime refers to for when the sound should start playing.</param>
    /// <param name="volume">The target volume.</param>
    public void PlayScheduled(double time, float volume)
    {
        // Recreate audiosource if it does not exist
        if (Source == null)
        {
            CreateAudiosource();
        }

        Source.PlayScheduled(time);
        IsPlaying = true;
        IsPaused = false;

        fadeInterpolater = 0f;

        onFadeStartVolume = PlaybackSettings.Volume;
        targetVolume = Mathf.Clamp(volume, 0f, 1f);

        //TODO: check if isplaying is true right after calling play scheduled
    }

    /// <summary>
    /// Stop playing audio clip
    /// </summary>
    /// <param name="forceStop">If true all audio will stop immediately. If false music will fade out with their own fade out seconds.</param>
    public void Stop(bool forceStop = false)
    {
        fadeInterpolater = 0f;
        onFadeStartVolume = forceStop ? 0f : this.PlaybackSettings.Volume;
        targetVolume = 0f;

        Stopping = true;
    }

    /// <summary>
    /// Stop playing audio clip
    /// </summary>
    /// <param name="fadeOutSeconds">How many seconds it needs for the audio to fade out and stop.</param>
    public void Stop(float fadeOutSeconds)
    {
        fadeInterpolater = 0f;
        onFadeStartVolume = this.PlaybackSettings.Volume;
        tempFadeSeconds = fadeOutSeconds;
        targetVolume = 0f;

        Stopping = true;
    }

    /// <summary>
    /// Pause playing audio clip
    /// </summary>
    public void Pause()
    {
        if (!IsPlaying)
            return;

        Source.Pause();
        IsPaused = true;
    }

    /// <summary>
    /// Resume playing audio clip
    /// </summary>
    public void UnPause()
    {
        if (!IsPlaying)
            return;

        Source.UnPause();
        IsPaused = false;
    }

    /// <summary>
    /// Sets the audio volume
    /// </summary>
    /// <param name="volume">The target volume</param>
    public void SetVolume(float volume)
    {
        if (volume > targetVolume)
        {
            SetVolume(volume, PlaybackSettings.FadeOutSeconds);
        }
        else
        {
            SetVolume(volume, PlaybackSettings.FadeInSeconds);
        }
    }

    /// <summary>
    /// Sets the audio volume
    /// </summary>
    /// <param name="volume">The target volume</param>
    /// <param name="fadeSeconds">How many seconds it needs for the audio to fade in/out to reach target volume. If passed, it will override the Audio's fade in/out seconds, but only for this transition</param>
    public void SetVolume(float volume, float fadeSeconds)
    {
        SetVolume(volume, fadeSeconds, PlaybackSettings.Volume);
    }

    /// <summary>
    /// Sets the audio volume
    /// </summary>
    /// <param name="volume">The target volume</param>
    /// <param name="fadeSeconds">How many seconds it needs for the audio to fade in/out to reach target volume. If passed, it will override the Audio's fade in/out seconds, but only for this transition</param>
    /// <param name="startVolume">Immediately set the volume to this value before beginning the fade.</param>
    public void SetVolume(float volume, float fadeSeconds, float startVolume)
    {
        targetVolume = Mathf.Clamp01(volume);
        fadeInterpolater = 0;
        onFadeStartVolume = Mathf.Clamp01(startVolume);
        tempFadeSeconds = fadeSeconds;
    }

    /// <summary>
    /// Update loop of the Audio. This is automatically called from the sound manager itself. Do not use this function anywhere else, as it may lead to unwanted behaviour.
    /// </summary>
    public void Update()
    {
        if (Source == null)
        {
            return;
        }

        IsInitialized = true;

        // Increase/decrease volume to reach the current target
        if (IsPlaying && !IsPaused)
        {
            if (PlaybackSettings.Volume - targetVolume > Eps)
            {
                float fadeValue;
                fadeInterpolater += Time.unscaledDeltaTime;
                if (PlaybackSettings.Volume > targetVolume)
                {
                    fadeValue = tempFadeSeconds <= 0 ? tempFadeSeconds : PlaybackSettings.FadeOutSeconds;
                }
                else
                {
                    fadeValue = tempFadeSeconds <= 0 ? tempFadeSeconds : PlaybackSettings.FadeInSeconds;
                }

                PlaybackSettings.Volume = fadeValue < Eps ? 0f : Mathf.Lerp(onFadeStartVolume, targetVolume, fadeInterpolater / fadeValue);
            }
            else if (tempFadeSeconds != -1)
            {
                tempFadeSeconds = -1;
            }
        }

        //Update Source with playback settings
        PlaybackSettings.ApplyToAudioSource(Source);

        //TODO: use mixers or do that
        // Set the volume, taking into account the global volumes as well.
        //switch (Type)
        //{
        //    case AudioType.Music:
        //        {
        //            AudioSource.volume = Volume * EazySoundManager.GlobalMusicVolume * EazySoundManager.GlobalVolume;
        //            break;
        //        }
        //    case AudioType.Sound:
        //        {
        //            AudioSource.volume = Volume * EazySoundManager.GlobalSoundsVolume * EazySoundManager.GlobalVolume;
        //            break;
        //        }
        //    case AudioType.UISound:
        //        {
        //            AudioSource.volume = Volume * EazySoundManager.GlobalUISoundsVolume * EazySoundManager.GlobalVolume;
        //            break;
        //        }
        //}

        // Completely stop audio if it finished the process of stopping
        if (PlaybackSettings.Volume < Eps && Stopping)
        {
            Source.Stop();
            Stopping = false;
            IsPlaying = false;
            IsPaused = false;
        }

        // Update playing status
        if (Source.isPlaying != IsPlaying && Application.isFocused)
        {
            IsPlaying = Source.isPlaying;
        }
    }

    private void CreateAudiosource()
    {
        Source = SourceTransform.gameObject.AddComponent<AudioSource>();
        Source.clip = Clip;
        Source.mute = Mute;

        PlaybackSettings.ApplyToAudioSource(Source);
    }

    private SoundType soundType;

    private bool mute = false;

    private float targetVolume;
    private float tempFadeSeconds;
    private float fadeInterpolater;
    private float onFadeStartVolume;
}
