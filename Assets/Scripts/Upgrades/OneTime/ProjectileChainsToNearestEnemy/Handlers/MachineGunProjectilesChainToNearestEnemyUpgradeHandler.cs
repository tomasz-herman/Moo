using Assets.Scripts.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers
{
    public class MachineGunProjectilesChainToNearestEnemyUpgradeHandler : ProjectilesChainToNearestEnemyUpgradeHandlerBase
    {
        protected readonly MachineGun MachineGun;

        public MachineGunProjectilesChainToNearestEnemyUpgradeHandler(MachineGun machineGun) : base(machineGun)
        {
            MachineGun = machineGun;
        }

        public override void ApplyUpgrade()
        {
            Weapon.AddUpgrade(this);
        }

        protected override Projectile InstantiateProjectile(Vector3 position)
        {
            return Object.Instantiate(MachineGun.projectilePrefab, position, Quaternion.identity);
        }
    }
}
