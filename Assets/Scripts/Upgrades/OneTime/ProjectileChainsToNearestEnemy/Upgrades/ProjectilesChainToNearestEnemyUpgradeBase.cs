using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public abstract class ProjectilesChainToNearestEnemyUpgradeBase : OneTimeUpgradeView
    {
        public WeaponType WeaponType { get; private set; }
        protected ProjectilesChainToNearestEnemyUpgradeBase(WeaponType type, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(type)} projectile chaining", upgradeType)
        {
            WeaponType = type;
        }

        protected override sealed string GetDescription(IUpgradeable upgradeable)
        {
            return $"Projectile chains to nearest enemy after hitting one with {Weapon.GetWeaponName(WeaponType)}. Chained projectile damage is decreased by 50%";
        }
    }
}
