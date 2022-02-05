using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.Handlers
{
    public interface IOneTimeProjectileUpgradeHandler : IOneTimeUpgradeHandler
    {
        void OnEnemyHit(ProjectileBase projectile, Enemy enemy);
        void OnTerrainHit(GameObject projectile, Collider terrain);
        void OnUpdate(ProjectileBase projectile);
        void OnLaunch(ProjectileBase projectile);
        void OnDestroy(ProjectileBase projectile);
        void OnDrawGizmos(ProjectileBase projectile);
    }
}
