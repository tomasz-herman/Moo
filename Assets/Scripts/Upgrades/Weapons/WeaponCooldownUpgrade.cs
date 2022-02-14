﻿using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponCooldownUpgrade : UpgradeView
    {
        public WeaponType WeaponType { get; private set; }
        protected WeaponCooldownUpgrade(WeaponType weapon, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(weapon)} Cooldown", upgradeType)
        {
            WeaponType = weapon;
        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;

            return gameplay.GetTriggerTimeoutScalingMultiplier(upgradeCount + 1, gameplay.UpgradeScalingMultiplier);
        }

        protected override string GetDescription(IUpgradeable upgradeable, float oldFactor, float newFactor)
        {
            var weapon = upgradeable.ShootingSystem[WeaponType];

            float currentTimeout = weapon.TriggerTimeout;
            float newTimeout = weapon.GetTriggerTimeout(newFactor);

            return $"Decrease cooldown of {Weapon.GetWeaponName(WeaponType)} from {currentTimeout.ToString("F2")}s to {newTimeout.ToString("F2")}s";
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            upgradeable.ShootingSystem[WeaponType].ammoConsumptionMultiplier = newFactor;
        }
    }

    public class PistolCooldownUpgrade : WeaponCooldownUpgrade
    {
        public PistolCooldownUpgrade()
            : base(WeaponType.Pistol, UpgradeType.PistolCooldown) { }
    }

    public class ShotgunCooldownUpgrade : WeaponCooldownUpgrade
    {
        public ShotgunCooldownUpgrade()
            : base(WeaponType.Shotgun, UpgradeType.ShotgunCooldown) { }
    }

    public class MachineGunCooldownUpgrade : WeaponCooldownUpgrade
    {
        public MachineGunCooldownUpgrade()
            : base(WeaponType.MachineGun, UpgradeType.MachineGunCooldown) { }
    }

    public class GrenadeLauncherCooldownUpgrade : WeaponCooldownUpgrade
    {
        public GrenadeLauncherCooldownUpgrade()
            : base(WeaponType.GrenadeLauncher, UpgradeType.GrenadeLauncherCooldown) { }
    }

    public class SwordCooldownUpgrade : WeaponCooldownUpgrade
    {
        public SwordCooldownUpgrade()
            : base(WeaponType.Sword, UpgradeType.SwordCooldown) { }
    }
}
