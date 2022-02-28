using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MaxAmmoUpgrade : UpgradeView
    {
        public MaxAmmoUpgrade()
            : base("Max Ammo", UpgradeType.MaxAmmo, UpgradeIcon.MaxAmmo, UpgradeColor.White)
        {

        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;
            return gameplay.GetPlayerAmmoScalingMultiplier(upgradeCount + 1, gameplay.GetSecondaryUpgradeMultiplier());
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            var ammoSystem = upgradeable.AmmoSystem;

            float newAmmo = GetCapacity(upgradeable, newFactor);

            float delta = newAmmo - ammoSystem.MaxAmmo;

            ammoSystem.MaxAmmo = newAmmo;
            ammoSystem.Ammo += delta;
        }

        private float GetCapacity(IUpgradeable upgradeable, float factor) { return upgradeable.AmmoSystem.defaultCapacity * factor; }

        protected override string GetDescription(IUpgradeable upgradeable, float newFactor)
        {
            return $"Increase ammo capacity from {Mathf.CeilToInt(upgradeable.AmmoSystem.MaxAmmo)} to {Mathf.CeilToInt(GetCapacity(upgradeable, newFactor))}";
        }
    }
}
