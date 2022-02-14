using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MaxHealthUpgrade : UpgradeView
    {
        public MaxHealthUpgrade()
            : base("Max Health", UpgradeType.MaxHealth)
        {

        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;
            return gameplay.GetHealthScalingMultiplier(upgradeCount + 1, gameplay.UpgradeScalingMultiplier);
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            throw new System.NotImplementedException();
        }

        private float GetMaxHealth(IUpgradeable upgradeable, float factor) { return upgradeable.HealthSystem.defaultHealth * factor; }

        //TODO make sure everyone (player, enemies) actually change defaultHealth value when loading from AppData

        protected override string GetDescription(IUpgradeable upgradeable, float oldFactor, float newFactor)
        {
            return "Increase Max Health from ";
        }
    }
}
