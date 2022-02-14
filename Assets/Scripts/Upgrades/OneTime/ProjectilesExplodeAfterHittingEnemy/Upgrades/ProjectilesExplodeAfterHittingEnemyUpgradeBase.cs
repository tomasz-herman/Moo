using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Upgrades
{
    public abstract class ProjectilesExplodeAfterHittingEnemyUpgradeBase : OneTimeUpgradeView
    {
        public WeaponType WeaponType { get; private set; }

        protected ProjectilesExplodeAfterHittingEnemyUpgradeBase(WeaponType type, UpgradeType upgradeType)
            : base($"Exploding {Weapon.GetWeaponName(type)} Projectiles", upgradeType)
        {
            WeaponType = type;
        }

        protected override string GetDescription(IUpgradeable upgradeable)
        {
            return $"{Weapon.GetWeaponName(WeaponType)} projectile explodes after hitting enemy";
        }
    }
}
