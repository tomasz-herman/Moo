using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class PistolProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public PistolProjectilesChainToNearestEnemyUpgrade()
            : base(WeaponType.Pistol, UpgradeType.PistolProjectilesChainToNearestEnemy) { }

        protected override void CommitUpdate(IUpgradeable upgradeable)
        {
            var projectileUpgrade = new PistolProjectilesChainToNearestEnemyUpgradeHandler(upgradeable.ShootingSystem.Pistol);
            projectileUpgrade.ApplyUpgrade();
        }
    }
}
