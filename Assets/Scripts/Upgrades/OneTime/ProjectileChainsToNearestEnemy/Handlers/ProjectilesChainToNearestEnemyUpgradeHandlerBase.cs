using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

        //protected bool Launched;
        //protected bool HasChained;

        //protected GameObject HitEnemy;

        //protected Dictionary<ProjectileBase, ProjectileData> ProjectileDataDictionary = new Dictionary<ProjectileBase, ProjectileData>();

        protected ProjectilesChainToNearestEnemyUpgradeHandlerBase(Weapon weapon)
        {
            Weapon = weapon;
        }

        public abstract void ApplyUpgrade();

        public abstract void OnEnemyHit(ProjectileBase projectile, Enemy enemy, IOneTimeProjectileUpgradeHandlerData data);

        public void OnTerrainHit(GameObject projectile, Collider terrain, IOneTimeProjectileUpgradeHandlerData data) { }

        public void OnUpdate(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data) { }

        public void OnLaunch(ProjectileBase projectileBase, IOneTimeProjectileUpgradeHandlerData data)
        {
            if (projectileBase is Projectile)
            {
                //ProjectileDataDictionary.Add(projectileBase, new ProjectileData(false));
            }
        }

        public void OnDestroy(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data)
        {
            //ProjectileDataDictionary.Remove(projectile);
        }

        public void OnDrawGizmos(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data) { }

        public IOneTimeProjectileUpgradeHandlerData CreateEmptyData(ProjectileBase projectile)
        {
            return new ProjectilesChainToNearestEnemyUpgradeHandlerData(projectile);
        }

        protected Enemy FindClosestEnemy(Enemy startingEnemy)
        {
            var position = startingEnemy.transform.position;
            var projectileLayer = Layers.GetLayer(Layers.Enemy);
            int layerMask = 1 << projectileLayer;
            var colliders = Physics.OverlapSphere(position, RayCastSphereRadius, layerMask);

            Debug.Log($"Found {colliders.Length} enemies nearby");

            Enemy foundEnemy = null;
            var minDistanceSquared = float.MaxValue;

            foreach (var collider in colliders)
            {
                if (collider.gameObject == startingEnemy.gameObject) continue;

                var offset = collider.transform.position - position;
                var dist = Vector3.SqrMagnitude(offset);

                if (dist >= minDistanceSquared) continue;

                minDistanceSquared = dist;

                var en1 = collider.GetComponent<Enemy>();
                //var en2 = collider.attachedRigidbody.GetComponent<Enemy>();
                foundEnemy = en1;
            }

            Debug.Log($"Found enemy is {foundEnemy}");

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
