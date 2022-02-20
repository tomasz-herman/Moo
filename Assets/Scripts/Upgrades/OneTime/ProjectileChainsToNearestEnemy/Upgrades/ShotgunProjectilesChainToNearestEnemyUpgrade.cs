using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class ShotgunProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public ShotgunProjectilesChainToNearestEnemyUpgrade()
            : base(WeaponType.Shotgun, UpgradeType.ShotgunProjectilesChainToNearestEnemy) {}

        protected override void CommitUpdate(IUpgradeable upgradeable)
        {
            var projectileUpgrade = new ShotgunProjectilesChainToNearestEnemyUpgradeHandler(upgradeable.ShootingSystem.Shotgun);
            projectileUpgrade.ApplyUpgrade();
        }
    }
}
