using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy
{
    public class ProjectilesExplodeAfterHittingEnemyUpgrade : UpgradeView, IOneTimeWeaponUpgradeHandler
    {
        private readonly Shooting _shootingSystem;
        public ProjectilesExplodeAfterHittingEnemyUpgrade(Shooting shootingSystem, Sprite sprite)
            : base("Projectile explodes", "Projectile explodes after hitting enemy. Works only on pistol, machine gun and shotgun.", sprite)
        {
            _shootingSystem = shootingSystem;
        }

        public override UpgradeType CommitUpdate()
        {
            return UpgradeType.ProjectilesExplodeAfterHittingEnemy;
        }

        public void OnEnemyHit(ProjectileBase projectile, Enemy enemy)
        {
            throw new System.NotImplementedException();
        }

        public void OnTerrainHit(GameObject projectile, Collider terrain)
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate(ProjectileBase projectile)
        {
            throw new System.NotImplementedException();
        }

        public void OnLaunch(ProjectileBase projectile)
        {
            throw new System.NotImplementedException();
        }
    }
}
