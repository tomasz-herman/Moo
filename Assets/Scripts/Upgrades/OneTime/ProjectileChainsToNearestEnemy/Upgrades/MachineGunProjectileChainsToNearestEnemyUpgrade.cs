using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class MachineGunProjectileChainsToNearestEnemyUpgrade : ProjectileChainsToNearestEnemyUpgradeBase
    {
        protected MachineGunProjectileChainsToNearestEnemyUpgrade(MachineGun machineGun, Sprite sprite, string weaponName)
            : base(machineGun, sprite, UpgradeType.MachineGunProjectileChainsToNearestEnemy) { }

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
