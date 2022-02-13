using Assets.Scripts.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers
{
    public class ShotgunProjectilesChainToNearestEnemyUpgradeHandler : ProjectilesChainToNearestEnemyUpgradeHandlerBase
    {
        protected readonly Shotgun Shotgun;

        public ShotgunProjectilesChainToNearestEnemyUpgradeHandler(Shotgun shotgun) : base(shotgun)
        {
            Shotgun = shotgun;
        }

        public override void ApplyUpgrade()
        {
            Weapon.AddUpgrade(this);
        }

        protected override Projectile InstantiateProjectile(Vector3 position)
        {
            return Object.Instantiate(Shotgun.bulletPrefab, position, Quaternion.identity);
        }
    }
}
