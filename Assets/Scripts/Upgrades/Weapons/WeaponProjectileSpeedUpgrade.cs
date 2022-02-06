using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponProjectileSpeedUpgrade : UpgradeView
    {
        private const float Multiplier = 1.1f;

        private readonly Weapon _weapon;

        protected WeaponProjectileSpeedUpgrade(Weapon w, Sprite sprite, UpgradeType upgradeType, string name = null, string description = null)
            : base(name ?? $"{Weapon.GetWeaponName(w.WeaponType)} projectile speed", description ?? $"Increase velocity of {Weapon.GetWeaponName(w.WeaponType)} projectiles by 10%", sprite, upgradeType)
        {
            _weapon = w;
        }

        public override UpgradeType CommitUpdate()
        {
            _weapon.baseProjectileSpeed *= Multiplier;
            return this.upgradeType;
        }
    }

    public class PistolProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public PistolProjectileSpeedUpgrade(Pistol weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.PistolProjectileSpeed) { }
    }

    public class ShotgunProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public ShotgunProjectileSpeedUpgrade(Shotgun weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.ShotgunProjectileSpeed) { }
    }

    public class MachineGunProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public MachineGunProjectileSpeedUpgrade(MachineGun weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.MachineGunProjectileSpeed) { }
    }

    public class GrenadeLauncherProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public GrenadeLauncherProjectileSpeedUpgrade(GrenadeLauncher weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.GrenadeLauncherProjectileSpeed) { }
    }

    public class SwordProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public SwordProjectileSpeedUpgrade(Sword weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.SwordProjectileSpeed, $"Increase {Weapon.GetWeaponName(weapon.WeaponType)} sweep speed", $"Increase {Weapon.GetWeaponName(weapon.WeaponType)} sweep velocity by 10%") { }
    }
}
