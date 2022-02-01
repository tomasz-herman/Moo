using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Shotgun : Weapon
    {
        private readonly Bullet bulletPrefab;
        private Color color = Color.yellow;

        public float scatterFactor { get; set; } = 10f;
        public int projectileCount { get; set; } = 10;

        public override float projectileSpeed { get; set; } = 3f;
        public override float triggerTimeout { get; set; } = 5f;
        public override float baseDamage { get; set; } = 1f;
        protected override int ammoConsumption => 3;

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
                bullet.Launch(shooter, dir * projectileSpeed * shooting.projectileSpeed, shooting.weaponDamage * baseDamage);
            }
        }
    }
}
