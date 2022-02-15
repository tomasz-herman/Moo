using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponProjectileSpeedUpgrade : UpgradeView
    {
        public WeaponType WeaponType { get; private set; }
        protected WeaponProjectileSpeedUpgrade(WeaponType weapon, UpgradeType upgradeType, string name = null)
            : base(name ?? $"{Weapon.GetWeaponName(weapon)} Projectile Speed", upgradeType)
        {
            WeaponType = weapon;
        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;

            return gameplay.GetProjectileSpeedScalingMultiplier(upgradeCount + 1, gameplay.GetSecondaryUpgradeMultiplier());
        }

        protected override string GetDescription(IUpgradeable upgradeable, float newFactor)
        {
            var weapon = upgradeable.ShootingSystem[WeaponType];

            float currentSpeed = weapon.ProjectileSpeed;
            float newSpeed = weapon.GetProjectileSpeed(newFactor);

            return $"Increase velocity of {Weapon.GetWeaponName(WeaponType)} projectiles from {currentSpeed.ToString("F1")} to {newSpeed.ToString("F1")}";
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            upgradeable.ShootingSystem[WeaponType].projectileSpeedMultiplier = newFactor;
        }
    }

    public class PistolProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public PistolProjectileSpeedUpgrade()
            : base(WeaponType.Pistol, UpgradeType.PistolProjectileSpeed) { }
    }

    public class ShotgunProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public ShotgunProjectileSpeedUpgrade()
            : base(WeaponType.Shotgun, UpgradeType.ShotgunProjectileSpeed) { }
    }

    public class MachineGunProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public MachineGunProjectileSpeedUpgrade()
            : base(WeaponType.MachineGun, UpgradeType.MachineGunProjectileSpeed) { }
    }

    public class GrenadeLauncherProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public GrenadeLauncherProjectileSpeedUpgrade()
            : base(WeaponType.GrenadeLauncher, UpgradeType.GrenadeLauncherProjectileSpeed) { }
    }

    //TODO consider removing this upgrade, it may be quite useless
    public class SwordProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public SwordProjectileSpeedUpgrade()
            : base(WeaponType.Sword, UpgradeType.SwordProjectileSpeed, $"Increase {Weapon.GetWeaponName(WeaponType.Sword)} sweep speed") 
        {

        }
        protected override string GetDescription(IUpgradeable upgradeable, float newFactor)
        {
            var weapon = upgradeable.ShootingSystem[WeaponType];

            float currentSpeed = weapon.ProjectileSpeed;
            float newSpeed = weapon.GetProjectileSpeed(newFactor);

            return $"Increase {Weapon.GetWeaponName(weapon.WeaponType)} sweep velocity from {currentSpeed.ToString("F1")} to {newSpeed.ToString("F1")}";
        }
    }
}
