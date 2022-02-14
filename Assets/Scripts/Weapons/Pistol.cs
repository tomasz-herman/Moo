using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : Weapon
    {
        public Projectile projectilePrefab { get; protected set; }

        public Pistol(Projectile projectilePrefab) : base(WeaponType.Pistol, SoundType.PistolShot)
        {
            this.projectilePrefab = projectilePrefab;
        }

        public Pistol(GameObject owner, Projectile projectilePrefab) : base(WeaponType.Pistol, owner, SoundType.PistolShot)
        {
            this.projectilePrefab = projectilePrefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Projectile projectile = Object.Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.color = color;
            projectile.SetUpgrades(projectileUpgrades);
            projectile.Launch(shooter, direction.normalized * ProjectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * Damage);
            //TODO it would be wise to not depend on Shooting class with damage calculation, it's a mess now
        }
    }
}
