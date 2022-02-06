using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Upgrades
{
    public abstract class ProjectilesExplodeAfterHittingEnemyUpgradeBase : UpgradeView
    {
        private readonly Shooting _shootingSystem;

        protected ProjectilesExplodeAfterHittingEnemyUpgradeBase(Shooting shootingSystem, Sprite sprite, UpgradeType upgradeType)
            : base("Projectile explodes", "Projectile explodes after hitting enemy. Works only on pistol, machine gun and shotgun.", sprite, upgradeType)
        {
            _shootingSystem = shootingSystem;
        }
    }
}
