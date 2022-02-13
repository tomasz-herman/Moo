using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class ShotgunProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public ShotgunProjectilesChainToNearestEnemyUpgrade(Shotgun shotgun, Sprite sprite)
            : base(shotgun, sprite, UpgradeType.ShotgunProjectilesChainToNearestEnemy) {}

        public override UpgradeType CommitUpdate()
        {
            var projectileUpgrade = new ShotgunProjectilesChainToNearestEnemyUpgradeHandler(Weapon as Shotgun);
            projectileUpgrade.ApplyUpgrade();
            return this.upgradeType;
        }
    }
}
