using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Sword : Weapon
    {
        private readonly Blade bladePrefab;
        private Blade currentBlade;

        public Sword(Blade bladePrefab) : base(WeaponType.Sword, SoundType.SwordSwing)
        {
            this.bladePrefab = bladePrefab;
            currentBlade = null;
        }

        public Sword(GameObject owner, Blade bladePrefab) : base(WeaponType.Sword, owner, SoundType.SwordSwing)
        {
            this.bladePrefab = bladePrefab;
            currentBlade = null;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            if (currentBlade != null) return;
            currentBlade = Object.Instantiate(bladePrefab, position, Quaternion.identity);
            currentBlade.color = color;
            currentBlade.SetUpgrades(projectileUpgrades);
            currentBlade.Launch(shooter, direction.normalized, shooting.weaponDamageMultiplier * Damage, ProjectileSpeed * shooting.projectileSpeedMultiplier);
        }
    }
}
