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

        protected override string GetDescription(IUpgradeable upgradeable, float newFactor)
        {
            return $"Increase movement speed from {upgradeable.MovementSystem.Speed.ToString("F1")} to {GetSpeed(upgradeable, newFactor).ToString("F1")}";
        }
    }
}
