using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : Weapon
    {
        private readonly Projectile projectilePrefab;

        public Pistol(Projectile projectileprefab) : base(WeaponType.Pistol, SoundType.PistolShot)
        {
            projectilePrefab = projectileprefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.color = color;
            projectile.Launch(shooter, direction.normalized * baseProjectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * baseDamage);
        }
    }
}
