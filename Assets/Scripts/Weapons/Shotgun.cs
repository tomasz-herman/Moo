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
        private readonly Projectile projectilePrefab;
        private Color color = Color.blue;

        private float scatterFactor = 10f;
        protected int projectileCount => 7;

        protected override float projectileSpeed => 2f;
        protected override float triggerTimeout => 5f;
        protected override float baseDamage => 1f;
        protected override int ammoConsumption => 3;

        public Shotgun(Projectile projectileprefab)
        {
            projectilePrefab = projectileprefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            if (projectilePrefab != null)
            {
                for (int k = 0; k < projectileCount; k++)
                {
                    Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
                    projectile.color = color;
                    var dir = Quaternion.Euler(0, Utils.RandomGaussNumber(0, scatterFactor), 0) * direction.normalized;
                    projectile.Launch(shooter, dir * projectileSpeed * shooting.projectileSpeed, baseDamage);
                }
            }
        }
    }
}
