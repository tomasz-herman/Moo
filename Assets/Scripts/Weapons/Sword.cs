using System.Collections.Generic;
using Assets.Scripts.SoundManager;
using Assets.Scripts.Upgrades.OneTime.Handlers;
using Assets.Scripts.Upgrades.OneTime.Upgradables;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Sword : Weapon
    {
        private readonly Blade bladePrefab;

        public Sword(Blade bladePrefab): base(WeaponType.Sword, SoundType.SwordSwing)
        {
            this.bladePrefab = bladePrefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Blade blade = Shooting.Instantiate(bladePrefab, position, Quaternion.identity);
            blade.color = color;
            blade.ProjectileUpgrades = BladeUpgrades;
            blade.Launch(shooter, direction.normalized, shooting.weaponDamageMultiplier * baseDamage, baseProjectileSpeed * shooting.projectileSpeedMultiplier);
        }

        //unfortunately
        public List<IOneTimeProjectileUpgradeHandler> BladeUpgrades { get; set; } = new List<IOneTimeProjectileUpgradeHandler>();
    }
}
