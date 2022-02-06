using System.Collections.Generic;
using Assets.Scripts.SoundManager;
using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Upgrades.OneTime.Upgradables;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class ProjectileBase : Entity, IOneTimeProjectileUpgradable
    {
        public List<IOneTimeProjectileUpgradeHandler> ProjectileUpgrades { get; set; } = new List<IOneTimeProjectileUpgradeHandler>();

        public SoundTypeWithPlaybackSettings Sound;

        [SerializeField] private ProjectileHitTerrainParticles hitTerrainParticles;
        [SerializeField] private int particleCount = 30;
        public Color color;

        [HideInInspector]
        public Audio Audio;

        public float TimeToLive = 10f;

        public GameObject Owner;
        protected AudioManager AudioManager;

        private float _elapsedTime = 0f;

        public float damage = 1;

        protected virtual void Start()
        {
            AudioManager = AudioManager.Instance;
            Audio = AudioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, transform);
        }

        protected virtual void FixedUpdate()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > TimeToLive)
                Destroy(gameObject);
        }

        protected virtual void Launch(GameObject owner, float damage)
        {
            Owner = owner;
            this.damage = damage;
        }

        protected virtual void OnDestroy()
        {
            foreach (var upgrade in ProjectileUpgrades)
            {
                upgrade.OnDestroy(this);
            }
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

        public void SpawnParticles(Vector3 position)
        {
            var particles = Instantiate(hitTerrainParticles, position, Quaternion.identity);
            particles.SparkColor = color;
            particles.ParticleCount = particleCount;
        }
    }
}
