using System;
using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Handlers
{
    //TODO: add here some probability of explosion
    public class ProjectilesExplodeAfterHittingEnemyUpgradeHandlerBase : IOneTimeProjectileUpgradeHandler
    {
        public void ApplyUpgrade()
        {
            throw new NotImplementedException();
        }

        public void OnEnemyHit(ProjectileBase projectile, Enemy enemy, IOneTimeProjectileUpgradeHandlerData data = null)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHit(GameObject projectile, Collider terrain, IOneTimeProjectileUpgradeHandlerData data = null)
        {
            throw new NotImplementedException();
        }

        public void OnUpdate(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data = null)
        {
            throw new NotImplementedException();
        }

        public void OnLaunch(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data = null)
        {
            throw new NotImplementedException();
        }

        public void OnDestroy(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data = null)
        {
            throw new NotImplementedException();
        }

        public void OnDrawGizmos(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data = null)
        {
            throw new NotImplementedException();
        }

        public IOneTimeProjectileUpgradeHandlerData CreateEmptyData(ProjectileBase projectile)
        {
            throw new NotImplementedException();
        }
    }
}
