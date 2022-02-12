﻿using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public abstract class ProjectilesChainToNearestEnemyUpgradeBase : UpgradeView
    {
        protected readonly Weapon Weapon;
        protected ProjectilesChainToNearestEnemyUpgradeBase(Weapon weapon, Sprite sprite, UpgradeType upgradeType)
            : base($"{Weapon.GetWeaponName(weapon.WeaponType)} projectile chaining",
                $"Projectile chains to nearest enemy after hitting one with {Weapon.GetWeaponName(weapon.WeaponType)}. Chained projectile damage is decreased by 50%",
                sprite, upgradeType)
        {
            Weapon = weapon;
        }
    }
}