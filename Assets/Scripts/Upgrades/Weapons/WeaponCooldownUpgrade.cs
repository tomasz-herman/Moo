using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponCooldownUpgrade : UpgradeView
    {
        private float multiplier = 0.9f;

        private readonly Weapon weapon;
        public WeaponCooldownUpgrade(Weapon w, Sprite sprite, string weaponName)
            : base($"{weaponName} cooldown", $"Decrease cooldown of {weaponName} by 10%", sprite)
        {
            weapon = w;
        }

        public override UpgradeType CommitUpdate()
        {
            weapon.triggerTimeout *= multiplier;
            return GetUpgradeType();
        }
        protected abstract UpgradeType GetUpgradeType();
    }

    public class PistolCooldownUpgrade : WeaponCooldownUpgrade
    {
        public PistolCooldownUpgrade(Pistol weapon, Sprite sprite)
            : base(weapon, sprite, "PISTOL") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.PistolCooldown;
    }
    public class ShotgunCooldownUpgrade : WeaponCooldownUpgrade
    {
        public ShotgunCooldownUpgrade(Shotgun weapon, Sprite sprite)
            : base(weapon, sprite, "SHOTGUN") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.ShotgunCooldown;
    }
    public class MachineGunCooldownUpgrade : WeaponCooldownUpgrade
    {
        public MachineGunCooldownUpgrade(MachineGun weapon, Sprite sprite)
            : base(weapon, sprite, "MACHINEGUN") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.MachineGunCooldown;
    }
    public class GrenadeLauncherCooldownUpgrade : WeaponCooldownUpgrade
    {
        public GrenadeLauncherCooldownUpgrade(GrenadeLauncher weapon, Sprite sprite)
            : base(weapon, sprite, "GRENADE LAUNCHER") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.GrenadeLauncherCooldown;
    }
    public class SwordCooldownUpgrade : WeaponCooldownUpgrade
    {
        public SwordCooldownUpgrade(Sword weapon, Sprite sprite)
            : base(weapon, sprite, "SWORD") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.SwordCooldown;
    }
}
