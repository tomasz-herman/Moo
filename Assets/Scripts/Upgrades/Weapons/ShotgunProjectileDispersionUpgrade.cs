using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public class ShotgunProjectileDispersionUpgrade : UpgradeView
    {
        //if needed we can move this multiplier to UpgradeData
        private const float Multiplier = 0.75f;

        public ShotgunProjectileDispersionUpgrade()
            : base($"{Weapon.GetWeaponName(WeaponType.Shotgun)} Projectile Dispersion", UpgradeType.ShotgunProjectileDispersion,
                  UpgradeIcon.ShotgunProjectileDispersion, UpgradeColor.Shotgun)
        {

        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;

            return gameplay.GetDescendingScalingFactor(upgradeCount + 1, Multiplier, gameplay.GetSecondaryUpgradeMultiplier());
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            upgradeable.ShootingSystem.Shotgun.scatterAngleMultiplier = newFactor;
        }

        private int GetScatter(IUpgradeable upgradeable, float factor)
        {
            return Mathf.RoundToInt(upgradeable.ShootingSystem.Shotgun.baseScatterAngle * factor);
        }

        protected override string GetDescription(IUpgradeable upgradeable, float newFactor)
        {
            return $"Decrease {Weapon.GetWeaponName(WeaponType.Shotgun)} scatter angle from " +
                $"{upgradeable.ShootingSystem.Shotgun.ScatterAngle.ToString("F1")}° to {GetScatter(upgradeable, newFactor).ToString("F1")}°";
        }
    }
}
