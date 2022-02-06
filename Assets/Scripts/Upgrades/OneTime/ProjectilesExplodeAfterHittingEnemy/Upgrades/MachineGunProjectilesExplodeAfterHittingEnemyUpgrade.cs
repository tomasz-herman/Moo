using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Upgrades
{
    public class MachineGunProjectilesExplodeAfterHittingEnemyUpgrade : ProjectilesExplodeAfterHittingEnemyUpgradeBase
    {
        public MachineGunProjectilesExplodeAfterHittingEnemyUpgrade(Shotgun shotgun, Sprite sprite)
            : base(shotgun, sprite, UpgradeType.MachineGunProjectilesExplodeAfterHittingEnemy) {}

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
