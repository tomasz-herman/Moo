using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponProjectileSpeedUpgrade : UpgradeView
    {
        private float multiplier = 1.1f;

        private readonly Weapon weapon;
        public WeaponProjectileSpeedUpgrade(Weapon w, Sprite sprite, string weaponName, string name = null, string description = null)
            : base(name ?? $"{weaponName} projectile speed", description ?? $"Increase velocity of {weaponName} projectiles by 10%", sprite)
        {
            weapon = w;
        }

        public override UpgradeType CommitUpdate()
        {
            weapon.baseProjectileSpeed *= multiplier;
            return GetUpgradeType();
        }
        protected abstract UpgradeType GetUpgradeType();
    }

    public class PistolProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public PistolProjectileSpeedUpgrade(Pistol weapon, Sprite sprite)
            : base(weapon, sprite, "PISTOL") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.PistolProjectileSpeed;
    }
    public class ShotgunProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public ShotgunProjectileSpeedUpgrade(Shotgun weapon, Sprite sprite)
            : base(weapon, sprite, "SHOTGUN") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.ShotgunProjectileSpeed;
    }
    public class MachineGunProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public MachineGunProjectileSpeedUpgrade(MachineGun weapon, Sprite sprite)
            : base(weapon, sprite, "MACHINEGUN") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.MachineGunProjectileSpeed;
    }
    public class GrenadeLauncherProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public GrenadeLauncherProjectileSpeedUpgrade(GrenadeLauncher weapon, Sprite sprite)
            : base(weapon, sprite, "GRENADE LAUNCHER") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.GrenadeLauncherProjectileSpeed;
    }
    public class SwordProjectileSpeedUpgrade : WeaponProjectileSpeedUpgrade
    {
        public SwordProjectileSpeedUpgrade(Sword weapon, Sprite sprite)
            : base(weapon, sprite, "SWORD", "Increase SWORD sweep speed", "Increase SWORD sweep velocity by 10%") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.SwordProjectileSpeed;
    }
}
