using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers
{
    //TODO: delete
    public class ProjectilesChainToNearestEnemyUpgradeHandlerData : IOneTimeProjectileUpgradeHandlerData
    {
        private readonly ProjectileBase _projectile;

        public bool Launched;
        public bool HasChained;

        public ProjectilesChainToNearestEnemyUpgradeHandlerData(ProjectileBase projectile)
        {
            _projectile = projectile;
        }
    }
}