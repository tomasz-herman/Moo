using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public class ShotgunProjectileCountUpgrade : UpgradeView
    {
        public ShotgunProjectileCountUpgrade()
            : base($"{Weapon.GetWeaponName(WeaponType.Shotgun)} Projectile Count", UpgradeType.ShotgunProjectileCount)
        {

        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;
            //if it's needed, we can add a separate multiplier that would combine with damage multiplier to get projectile count
            return gameplay.GetDamageScalingMultiplier(upgradeCount + 1, gameplay.UpgradeScalingMultiplier);
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            upgradeable.ShootingSystem.Shotgun.projectileCount = GetProjectileCount(upgradeable, newFactor);
        }

        private int GetProjectileCount(IUpgradeable upgradeable, float factor)
        {
            return Mathf.RoundToInt(upgradeable.ShootingSystem.Shotgun.baseProjectileCount * factor);
        }

        protected override string GetDescription(IUpgradeable upgradeable, float oldFactor, float newFactor)
        {
            return $"Increase number of {Weapon.GetWeaponName(WeaponType.Shotgun)} projectiles in single shot from " +
                $"{GetProjectileCount(upgradeable, oldFactor)} to {GetProjectileCount(upgradeable, newFactor)}";
        }
    }
}
