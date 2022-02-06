using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Upgrades
{
    public class PistolProjectilesExplodeAfterHittingEnemyUpgrade : ProjectilesExplodeAfterHittingEnemyUpgradeBase
    {
        public PistolProjectilesExplodeAfterHittingEnemyUpgrade(Shotgun shotgun, Sprite sprite)
            : base(shotgun, sprite, UpgradeType.PistolProjectilesExplodeAfterHittingEnemy) {}

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
