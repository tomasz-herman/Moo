using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MovementSpeedUpgrade : UpgradeView
    {
        public MovementSpeedUpgrade()
            : base("Movement speed", UpgradeType.MovementSpeed)
        {
        
        }

        public override float GetScalingFactor(int upgradeCount)
        {
            var gameplay = ApplicationData.GameplayData;
            return gameplay.GetMovementSpeedScalingMultiplier(upgradeCount + 1, gameplay.UpgradeScalingMultiplier);
        }

        protected override void CommitUpdate(IUpgradeable upgradeable, float newFactor)
        {
            upgradeable.MovementSystem.Speed = GetSpeed(upgradeable, newFactor);
        }

        private float GetSpeed(IUpgradeable upgradeable, float factor) { return upgradeable.MovementSystem.defaultMovementSpeed * factor; }

        protected override string GetDescription(IUpgradeable upgradeable, float oldFactor, float newFactor)
        {
            return $"Increase Movement Speed from {GetSpeed(upgradeable, oldFactor)} to {GetSpeed(upgradeable, newFactor)}";
        }
    }
}
