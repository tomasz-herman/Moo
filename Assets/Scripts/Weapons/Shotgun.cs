using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Shotgun : Weapon
    {
        public Bullet bulletPrefab { get; protected set; }

        public float baseScatterAngle { get; set; } = 70f;
        public float scatterAngle { get; set; }
        public int baseProjectileCount { get; set; } = 10;
        public int projectileCount { get; set; }

        public Shotgun(Bullet bulletPrefab) : base(WeaponType.Shotgun, SoundType.ShotgunShot)
        {
            this.bulletPrefab = bulletPrefab;
            projectileCount = baseProjectileCount;
            scatterAngle = baseScatterAngle;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            for (int k = 0; k < projectileCount; k++)
            {
                Bullet bullet = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
                bullet.color = color;
                bullet.SetUpgrades(projectileUpgrades);

                var dir = Quaternion.Euler(0, Utils.RandomTriangular(-scatterAngle/2, 0, scatterAngle/2), 0) * direction.normalized;
                bullet.Launch(shooter, dir * ProjectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * Damage);
            }
        }
    }
}
