using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public abstract class ProjectileChainsToNearestEnemyUpgradeBase : UpgradeView
    {
        private readonly Weapon _weapon;
        protected ProjectileChainsToNearestEnemyUpgradeBase(Weapon weapon, Sprite sprite, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(weapon.WeaponType)} Projectile Chaining",
                $"Projectile chains to nearest enemy after hitting one with {Weapon.GetWeaponName(weapon.WeaponType)}. Chained projectile damage is decreased by 50%.",
                sprite, upgradeType)
        {
            _weapon = weapon;
        }
    }
}
