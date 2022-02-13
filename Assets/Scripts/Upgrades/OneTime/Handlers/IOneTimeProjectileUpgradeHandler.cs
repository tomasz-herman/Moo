using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.Handlers
{
    public interface IOneTimeProjectileUpgradeHandler : IOneTimeUpgradeHandler
    {
        void ApplyUpgrade();
        void OnEnemyHit(ProjectileBase projectile, Enemy enemy, IOneTimeProjectileUpgradeHandlerData data = null);
        void OnTerrainHit(GameObject projectile, Collider terrain, IOneTimeProjectileUpgradeHandlerData data = null);
        void OnUpdate(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data = null);
        void OnLaunch(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data = null);
        void OnDestroy(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data = null);
        void OnDrawGizmos(ProjectileBase projectile, IOneTimeProjectileUpgradeHandlerData data = null);
        IOneTimeProjectileUpgradeHandlerData CreateEmptyData(ProjectileBase projectile);
    }
}
