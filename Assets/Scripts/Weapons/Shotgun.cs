using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Shotgun : Weapon
    {
        private readonly Bullet bulletPrefab;

        public float scatterFactor { get; set; } = 10f;
        public int projectileCount { get; set; } = 10;


        public Shotgun(Bullet bulletprefab) : base(WeaponType.Shotgun, SoundType.ShotgunShot)
        {
            bulletPrefab = bulletprefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            for (int k = 0; k < projectileCount; k++)
            {
                Bullet bullet = Shooting.Instantiate(bulletPrefab, position, Quaternion.identity);
                bullet.color = color;
                var dir = Quaternion.Euler(0, Utils.RandomGaussNumber(0, scatterFactor), 0) * direction.normalized;
                bullet.Launch(shooter, dir * baseProjectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * baseDamage);
            }
        }
    }
}
