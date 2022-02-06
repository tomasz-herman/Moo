using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Upgrades
{
    public class ShotgunProjectilesExplodeAfterHittingEnemyUpgrade : ProjectilesExplodeAfterHittingEnemyUpgradeBase
    {
        public ShotgunProjectilesExplodeAfterHittingEnemyUpgrade(Shotgun shotgun, Sprite sprite)
            : base(shotgun, sprite, UpgradeType.ShotgunProjectilesExplodeAfterHittingEnemy) {}

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
