using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class PistolProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public PistolProjectilesChainToNearestEnemyUpgrade(Pistol pistol, Sprite sprite)
            : base(pistol, sprite, UpgradeType.PistolProjectilesChainToNearestEnemy) { }

        public override UpgradeType CommitUpdate()
        {
            var projectileUpgrade = new PistolProjectilesChainToNearestEnemyUpgradeHandler(Weapon as Pistol);
            projectileUpgrade.ApplyUpgrade();
            return this.upgradeType;
        }
    }
}
