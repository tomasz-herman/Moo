using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MaxAmmoUpgrade : UpgradeView
    {
        public MaxAmmoUpgrade() : base("Max Ammo", UpgradeType.MaxAmmo) { }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;
            return gameplay.GetAmmoScalingMultiplier(upgradeCount + 1, gameplay.GetSecondaryUpgradeMultiplier());
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

        //TODO make sure everyone (player, enemies) actually change defaultCapacity value when loading from AppData

        protected override string GetDescription(IUpgradeable upgradeable, float newFactor)
        {
            return $"Increase ammo capacity from {Mathf.CeilToInt(upgradeable.AmmoSystem.MaxAmmo)} to {Mathf.CeilToInt(GetCapacity(upgradeable, newFactor))}";
        }
    }
}
