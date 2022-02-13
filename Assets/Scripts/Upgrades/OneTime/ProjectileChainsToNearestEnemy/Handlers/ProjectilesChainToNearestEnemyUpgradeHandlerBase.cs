using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers
{
    public abstract class ProjectilesChainToNearestEnemyUpgradeHandlerBase : IOneTimeProjectileUpgradeHandler
    {
        protected readonly float RayCastSphereRadius = 30f;
        protected readonly float ChainedProjectileDamageMultiplier = 0.5f;

        protected readonly Weapon Weapon;

        protected ProjectilesChainToNearestEnemyUpgradeHandlerBase(Weapon weapon)
        {
            Weapon = weapon;
        }

        public abstract void ApplyUpgrade();

        public void OnEnemyHit(ProjectileBase projectile, Enemy enemy, IOneTimeProjectileUpgradeHandlerData data)
        {
            var closestEnemy = FindClosestEnemy(enemy);
            if (closestEnemy == null) return;

            var direction = closestEnemy.transform.position - projectile.transform.position;
            //TODO: do sth with that
            direction.y = 0f;
            direction.Normalize();

            var projectileRigidBody = projectile.gameObject.GetComponent<Rigidbody>();
            var projectileSpeed = projectileRigidBody.velocity.magnitude;
            var velocity = projectileSpeed * direction;

            var projectileObject = InstantiateProjectile(projectile.transform.position);
            projectileObject.color = projectile.color;
            projectileObject.nonCollidableObjects.Add(enemy.gameObject);
            projectileObject.Launch(projectile.Owner, velocity, ChainedProjectileDamageMultiplier * projectile.damage);
        }

        public void OnTerrainHit(GameObject projectile, Collider terrain, IOneTimeProjectileUpgradeHandlerData data) { }

        public void OnUpdate(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data) { }

        public void OnLaunch(ProjectileBase projectileBase, IOneTimeProjectileUpgradeHandlerData data) { }

        public void OnDestroy(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data) { }

        public void OnDrawGizmos(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data) { }

        public IOneTimeProjectileUpgradeHandlerData CreateEmptyData(ProjectileBase projectile)
        {
            return null;
        }

        protected abstract Projectile InstantiateProjectile(Vector3 position);

        protected Enemy FindClosestEnemy(Enemy startingEnemy)
        {
            var position = startingEnemy.transform.position;
            int layerMask = LayerMask.GetMask(Layers.Enemy);
            var colliders = Physics.OverlapSphere(position, RayCastSphereRadius, layerMask);

            Enemy foundEnemy = null;
            var minDistanceSquared = float.MaxValue;

            foreach (var collider in colliders)
            {
                if (collider.gameObject == startingEnemy.gameObject) continue;

                var offset = collider.transform.position - position;
                var dist = Vector3.SqrMagnitude(offset);

                if (dist >= minDistanceSquared) continue;

                minDistanceSquared = dist;

                foundEnemy = collider.GetComponent<Enemy>(); ;
            }

            return foundEnemy;
        }

        protected struct ProjectileData
        {
            public bool HasChained;

            public ProjectileData(bool hasChained)
            {
                HasChained = hasChained;
            }
        }
    }
}
