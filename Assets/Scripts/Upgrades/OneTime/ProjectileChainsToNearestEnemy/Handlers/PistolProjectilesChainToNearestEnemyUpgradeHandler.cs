using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers
{
    public class PistolProjectilesChainToNearestEnemyUpgradeHandler : ProjectilesChainToNearestEnemyUpgradeHandlerBase
    {
        protected readonly Pistol Pistol;

        public PistolProjectilesChainToNearestEnemyUpgradeHandler(Pistol pistol) : base(pistol)
        {
            Pistol = pistol;
        }

        public override void ApplyUpgrade()
        {
            Weapon.AddUpgrade(this);
        }

        public override void OnEnemyHit(ProjectileBase projectile, Enemy enemy, IOneTimeProjectileUpgradeHandlerData data)
        {
            //if(!ProjectileDataDictionary.TryGetValue(projectile, out var projectileData)) return;
            if (!(data is ProjectilesChainToNearestEnemyUpgradeHandlerData projectileData)) return;
            if (projectileData.HasChained) return;

            projectileData.HasChained = true;
            //ProjectileDataDictionary[projectile] = projectileData;

            var closestEnemy = FindClosestEnemy(enemy);
            if(closestEnemy == null) return;

            var direction = closestEnemy.transform.position - projectile.transform.position;
            //TODO: do sth with that
            direction += Vector3.up;
            direction.Normalize();

            var projectileRigidBody = projectile.gameObject.GetComponent<Rigidbody>();
            var projectileSpeed = projectileRigidBody.velocity.magnitude;
            var velocity = projectileSpeed * direction;

            Projectile projectileObject = Object.Instantiate(Pistol.projectilePrefab, projectile.transform.position, Quaternion.identity);
            projectileObject.color = Pistol.color;
            projectileObject.name = "ChainedProjectile";
            //projectileObject.SetUpgrades(Pistol.projectileUpgrades);
            projectileObject.nonCollidableObjects.Add(enemy.gameObject);
            projectileObject.Launch(projectile.Owner, velocity, ChainedProjectileDamageMultiplier * projectile.damage);
        }
    }
}
