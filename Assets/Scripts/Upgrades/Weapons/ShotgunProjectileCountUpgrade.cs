using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public class ShotgunProjectileCountUpgrade : UpgradeView
    {
        public ShotgunProjectileCountUpgrade()
            : base($"{Weapon.GetWeaponName(WeaponType.Shotgun)} Projectile Count", UpgradeType.ShotgunProjectileCount, UpgradeIcon.ShotgunProjectileCount, UpgradeColor.Shotgun)
        {

        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;
            //if it's needed, we can add a separate multiplier that would combine with damage multiplier to get projectile count
            return gameplay.GetPlayerDamageScalingMultiplier(upgradeCount + 1, gameplay.GetSecondaryUpgradeMultiplier());
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            upgradeable.ShootingSystem.Shotgun.projectileCountMultiplier = newFactor;
        }

        protected override string GetDescription(IUpgradeable upgradeable, float newFactor)
        {
            var shotgun = upgradeable.ShootingSystem.Shotgun;
            return $"Increase number of {Weapon.GetWeaponName(WeaponType.Shotgun)} projectiles in single shot from " +
                $"{shotgun.ProjectileCount} to {shotgun.GetProjectileCount(newFactor)}";
        }
    }
}
