using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.SoundManager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogWarning("AudioManager instance is null!");
                }

                return _instance;
            }
        }

        private static AudioManager _instance;

        public AudioMixerGroup SoundMixer;
        public AudioMixerGroup UISoundMixer;
        public AudioMixerGroup MusicMixer;

        private Dictionary<int, Audio> Sounds;
        private Dictionary<int, Audio> UISounds;
        private Dictionary<int, Audio> Music;

        private bool isInitialized;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            Init();
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

        #region Delete Audio Functions

        public bool DeleteAudio(AudioType audioType, int id)
        {
            return GetAudioTypeDictionary(audioType)?.Remove(id) ?? false;
        }

        public bool DeleteMusic(int id)
        {
            return DeleteAudio(AudioType.Music, id);
        }

        public bool DeleteSound(int id)
        {
            return DeleteAudio(AudioType.Sound, id);
        }

        public bool DeleteUISound(AudioType audioType, int id)
        {
            return DeleteAudio(AudioType.UISound, id);
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
            if (createNew)
            {
                return CreateAudio(AudioType.Music, soundType, playbackSettings, transform);
            }

            var music = GetMusic(soundType);
            if (music == null)
            {
                return CreateAudio(AudioType.Music, soundType, playbackSettings, transform);
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
            return CreateAudio(AudioType.Sound, soundType, playbackSettings, sourceTransform);
        }

        /// <summary>
        /// Creates ui sound
        /// </summary>
        /// <param name="soundType">Type of sound</param>
        /// <param name="playbackSettings">Playback settings</param>
        /// <param name="sourceTransform">Object of audio origin</param>
        public Audio CreateUISound(SoundType soundType, PlaybackSettings playbackSettings, Transform sourceTransform)
        {
            return CreateAudio(AudioType.UISound, soundType, playbackSettings, sourceTransform);
        }

        private Audio CreateAudio(AudioType audioType, SoundType soundType, PlaybackSettings playbackSettings, Transform sourceTransform)
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
            }
        }

        private Dictionary<int, Audio> GetAudioTypeDictionary(AudioType audioType)
        {
            return audioType switch
            {
                AudioType.Music => Music,
                AudioType.Sound => Sounds,
                AudioType.UISound => UISounds,
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

            isInitialized = true;
            DontDestroyOnLoad(this);
        }

        public float SoundVolume
        {
            get
            {
                SoundMixer.audioMixer.GetFloat("volume", out float volume);
                return volume;
            }
            set
            {
                SoundMixer.audioMixer.SetFloat("volume", value);
                Config.Entry.soundVolume = value;
            }
        }

        public float MusicVolume
        {
            get
            {
                MusicMixer.audioMixer.GetFloat("volume", out float volume);
                return volume;
            }
            set
            {
                MusicMixer.audioMixer.SetFloat("volume", value);
                Config.Entry.musicVolume = value;
            }
        }

        public float UiVolume
        {
            get
            {
                UISoundMixer.audioMixer.GetFloat("volume", out float volume);
                return volume;
            }
            set
            {
                UISoundMixer.audioMixer.SetFloat("volume", value);
                Config.Entry.uiVolume = value;
            }
        }
    }
}
