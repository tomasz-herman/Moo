using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy
{
    public class ProjectileChainsToNearestEnemyUpgrade : UpgradeView
    {
        private readonly Shooting _shootingSystem;
        public ProjectileChainsToNearestEnemyUpgrade(Shooting shootingSystem, Sprite sprite)
            : base("Projectile Chaining", "Projectile chains to nearest enemy after hitting one. Works only on pistol, machine gun and shotgun.", sprite)
        {
            _shootingSystem = shootingSystem;
        }

        public override UpgradeType CommitUpdate()
        {
            return UpgradeType.ProjectileChainsToNearestEnemy;
        }
    }
}
