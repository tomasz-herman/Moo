using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponDamageUpgrade : UpgradeView
    {
        private float multiplier = 2f;

        private readonly Weapon weapon;
        public WeaponDamageUpgrade(Weapon w, Sprite sprite, string weaponName)
            : base($"{weaponName} damage", $"Increase damage dealt by {weaponName} by 20%", sprite)
        {
            weapon = w;
        }

        public override UpgradeType CommitUpdate()
        {
            weapon.baseDamage *= multiplier;
            return GetUpgradeType();
        }
        protected abstract UpgradeType GetUpgradeType();
    }

    public class PistolDamageUpgrade : WeaponDamageUpgrade
    {
        public PistolDamageUpgrade(Pistol weapon, Sprite sprite)
            : base(weapon, sprite, "PISTOL") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.PistolDamage;
    }
    public class ShotgunDamageUpgrade : WeaponDamageUpgrade
    {
        public ShotgunDamageUpgrade(Shotgun weapon, Sprite sprite)
            : base(weapon, sprite, "SHOTGUN") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.ShotgunDamage;
    }
    public class MachineGunDamageUpgrade : WeaponDamageUpgrade
    {
        public MachineGunDamageUpgrade(MachineGun weapon, Sprite sprite)
            : base(weapon, sprite, "MACHINEGUN") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.MachineGunDamage;
    }
    public class GrenadeLauncherDamageUpgrade : WeaponDamageUpgrade
    {
        public GrenadeLauncherDamageUpgrade(GrenadeLauncher weapon, Sprite sprite)
            : base(weapon, sprite, "GRENADE LAUNCHER") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.GrenadeLauncherDamage;
    }
    public class SwordDamageUpgrade : WeaponDamageUpgrade
    {
        public SwordDamageUpgrade(Sword weapon, Sprite sprite)
            : base(weapon, sprite, "SWORD") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.SwordDamage;
    }
}
