using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponAmmoCostUpgrade : UpgradeView
    {
        private float multiplier = 0.75f;

        private readonly Weapon weapon;
        public WeaponAmmoCostUpgrade(Weapon w, Sprite sprite, string weaponName)
            : base($"{weaponName} ammo cost", $"Decrease ammo cost of {weaponName} by 25%", sprite)
        {
            weapon = w;
        }

        public override UpgradeType CommitUpdate()
        {
            weapon.baseAmmoConsumption *= multiplier;
            return GetUpgradeType();
        }
        protected abstract UpgradeType GetUpgradeType();
    }

    public class PistolAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public PistolAmmoCostUpgrade(Pistol weapon, Sprite sprite)
            : base(weapon, sprite, "PISTOL") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.PistolCooldown;
    }
    public class ShotgunAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public ShotgunAmmoCostUpgrade(Shotgun weapon, Sprite sprite)
            : base(weapon, sprite, "SHOTGUN") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.ShotgunCooldown;
    }
    public class MachineGunAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public MachineGunAmmoCostUpgrade(MachineGun weapon, Sprite sprite)
            : base(weapon, sprite, "MACHINEGUN") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.MachineGunCooldown;
    }
    public class GrenadeLauncherAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public GrenadeLauncherAmmoCostUpgrade(GrenadeLauncher weapon, Sprite sprite)
            : base(weapon, sprite, "GRENADE LAUNCHER") { }

        protected override UpgradeType GetUpgradeType() => UpgradeType.GrenadeLauncherCooldown;
    }
}
