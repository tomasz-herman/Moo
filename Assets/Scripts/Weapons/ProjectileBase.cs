using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.SoundManager;
using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Upgrades.OneTime.Upgradables;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class ProjectileBase : Entity, IOneTimeProjectileUpgradable
    {
        /// <summary>
        /// Do not modify this collection
        /// </summary>
        public List<IOneTimeProjectileUpgradeHandler> projectileUpgrades { get; protected set; }
        /// <summary>
        /// Do not modify this collection
        /// </summary>
        public List<IOneTimeProjectileUpgradeHandlerData> projectileUpgradesData { get; protected set; }

        public List<GameObject> nonCollidableObjects;

        [SerializeField] private ProjectileHitTerrainParticles hitTerrainParticles;
        [SerializeField] private int particleCount = 30;
        public Color color;

        public float TimeToLive = 10f;

        public GameObject Owner;

        private float _elapsedTime = 0f;

        public float damage = 1;

        public SoundTypeWithPlaybackSettings Sound;
        public AudioSourcePrefab AudioSourcePrefab;
        protected AudioSourcePrefab AudioSourceInstance;

        protected virtual void Awake()
        {
            projectileUpgrades ??= new List<IOneTimeProjectileUpgradeHandler>();
            projectileUpgradesData ??= new List<IOneTimeProjectileUpgradeHandlerData>();
            nonCollidableObjects ??= new List<GameObject>();
        }

        protected virtual void Start()
        {
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
            for (int i = 0; i < projectileUpgrades.Count; i++)
            {
                projectileUpgrades[i].OnDestroy(this, projectileUpgradesData[i]);
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
                    playerHit.HealthSystem.Health -= damage;
                }
            }
        }

        protected virtual void PlaySound()
        {
            AudioSourceInstance?.PlayOneShot();
        }

        public void SpawnParticles(Vector3 position)
        {
            SpawnParticles(position, color);
        }

        public void SpawnParticles(Vector3 position, Color sparkColor)
        {
            SpawnParticles(position, sparkColor, particleCount);
        }

        public void SpawnParticles(Vector3 position, Color sparkColor, int particleCount)
        {
            var particles = Instantiate(hitTerrainParticles, position, Quaternion.identity);
            particles.SparkColor = sparkColor;
            particles.ParticleCount = particleCount;
        }

        public void SetUpgrades(List<IOneTimeProjectileUpgradeHandler> projectileUpgrades)
        {
            this.projectileUpgrades = projectileUpgrades;
            this.projectileUpgradesData = projectileUpgrades.Select(x => x.CreateEmptyData(this)).ToList();
        }
    }
}
