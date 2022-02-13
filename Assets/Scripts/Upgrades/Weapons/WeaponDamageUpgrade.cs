using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponDamageUpgrade : UpgradeView
    {
        private const float Multiplier = 1.2f;

        private readonly Weapon _weapon;
        protected WeaponDamageUpgrade(Weapon w, Sprite sprite, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(w.WeaponType)} damage", $"Increase damage dealt by {Weapon.GetWeaponName(w.WeaponType)} by 20%", sprite, upgradeType)
        {
            _weapon = w;
        }

        public override UpgradeType CommitUpdate()
        {
            _weapon.baseDamage *= Multiplier;
            return this.upgradeType;
        }
    }

    public class PistolDamageUpgrade : WeaponDamageUpgrade
    {
        public PistolDamageUpgrade(Pistol weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.PistolDamage) { }
    }

    public class ShotgunDamageUpgrade : WeaponDamageUpgrade
    {
        public ShotgunDamageUpgrade(Shotgun weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.ShotgunDamage) { }
    }

    public class MachineGunDamageUpgrade : WeaponDamageUpgrade
    {
        public MachineGunDamageUpgrade(MachineGun weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.MachineGunDamage) { }
    }

    public class GrenadeLauncherDamageUpgrade : WeaponDamageUpgrade
    {
        public GrenadeLauncherDamageUpgrade(GrenadeLauncher weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.GrenadeLauncherDamage) { }
    }

    public class SwordDamageUpgrade : WeaponDamageUpgrade
    {
        public SwordDamageUpgrade(Sword weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.SwordDamage) { }
    }
}
