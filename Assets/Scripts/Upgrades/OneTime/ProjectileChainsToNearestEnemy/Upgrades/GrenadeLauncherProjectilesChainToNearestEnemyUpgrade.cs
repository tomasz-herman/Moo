using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class GrenadeLauncherProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public GrenadeLauncherProjectilesChainToNearestEnemyUpgrade(GrenadeLauncher grenadeLauncher, Sprite sprite)
            : base(grenadeLauncher, sprite, UpgradeType.GrenadeLauncherProjectilesChainToNearestEnemy) { }

        public override UpgradeType CommitUpdate()
        {
            var projectileUpgrade = new GrenadeLauncherProjectilesChainToNearestEnemyUpgradeHandler(Weapon as GrenadeLauncher);
            projectileUpgrade.ApplyUpgrade();
            return this.upgradeType;
        }
    }
}
