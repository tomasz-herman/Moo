using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Upgrades
{
    public abstract class ProjectilesExplodeAfterHittingEnemyUpgradeBase : UpgradeView
    {
        private readonly Weapon _weapon;

        protected ProjectilesExplodeAfterHittingEnemyUpgradeBase(Weapon weapon, Sprite sprite, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(weapon.WeaponType)} projectile explodes",
                $"{Weapon.GetWeaponName(weapon.WeaponType)} projectile explodes after hitting enemy",
                sprite, upgradeType)
        {
            _weapon = weapon;
        }
    }
}
