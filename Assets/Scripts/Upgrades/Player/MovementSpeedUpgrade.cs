using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MovementSpeedUpgrade : UpgradeView
    {
        private float bonus = 0.2f;

        private readonly PlayerMovement movementSystem;
        public MovementSpeedUpgrade(PlayerMovement movementsystem, Sprite sprite)
            : base("Movement speed", "Increase movement speed", sprite)
        {
            movementSystem = movementsystem;
        }

        public override UpgradeType CommitUpdate()
        {
            movementSystem.Speed += bonus;
            return UpgradeType.MovementSpeed;
        }
    }
}
