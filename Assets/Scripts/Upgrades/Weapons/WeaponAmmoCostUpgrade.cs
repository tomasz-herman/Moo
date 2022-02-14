using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public abstract class WeaponAmmoCostUpgrade : UpgradeView
    {
        //we could move this multiplier to UpgradeData
        private const float Multiplier = 0.75f;
        public WeaponType WeaponType { get; private set; }
        protected WeaponAmmoCostUpgrade(WeaponType weaponType, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(weaponType)} Ammo Cost", upgradeType)
        {
            WeaponType = weaponType;
        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;

            return gameplay.GetDescendingScalingFactor(upgradeCount + 1, Multiplier, gameplay.UpgradeScalingMultiplier);
        }

        protected override string GetDescription(IUpgradeable upgradeable, float oldFactor, float newFactor)
        {
            //TODO check if you can remove oldFactor from all these methods
            var weapon = upgradeable.ShootingSystem[WeaponType];
            float currentConsumption = weapon.AmmoConsumption;
            float newConsumtpion = weapon.GetAmmoConsumption(newFactor);

            return $"Decrease ammo cost of { Weapon.GetWeaponName(WeaponType) } from {currentConsumption.ToString("F2")} to {newConsumtpion.ToString("F2")}";
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            upgradeable.ShootingSystem[WeaponType].ammoConsumptionMultiplier = newFactor;
        }
    }

    public class PistolAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public PistolAmmoCostUpgrade()
            : base(WeaponType.Pistol, UpgradeType.PistolAmmoCost) { }
    }

    public class ShotgunAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public ShotgunAmmoCostUpgrade()
            : base(WeaponType.Shotgun, UpgradeType.ShotgunAmmoCost) { }
    }

    public class MachineGunAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public MachineGunAmmoCostUpgrade()
            : base(WeaponType.MachineGun, UpgradeType.MachineGunAmmoCost) { }
    }

    public class GrenadeLauncherAmmoCostUpgrade : WeaponAmmoCostUpgrade
    {
        public GrenadeLauncherAmmoCostUpgrade()
            : base(WeaponType.GrenadeLauncher, UpgradeType.GrenadeLauncherAmmoCost) { }
    }
}
