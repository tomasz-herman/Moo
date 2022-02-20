using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class MachineGunProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public MachineGunProjectilesChainToNearestEnemyUpgrade()
            : base(WeaponType.MachineGun, UpgradeType.MachineGunProjectilesChainToNearestEnemy) { }

        protected override void CommitUpdate(IUpgradeable upgradeable)
        {
            var projectileUpgrade = new MachineGunProjectilesChainToNearestEnemyUpgradeHandler(upgradeable.ShootingSystem.MachineGun);
            projectileUpgrade.ApplyUpgrade();
        }
    }
}
