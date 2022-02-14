using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Shotgun : Weapon
    {
        public Bullet bulletPrefab { get; protected set; }

        public float baseScatterAngle { get; set; } = 70f;
        public float ScatterAngle { get { return GetScatterAngle(scatterAngleMultiplier); } }
        public float scatterAngleMultiplier { get; set; } = 1;
        public float GetScatterAngle(float multiplier) { return Mathf.RoundToInt(baseScatterAngle * multiplier); }

        public int baseProjectileCount { get; set; } = 10;
        public float projectileCountMultiplier { get; set; } = 1;
        public int ProjectileCount { get { return GetProjectileCount(projectileCountMultiplier); } }
        public int GetProjectileCount(float multiplier) { return Mathf.RoundToInt(baseProjectileCount * multiplier); }

        public Shotgun(Bullet bulletPrefab) : base(WeaponType.Shotgun, SoundType.ShotgunShot)
        {
            this.bulletPrefab = bulletPrefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            for (int k = 0; k < ProjectileCount; k++)
            {
                Bullet bullet = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
                bullet.color = color;
                bullet.SetUpgrades(projectileUpgrades);

                var dir = Quaternion.Euler(0, Utils.RandomTriangular(-ScatterAngle/2, 0, ScatterAngle/2), 0) * direction.normalized;
                bullet.Launch(shooter, dir * ProjectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * Damage);
            }
        }
    }
}
