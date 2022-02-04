using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    public class MusicRoom : Entity
    {
        public SoundTypeWithPlaybackSettings Sound;

        [HideInInspector] public Audio Audio;

        public bool IsInside { get; private set; }

        public int ColliderCount { get; private set; }

        private AudioManager _audioManager;

        private void Start()
        {
            _audioManager = AudioManager.Instance;
            Audio = _audioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player == null) return;
            ColliderCount++;
            UpdateState();
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player == null) return;
            ColliderCount--;
            UpdateState();
        }

        private void UpdateState()
        {
            if (ColliderCount == 0 && IsInside)
            {
                IsInside = false;

                Audio.Pause();
                GameWorld.IdleMusicManager.UnPause();
            }
            else if (ColliderCount > 0 && !IsInside)
            {
                IsInside = true;

                if (!Audio.IsPlaying)
                {
                    Audio.Play();
                }
                else
                {
                    Audio.UnPause();
                }

                GameWorld.IdleMusicManager.Pause();
            }
        }
    }
}
