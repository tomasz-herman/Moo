using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponDamageUpgrade : UpgradeView
    {
        public WeaponType WeaponType { get; private set; }
        protected WeaponDamageUpgrade(WeaponType weapon, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(weapon)} Damage", upgradeType)
        {
            WeaponType = weapon;
        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;

            return gameplay.GetDamageScalingMultiplier(upgradeCount + 1, gameplay.UpgradeScalingMultiplier);
        }

        protected override string GetDescription(IUpgradeable upgradeable, float oldFactor, float newFactor)
        {
            var weapon = upgradeable.ShootingSystem[WeaponType];

            float currentDamage = weapon.Damage;
            float newDamage = weapon.GetDamage(newFactor);

            return $"Increase {Weapon.GetWeaponName(WeaponType)} projectile damage from {currentDamage.ToString("F1")} to {newDamage.ToString("F1")}";
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            upgradeable.ShootingSystem[WeaponType].damageMultiplier = newFactor;
        }
    }

    public class PistolDamageUpgrade : WeaponDamageUpgrade
    {
        public PistolDamageUpgrade()
            : base(WeaponType.Pistol, UpgradeType.PistolDamage) { }
    }

    public class ShotgunDamageUpgrade : WeaponDamageUpgrade
    {
        public ShotgunDamageUpgrade()
            : base(WeaponType.Shotgun, UpgradeType.ShotgunDamage) { }
    }

    public class MachineGunDamageUpgrade : WeaponDamageUpgrade
    {
        public MachineGunDamageUpgrade()
            : base(WeaponType.MachineGun, UpgradeType.MachineGunDamage) { }
    }

    public class GrenadeLauncherDamageUpgrade : WeaponDamageUpgrade
    {
        public GrenadeLauncherDamageUpgrade()
            : base(WeaponType.GrenadeLauncher, UpgradeType.GrenadeLauncherDamage) { }
    }

    public class SwordDamageUpgrade : WeaponDamageUpgrade
    {
        public SwordDamageUpgrade()
            : base(WeaponType.Sword, UpgradeType.SwordDamage) { }
    }
}
