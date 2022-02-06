using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class ShotgunProjectileChainsToNearestEnemyUpgrade : ProjectileChainsToNearestEnemyUpgradeBase
    {
        protected ShotgunProjectileChainsToNearestEnemyUpgrade(Shotgun shotgun, Sprite sprite)
            : base(shotgun, sprite, UpgradeType.ShotgunProjectileChainsToNearestEnemy) {}

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
