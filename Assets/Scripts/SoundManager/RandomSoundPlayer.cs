using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    public class RandomSoundPlayer : MonoBehaviour
    {
        public SoundTypeWithPlaybackSettings[] RandomSoundQueue;

        [HideInInspector]
        public Audio[] RandomSoundAudio;

        public BackgroundMusicSwitchType SwitchType = BackgroundMusicSwitchType.Random;

        public float minSoundIntervalSeconds = 4f;
        public float maxSoundIntervalSeconds = 10f;

        private float currentTimeout = 0f;
        private int nextPlayingIndex = -1;

        private AudioManager _audioManager;

        // Use this for initialization
        private void Start()
        {
            this._audioManager = AudioManager.Instance;
            this.RandomSoundAudio = new Audio[this.RandomSoundQueue.Length];
            for (int i = 0; i < this.RandomSoundQueue.Length; i++)
            {
                var music = this.RandomSoundQueue[i];
                music.PlaybackSettings.Loop = false;
                this.RandomSoundAudio[i] = _audioManager.CreateSound(music.SoundType, music.PlaybackSettings, transform);
            }

            this.nextPlayingIndex = Utils.NumberBetween(0, this.RandomSoundQueue.Length - 1);
        }

        private void Update()
        {
            this.currentTimeout = Mathf.Max(this.currentTimeout - Time.deltaTime, 0f);
        }

        public void PlayNextSound()
        {
            if (this.currentTimeout > 0) return;

            var audio = this.RandomSoundAudio[this.nextPlayingIndex];
            audio.PlayOneShot();
            this.nextPlayingIndex = GetNextAudioIndex(this.nextPlayingIndex, this.RandomSoundQueue.Length, this.SwitchType);
            this.currentTimeout = Utils.FloatBetween(this.minSoundIntervalSeconds, this.maxSoundIntervalSeconds);
        }

        private static int GetNextAudioIndex(int currentlyPlayingIndex, int queueLength, BackgroundMusicSwitchType switchType)
        {
            switch (switchType)
            {
                case BackgroundMusicSwitchType.Linear:
                    return currentlyPlayingIndex + 1 < queueLength ? currentlyPlayingIndex + 1 : 0;
                case BackgroundMusicSwitchType.Random:
                    if (queueLength == 1) return 0;
                    var next = Utils.NumberBetween(0, queueLength - 2);
                    next = next < currentlyPlayingIndex ? next : next + 1;
                    return next;
                default:
                    return currentlyPlayingIndex;
            }
        }
    }
}