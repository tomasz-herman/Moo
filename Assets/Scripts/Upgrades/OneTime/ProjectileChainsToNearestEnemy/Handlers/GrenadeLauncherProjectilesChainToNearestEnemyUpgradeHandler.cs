using Assets.Scripts.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers
{
    public class GrenadeLauncherProjectilesChainToNearestEnemyUpgradeHandler : ProjectilesChainToNearestEnemyUpgradeHandlerBase
    {
        protected readonly GrenadeLauncher GrenadeLauncher;

        public GrenadeLauncherProjectilesChainToNearestEnemyUpgradeHandler(GrenadeLauncher grenadeLauncher) : base(grenadeLauncher)
        {
            GrenadeLauncher = grenadeLauncher;
        }

        public override void ApplyUpgrade()
        {
            Weapon.AddUpgrade(this);
        }

        protected override Projectile InstantiateProjectile(Vector3 position)
        {
            return Object.Instantiate(GrenadeLauncher.grenadePrefab, position, Quaternion.identity);
        }
    }
}
