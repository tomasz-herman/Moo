using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MovementSpeedUpgrade : UpgradeView
    {
        private const float Bonus = 0.2f;

        private readonly PlayerMovement _movementSystem;

        public MovementSpeedUpgrade(PlayerMovement movementSystem, Sprite sprite)
            : base("Movement speed", "Increase movement speed", sprite, UpgradeType.MovementSpeed)
        {
            _movementSystem = movementSystem;
        }

        public override UpgradeType CommitUpdate()
        {
            _movementSystem.Speed += Bonus;
            return this.upgradeType;
        }
    }
}
