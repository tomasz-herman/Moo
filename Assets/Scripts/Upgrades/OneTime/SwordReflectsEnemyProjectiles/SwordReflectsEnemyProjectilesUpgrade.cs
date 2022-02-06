using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles
{
    public class SwordReflectsEnemyProjectilesUpgrade : UpgradeView
    {
        private readonly Shooting _shootingSystem;
        public SwordReflectsEnemyProjectilesUpgrade(Shooting shootingSystem, Sprite sprite)
            : base("Lightsaber", "Sword reflects enemy projectiles.", sprite)
        {
            _shootingSystem = shootingSystem;
        }

        public override UpgradeType CommitUpdate()
        {
            var projectileUpgrade = new SwordReflectsEnemyProjectilesUpgradeHandler(_shootingSystem.Sword);
            return UpgradeType.SwordReflectsEnemyProjectiles;
        }
    }
}
