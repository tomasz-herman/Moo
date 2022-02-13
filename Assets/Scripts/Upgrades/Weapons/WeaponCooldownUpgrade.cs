using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponCooldownUpgrade : UpgradeView
    {
        private const float Multiplier = 0.9f;

        private readonly Weapon _weapon;

        protected WeaponCooldownUpgrade(Weapon w, Sprite sprite, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(w.WeaponType)} cooldown", $"Decrease cooldown of {Weapon.GetWeaponName(w.WeaponType)} by 10%", sprite, upgradeType)
        {
            _weapon = w;
        }

        public override UpgradeType CommitUpdate()
        {
            _weapon.basetriggerTimeout *= Multiplier;
            return this.upgradeType;
        }
    }

    public class PistolCooldownUpgrade : WeaponCooldownUpgrade
    {
        public PistolCooldownUpgrade(Pistol weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.PistolCooldown) { }
    }

    public class ShotgunCooldownUpgrade : WeaponCooldownUpgrade
    {
        public ShotgunCooldownUpgrade(Shotgun weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.ShotgunCooldown) { }
    }

    public class MachineGunCooldownUpgrade : WeaponCooldownUpgrade
    {
        public MachineGunCooldownUpgrade(MachineGun weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.MachineGunCooldown) { }
    }

    public class GrenadeLauncherCooldownUpgrade : WeaponCooldownUpgrade
    {
        public GrenadeLauncherCooldownUpgrade(GrenadeLauncher weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.GrenadeLauncherCooldown) { }
    }

    public class SwordCooldownUpgrade : WeaponCooldownUpgrade
    {
        public SwordCooldownUpgrade(Sword weapon, Sprite sprite)
            : base(weapon, sprite, UpgradeType.SwordCooldown) { }
    }
}
