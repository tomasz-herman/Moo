using Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles.Upgrades
{
    public class SwordReflectsEnemyProjectilesUpgrade : OneTimeUpgradeView
    {
        public SwordReflectsEnemyProjectilesUpgrade()
            : base("Lightsaber", UpgradeType.SwordReflectsEnemyProjectiles, UpgradeIcon.Lightsaber)
        {
            
        }

        protected override void CommitUpdate(IUpgradeable upgradeable)
        {
            var projectileUpgrade = new SwordReflectsEnemyProjectilesUpgradeHandler(upgradeable.ShootingSystem.Sword);
            projectileUpgrade.ApplyUpgrade();
        }

        protected override string GetDescription(IUpgradeable upgradeable)
        {
            return $"{Weapon.GetWeaponName(upgradeable.ShootingSystem.Sword.WeaponType)} reflects enemy projectiles";
        }
    }
}
