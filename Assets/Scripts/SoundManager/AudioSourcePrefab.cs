using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    public class AudioSourcePrefab : Entity
    {
        [HideInInspector]
        public GameObject Owner;

        [HideInInspector]
        public SoundTypeWithPlaybackSettings Sound;

        [HideInInspector]
        public Audio Audio;

        [HideInInspector]
        protected AudioManager AudioManager;

        [HideInInspector]
        public bool DestroyAfterPlaying;

        private bool _isInitialized;

        private void Update()
        {
            if (Owner != null)
            {
                transform.position = Owner.transform.position;
            }

            if (!_isInitialized) return;

            if(!DestroyAfterPlaying) return;

            if (Audio == null)
            {
                Destroy(gameObject);
                return;
            }

            if (Audio.IsPaused) return;

            if (Audio.IsPlaying) return;

            Destroy(gameObject);
        }

        public void InitializeSound(SoundTypeWithPlaybackSettings sound)
        {
            Sound = sound.Clone();
            AudioManager = AudioManager.Instance;
            Audio = AudioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, transform);
            _isInitialized = true;
        }

        public void PlayOneShot(bool destroy = true)
        {
            Audio?.PlayOneShot();
            DestroyAfterPlaying = destroy;
        }

        public void Play(bool destroy = true)
        {
            Audio?.Play();
            DestroyAfterPlaying = destroy;
        }
    }
}
