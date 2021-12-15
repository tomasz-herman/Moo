using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    public class BackgroundMusicManager : MonoBehaviour
    {
        public SoundTypeWithPlaybackSettings[] BackgroundMusicQueue;

        [HideInInspector]
        public Audio[] BackgroundMusicAudio;

        public BackgroundMusicSwitchType SwitchType = BackgroundMusicSwitchType.Linear;

        /// <summary>
        /// Whether the audio is currently playing.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Whether the audio is paused.
        /// </summary>
        public bool IsPaused { get; private set; }

        private int _currentlyPlayingIndex = -1;
        private int _nextPlayingIndex = -1;
        private double _startTime;
        private double _nextStartTime;
        private double _dspTimePauseStart;

        private AudioManager _audioManager;

        /// <summary>
        /// Plays background music in 0.1 second
        /// </summary>
        public void Play()
        {
            IsPlaying = true;
            IsPaused = false;
            _startTime = AudioSettings.dspTime + 0.1;
            _nextStartTime = AudioSettings.dspTime + 0.1;
            _dspTimePauseStart = 0;
            _nextPlayingIndex = -1;
            _currentlyPlayingIndex = -1;
        }

        /// <summary>
        /// Pauses background music
        /// </summary>
        public void Pause()
        {
            if (!IsPlaying)
                return;

            IsPaused = true;
            _dspTimePauseStart = AudioSettings.dspTime;
            if (_currentlyPlayingIndex >= 0)
            {
                BackgroundMusicAudio[_currentlyPlayingIndex].Pause();
            }
        }

        /// <summary>
        /// Resumes background music
        /// </summary>
        public void UnPause()
        {
            if (!IsPlaying)
                return;

            IsPaused = false;
            var dspTimeDelta = AudioSettings.dspTime - _dspTimePauseStart;
            _startTime += dspTimeDelta;
            _nextStartTime += dspTimeDelta;
            if (_currentlyPlayingIndex >= 0)
            {
                BackgroundMusicAudio[_currentlyPlayingIndex].UnPause();
            }
        }

        /// <summary>
        /// Stops background music. By default it lets music to fade out.
        /// If force stop is not used it is not exactly clear when music will stop - it can be from 0 to fadeOutSeconds seconds.
        /// </summary>
        /// <param name="forceStop">If true all audio will stop immediately. If false music will fade out with their own fade out seconds.</param>
        public void Stop(bool forceStop = false)
        {
            IsPlaying = false;

            if (_currentlyPlayingIndex >= 0)
            {
                BackgroundMusicAudio[_currentlyPlayingIndex].Stop(forceStop);
            }
        }

        // Use this for initialization
        private void Start()
        {
            _audioManager = AudioManager.Instance;
            BackgroundMusicAudio = new Audio[BackgroundMusicQueue.Length];
            for (int i = 0; i < BackgroundMusicQueue.Length; i++)
            {
                var music = BackgroundMusicQueue[i];
                music.PlaybackSettings.Loop = false;
                BackgroundMusicAudio[i] = _audioManager.CreateMusic(music.SoundType, music.PlaybackSettings);
            }

            _currentlyPlayingIndex = -1;
            _nextPlayingIndex = -1;
        }

        // Update is called once per frame
        private void Update()
        {            
            if (!IsPlaying || IsPaused)
            {
                return;
            }

            var dspTime = AudioSettings.dspTime;
            if (dspTime > _nextStartTime - 1)
            {
                _nextPlayingIndex = GetNextAudioIndex(_currentlyPlayingIndex, BackgroundMusicQueue.Length, SwitchType);
                var nextAudio = BackgroundMusicAudio[_nextPlayingIndex];

                nextAudio.PlayScheduled(_nextStartTime);

                _nextStartTime += nextAudio.ClipDuration;
            }
            if (dspTime > _startTime)
            {
                if (_currentlyPlayingIndex != _nextPlayingIndex)
                {
                    _currentlyPlayingIndex = _nextPlayingIndex;
                    _startTime += BackgroundMusicAudio[_currentlyPlayingIndex].ClipDuration;
                }
            }
        }

        private void OnDestroy()
        {
            //TODO: there is some bug there when switch time comes
            Stop(true);
        }

        private static int GetNextAudioIndex(int currentlyPlayingIndex, int queueLength, BackgroundMusicSwitchType switchType)
        {
            switch (switchType)
            {
                case BackgroundMusicSwitchType.Linear:
                    return currentlyPlayingIndex + 1 < queueLength ? currentlyPlayingIndex + 1 : 0;
                case BackgroundMusicSwitchType.Random:
                    var next = Utils.NumberBetween(0, queueLength - 2);
                    next = next < currentlyPlayingIndex ? next : next + 1;
                    return next;
                default:
                    return currentlyPlayingIndex;
            }
        }
    }
}