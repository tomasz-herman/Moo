using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MachineGun : Weapon
    {
        private readonly Projectile projectilePrefab;
        private Color color = Color.cyan;


        protected override float projectileSpeed => 1.5f;
        protected override float triggerTimeout => 0.3f;
        protected override float baseDamage => 1f;//TODO: refer to damagesystem
        protected override int ammoConsumption => 1;

        public MachineGun(Projectile projectileprefab)
        {
            projectilePrefab = projectileprefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            if (projectilePrefab != null)
            {
                Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
                projectile.color = color;
                var dir = Quaternion.Euler(0, Utils.RandomGaussNumber(0, 3), 0) * direction.normalized;
                projectile.Launch(shooter, dir * projectileSpeed * shooting.projectileSpeed);
            }
        }
    }
}
