using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MaxHealthUpgrade : UpgradeView
    {
        public MaxHealthUpgrade()
            : base("Max Health", UpgradeType.MaxHealth, UpgradeIcon.MaxHealth, UpgradeColor.White)
        {

        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;
            return gameplay.GetHealthScalingMultiplier(upgradeCount + 1, gameplay.GetSecondaryUpgradeMultiplier());
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            var healthSystem = upgradeable.HealthSystem;

            float newHealth = GetMaxHealth(upgradeable, newFactor);

            float delta = newHealth - healthSystem.MaxHealth;

            healthSystem.MaxHealth = newHealth;
            healthSystem.Health += delta;
        }

        private float GetMaxHealth(IUpgradeable upgradeable, float factor) { return upgradeable.HealthSystem.defaultHealth * factor; }

        protected override string GetDescription(IUpgradeable upgradeable, float newFactor)
        {
            return $"Increase Max Health from {Mathf.CeilToInt(upgradeable.HealthSystem.MaxHealth)} to {Mathf.CeilToInt(GetMaxHealth(upgradeable, newFactor))}";
        }
    }
}
