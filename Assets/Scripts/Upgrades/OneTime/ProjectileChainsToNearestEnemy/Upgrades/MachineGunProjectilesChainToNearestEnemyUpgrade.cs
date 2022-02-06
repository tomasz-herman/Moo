using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class MachineGunProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public MachineGunProjectilesChainToNearestEnemyUpgrade(MachineGun machineGun, Sprite sprite)
            : base(machineGun, sprite, UpgradeType.MachineGunProjectilesChainToNearestEnemy) { }

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
