using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponAmmoCostUpgrade : UpgradeView
    {
        private const float Multiplier = 0.75f;

        private readonly Weapon _weapon;

        protected WeaponAmmoCostUpgrade(Weapon w, Sprite sprite, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(w.WeaponType)} ammo cost",
         $"Decrease ammo cost of {Weapon.GetWeaponName(w.WeaponType)} by 25%",
                   sprite, upgradeType)
        {
            _weapon = w;
        }

        public override UpgradeType CommitUpdate()
        {
            _weapon.baseAmmoConsumption *= Multiplier;
            return this.upgradeType;
        }
    }

    public class PistolAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public PistolAmmoCostUpgrade(Pistol weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.PistolAmmoCost) { }
    }

    public class ShotgunAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public ShotgunAmmoCostUpgrade(Shotgun weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.ShotgunAmmoCost) { }
    }

    public class MachineGunAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public MachineGunAmmoCostUpgrade(MachineGun weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.MachineGunAmmoCost) { }
    }

    public class GrenadeLauncherAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public GrenadeLauncherAmmoCostUpgrade(GrenadeLauncher weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.GrenadeLauncherAmmoCost) { }
    }
}
