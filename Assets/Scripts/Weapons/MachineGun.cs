using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MachineGun : Weapon
    {
        private readonly Projectile projectilePrefab;

        private float scatterFactor = 3f;

        public MachineGun(Projectile projectileprefab) : base(WeaponType.MachineGun, SoundType.PistolShot)
        {
            projectilePrefab = projectileprefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.color = color;
            var dir = Quaternion.Euler(0, Utils.RandomGaussNumber(0, scatterFactor), 0) * direction.normalized;
            projectile.Launch(shooter, dir * baseProjectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * baseDamage);
        }
    }
}
