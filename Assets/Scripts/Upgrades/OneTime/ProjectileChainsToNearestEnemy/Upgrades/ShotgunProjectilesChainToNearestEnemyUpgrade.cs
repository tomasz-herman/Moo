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
            throw new System.NotImplementedException();
        }
    }
}
