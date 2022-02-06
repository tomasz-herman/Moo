using System;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.Handlers.ProjectilesExplodeAfterHittingEnemy
{
    public class ProjectilesExplodeAfterHittingEnemyUpgradeHandlerBase : IOneTimeProjectileUpgradeHandler
    {
        public void OnEnemyHit(ProjectileBase projectile, Enemy enemy)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHit(GameObject projectile, Collider terrain)
        {
            throw new NotImplementedException();
        }

        public void OnUpdate(ProjectileBase projectile)
        {
            throw new NotImplementedException();
        }

        public void OnLaunch(ProjectileBase projectile)
        {
            throw new NotImplementedException();
        }

        public void OnDestroy(ProjectileBase projectile)
        {
            throw new NotImplementedException();
        }

        public void OnDrawGizmos(ProjectileBase projectile)
        {
            throw new NotImplementedException();
        }
    }
}
