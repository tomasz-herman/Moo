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
        public Projectile projectilePrefab;
        public float projectileSpeed = 2f;
        public float triggerTimeout = 5f;

        private int projectileCount = 7;

        private Color color = Color.blue;
        public Shotgun(Projectile projectileprefab)
        {
            projectilePrefab = projectileprefab;
        }

        public override void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting, AmmoSystem ammoSystem)
        {
            int dischargeCount = trigger.PullTrigger(shooting.triggerTimeout * triggerTimeout);
            if (projectilePrefab != null)
            {
                for (int i = 0; i < dischargeCount; i++)
                {
                    if (ammoSystem.Ammo > 0)
                    {
                        for (int k = 0; k < projectileCount; k++)
                        {
                            Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
                            projectile.color = color;
                            var dir = Quaternion.Euler(0, Utils.RandomGaussNumber(0, 10), 0) * direction.normalized;
                            projectile.Launch(shooter, dir * projectileSpeed * shooting.projectileSpeed);
                        }
                        ammoSystem.Ammo--;
                    }
                }
            }
        }
    }
}
