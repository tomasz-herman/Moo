using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Shotgun : Weapon
    {
        private readonly Bullet bulletPrefab;
        private Color color = Color.yellow;

        private float scatterFactor = 10f;
        protected int projectileCount => 10;

        protected override float projectileSpeed => 3f;
        protected override float triggerTimeout => 5f;
        protected override float baseDamage => 1f;
        protected override int ammoConsumption => 3;

        public Shotgun(Bullet bulletprefab)
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
