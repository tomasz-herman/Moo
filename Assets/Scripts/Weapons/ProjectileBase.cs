using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class ProjectileBase : Entity
    {
        public SoundTypeWithPlaybackSettings Sound;

        [HideInInspector]
        public Audio Audio;

        public float TimeToLive = 10f;

        protected GameObject Owner;
        protected AudioManager AudioManager;

        private float _elapsedTime = 0f;

        protected abstract float baseDamage { get; }

        protected virtual void Start()
        {
            AudioManager = AudioManager.Instance;
            Audio = AudioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, transform);
        }

        protected virtual void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > TimeToLive)
                Destroy(gameObject);
        }

        public void ApplyDamage(Collider other, float damage)
        {
            Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
            Player playerHit = other.gameObject.GetComponent<Player>();
            if (Owner != null && Owner.GetComponent<Player>() != null) // Player was shooting
            {
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(damage, Owner.GetComponent<ScoreSystem>());
                }
            }
            if (Owner == null || (Owner != null && Owner.GetComponent<Enemy>() != null)) // Enemy was shooting (if it is null it means it is dead enemy)
            {
                if (playerHit != null)
                {
                    playerHit.healthSystem.Health -= damage;
                }
            }
        }

        protected virtual void PlaySound()
        {
            Audio?.PlayOneShot();
        }

    }
}
