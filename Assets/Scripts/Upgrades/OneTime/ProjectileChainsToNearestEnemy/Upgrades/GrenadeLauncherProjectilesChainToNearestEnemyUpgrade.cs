using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class GrenadeLauncherProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public GrenadeLauncherProjectilesChainToNearestEnemyUpgrade()
            : base(WeaponType.GrenadeLauncher, UpgradeType.GrenadeLauncherProjectilesChainToNearestEnemy) { }

        protected override void CommitUpdate(IUpgradeable upgradeable)
        {
            var projectileUpgrade = new GrenadeLauncherProjectilesChainToNearestEnemyUpgradeHandler(upgradeable.ShootingSystem.GrenadeLauncher);
            projectileUpgrade.ApplyUpgrade();
        }
    }
}
