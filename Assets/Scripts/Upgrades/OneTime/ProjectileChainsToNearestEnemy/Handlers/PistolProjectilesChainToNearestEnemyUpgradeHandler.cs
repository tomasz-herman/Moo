using Assets.Scripts.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers
{
    public class PistolProjectilesChainToNearestEnemyUpgradeHandler : ProjectilesChainToNearestEnemyUpgradeHandlerBase
    {
        protected readonly Pistol Pistol;

        public PistolProjectilesChainToNearestEnemyUpgradeHandler(Pistol pistol) : base(pistol)
        {
            Pistol = pistol;
        }

        public override void ApplyUpgrade()
        {
            Weapon.AddUpgrade(this);
        }

        protected override Projectile InstantiateProjectile(Vector3 position)
        {
            return Object.Instantiate(Pistol.projectilePrefab, position, Quaternion.identity);
        }
    }
}
