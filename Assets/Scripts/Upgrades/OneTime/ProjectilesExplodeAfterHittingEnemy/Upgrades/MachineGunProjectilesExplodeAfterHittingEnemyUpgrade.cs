using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Upgrades
{
    public class MachineGunProjectilesExplodeAfterHittingEnemyUpgrade : ProjectilesExplodeAfterHittingEnemyUpgradeBase
    {
        public MachineGunProjectilesExplodeAfterHittingEnemyUpgrade(MachineGun machineGun, Sprite sprite)
            : base(machineGun, sprite, UpgradeType.MachineGunProjectilesExplodeAfterHittingEnemy) {}

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
