using Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles.Upgrades
{
    public class SwordReflectsEnemyProjectilesUpgrade : UpgradeView
    {
        private readonly Sword _sword;
        public SwordReflectsEnemyProjectilesUpgrade(Sword sword, Sprite sprite)
            : base("Lightsaber", $"{Weapon.GetWeaponName(sword.WeaponType)} reflects enemy projectiles.", sprite, UpgradeType.SwordReflectsEnemyProjectiles)
        {
            _sword = sword;
        }

        public override UpgradeType CommitUpdate()
        {
            var projectileUpgrade = new SwordReflectsEnemyProjectilesUpgradeHandler(_sword);
            projectileUpgrade.ApplyUpgrade();
            return this.upgradeType;
        }
    }
}
