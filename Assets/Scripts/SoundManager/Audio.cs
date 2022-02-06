using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.SoundManager
{
    public class Audio : IDisposable
    {
        private static int _audioCounter;
        private const float Eps = 1e-3f;

        public int Id { get; }

        public AudioClip Clip { get; private set; }

        public double ClipDuration { get; private set; }

        public SoundType SoundType
        {
            get => _soundType;
            set
            {
                if (value == SoundType.NoSound)
                {
                    Clip = null;
                    _soundType = value;
                    if (Source != null)
                    {
                        Source.clip = Clip;
                    }

                    return;
                }

                if (_audioLibrary.Sounds.TryGetValue(value, out var audioClip))
                {
                    _soundType = value;
                    Clip = audioClip;
                    ClipDuration = (double)Clip.samples / Clip.frequency;
                    if (Source != null)
                    {
                        Source.clip = Clip;
                    }

                    return;
                }

                Debug.LogWarning($"There is not sound of type {value}");
            }
        }

        public AudioType AudioType { get; private set; }

        public Transform SourceTransform { get; private set; }

        public AudioSource Source { get; private set; }

        public PlaybackSettings PlaybackSettings { get; set; }

        /// <summary>
        /// Whether the audio is currently playing.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Whether the audio is paused.
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// Whether the audio is stopping.
        /// </summary>
        public bool Stopping { get; private set; }

        /// <summary>
        /// Whether the audio is muted.
        /// </summary>
        public bool Mute
        {
            get => _mute;
            set
            {
                _mute = value;
                if (Source != null)
                {
                    Source.mute = _mute;
                }
            }
        }

        /// <summary>
        /// Whether the audio is initialized - created and updated at least once.
        /// </summary>
        public bool IsInitialized { get; private set; }

        private readonly AudioLibrary _audioLibrary;
        private readonly AudioManager _audioManager;

        public Audio(AudioType audioType, SoundType soundType, PlaybackSettings playbackSettings, Transform sourceTransform)
        {
            _audioLibrary = AudioLibrary.Instance;
            _audioManager = AudioManager.Instance;
            //Set ID
            Id = _audioCounter;
            _audioCounter++;

            AudioType = audioType;
            SoundType = soundType;
            SourceTransform = sourceTransform ?? _audioManager.gameObject.transform;
            PlaybackSettings = playbackSettings ?? new PlaybackSettings();
            CreateAudioSource();
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
            if (_disposed) return;

            if (SoundType == SoundType.NoSound) return;

            // Recreate audio source if it does not exist
            if (Source == null)
            {
                CreateAudioSource();
            }

            Source.Play();
            IsPlaying = true;

            _fadeInterpolator = 0f;

            _onFadeStartVolume = PlaybackSettings.Volume;
            _targetVolume = Mathf.Clamp(volume, 0f, 1f);
        }

        /// <summary>
        /// Start playing audio clip from the beginning, it does not cancel already played audio.
        /// </summary>
        public void PlayOneShot()
        {
            PlayOneShot(1f);
        }

        /// <summary>
        /// Start playing audio clip from the beginning, it does not cancel already played audio.
        /// </summary>
        /// <param name="volumeScale">The scale of the volume (0-1).</param>
        public void PlayOneShot(float volumeScale)
        {
            if (_disposed) return;

            if (SoundType == SoundType.NoSound) return;

            // Recreate audio source if it does not exist
            if (Source == null)
            {
                CreateAudioSource();
            }

            Source.PlayOneShot(Clip, volumeScale);
            IsPlaying = true;
            IsPaused = false;

            _fadeInterpolator = 0f;

            _onFadeStartVolume = PlaybackSettings.Volume;
            _targetVolume = PlaybackSettings.Volume;
        }

        /// <summary>
        /// Start playing audio clip from the beginning with delay
        /// </summary>
        /// <param name="delay">Delay in seconds</param>
        public void PlayDelayed(float delay)
        {
            PlayDelayed(delay, PlaybackSettings.Volume);
        }

        /// <summary>
        /// Start playing audio clip from the beginning with delay
        /// </summary>
        /// <param name="delay">Delay in seconds</param>
        /// <param name="volume">The target volume.</param>
        public void PlayDelayed(float delay, float volume)
        {
            if (_disposed) return;

            if (SoundType == SoundType.NoSound) return;

            // Recreate audio source if it does not exist
            if (Source == null)
            {
                CreateAudioSource();
            }

            Source.PlayDelayed(delay);
            IsPlaying = true;
            IsPaused = false;

            _fadeInterpolator = 0f;

            _onFadeStartVolume = PlaybackSettings.Volume;
            _targetVolume = Mathf.Clamp(volume, 0f, 1f);

            //TODO: check if isplaying is true right after calling play delayed
            _tempFadeSeconds = _targetVolume > PlaybackSettings.Volume ? delay + PlaybackSettings.FadeInSeconds : delay + PlaybackSettings.FadeOutSeconds;
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
            if (_disposed) return;

            if (SoundType == SoundType.NoSound) return;

            // Recreate audio source if it does not exist
            if (Source == null)
            {
                CreateAudioSource();
            }

            Source.PlayScheduled(time);
            IsPlaying = true;
            IsPaused = false;

            _fadeInterpolator = 0f;

            _onFadeStartVolume = PlaybackSettings.Volume;
            _targetVolume = Mathf.Clamp(volume, 0f, 1f);

            //TODO: check if isplaying is true right after calling play scheduled
        }

        /// <summary>
        /// Start playing audio clip at given position in world space.
        /// This function creates an audio source but automatically disposes of it once the clip has finished playing.
        /// It only allows to adjust volume of clip.
        /// </summary>
        public void PlayClipAtPoint(Vector3 position, float volume = 1f)
        {
            if (SoundType == SoundType.NoSound) return;

            AudioSource.PlayClipAtPoint(Clip, position, volume);
        }

        /// <summary>
        /// Stop playing audio clip
        /// </summary>
        /// <param name="forceStop">If true all audio will stop immediately. If false music will fade out with their own fade out seconds.</param>
        public void Stop(bool forceStop = false)
        {
            if (_disposed || Source == null) return;

            if (forceStop)
            {
                Source.Stop();
                Stopping = false;
                IsPlaying = false;
                IsPaused = false;
                return;
            }

            _fadeInterpolator = 0f;
            _onFadeStartVolume = PlaybackSettings.Volume;
            _targetVolume = 0f;

            Stopping = true;
        }

        /// <summary>
        /// Stop playing audio clip
        /// </summary>
        /// <param name="fadeOutSeconds">How many seconds it needs for the audio to fade out and stop.</param>
        public void Stop(float fadeOutSeconds)
        {
            if (_disposed || Source == null) return;

            _fadeInterpolator = 0f;
            _onFadeStartVolume = PlaybackSettings.Volume;
            _tempFadeSeconds = fadeOutSeconds;
            _targetVolume = 0f;

            Stopping = true;
        }

        /// <summary>
        /// Pause playing audio clip
        /// </summary>
        public void Pause()
        {
            if (_disposed || !IsPlaying || Source == null) return;

            Source.Pause();
            IsPaused = true;
        }

        /// <summary>
        /// Resume playing audio clip
        /// </summary>
        public void UnPause()
        {
            if (_disposed || !IsPlaying || Source == null) return;

            Source.UnPause();
            IsPaused = false;
        }

        /// <summary>
        /// Sets the audio volume
        /// </summary>
        /// <param name="volume">The target volume</param>
        public void SetVolume(float volume)
        {
            SetVolume(volume, volume > _targetVolume ? PlaybackSettings.FadeOutSeconds : PlaybackSettings.FadeInSeconds);
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
            if (_disposed) return;

            _targetVolume = Mathf.Clamp01(volume);
            _fadeInterpolator = 0;
            _onFadeStartVolume = Mathf.Clamp01(startVolume);
            _tempFadeSeconds = fadeSeconds;
        }

        /// <summary>
        /// Update loop of the Audio. This is automatically called from the sound manager itself. Do not use this function anywhere else, as it may lead to unwanted behaviour.
        /// </summary>
        public void Update()
        {
            if (Source == null || _disposed) return; ;

            IsInitialized = true;

            //TODO: should this be here? - dunno if this interpolation is good idea - for example for play one shot, maybe add some inheriting class that uses it
            // Increase/decrease volume to reach the current target
            if (IsPlaying && !IsPaused)
            {
                if (PlaybackSettings.Volume - _targetVolume > Eps)
                {
                    float fadeValue;
                    _fadeInterpolator += Time.unscaledDeltaTime;
                    if (PlaybackSettings.Volume > _targetVolume)
                    {
                        fadeValue = _tempFadeSeconds <= 0 ? _tempFadeSeconds : PlaybackSettings.FadeOutSeconds;
                    }
                    else
                    {
                        fadeValue = _tempFadeSeconds <= 0 ? _tempFadeSeconds : PlaybackSettings.FadeInSeconds;
                    }

                    PlaybackSettings.Volume = fadeValue < Eps ? 0f : Mathf.Lerp(_onFadeStartVolume, _targetVolume, _fadeInterpolator / fadeValue);
                }
                else if (_tempFadeSeconds <= 0)
                {
                    _tempFadeSeconds = -1;
                }
            }

            //Update Source with playback settings
            PlaybackSettings.ApplyToAudioSource(Source);

            // Completely stop audio if it finished the process of stopping
            if (PlaybackSettings.Volume < Eps && Stopping)
            {
                Source.Stop();
                Stopping = false;
                IsPlaying = false;
                IsPaused = false;
            }

            // Update playing status
            if (Source.isPlaying != IsPlaying && !IsPaused && Application.isFocused)
            {
                IsPlaying = Source.isPlaying;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            _audioManager.DeleteAudio(AudioType, Id);
            Object.Destroy(Source);
        }

        private void CreateAudioSource()
        {
            if (_disposed) return;

            Source = SourceTransform.gameObject.AddComponent<AudioSource>();
            Source.clip = Clip;
            Source.mute = Mute;
            Source.outputAudioMixerGroup = AudioType switch
            {
                AudioType.Music => _audioManager.MusicMixer,
                AudioType.Sound => _audioManager.SoundMixer,
                AudioType.UISound => _audioManager.UISoundMixer,
                _ => null
            };

            PlaybackSettings.ApplyToAudioSource(Source);
        }

        private SoundType _soundType;

        private bool _mute;

        private float _targetVolume;
        private float _tempFadeSeconds;
        private float _fadeInterpolator;
        private float _onFadeStartVolume;

        private bool _disposed;
    }
}
