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
        public Projectile projectilePrefab;
        public float projectileSpeed = 1.5f;
        public float triggerTimeout = 0.3f;

        private Color color = Color.cyan;
        public MachineGun(Projectile projectileprefab)
        {
            projectilePrefab = projectileprefab;
        }

        public override void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting, AmmoSystem ammoSystem)
        {
            if (ammoSystem.Ammo == 0)
                return;
            int dischargeCount = trigger.PullTrigger(shooting.triggerTimeout * triggerTimeout);
            if (projectilePrefab != null)
            {
                for (int i = 0; i < dischargeCount; i++)
                {
                    if (ammoSystem.Ammo > 0)
                    {
                        Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
                        projectile.color = color;
                        var dir = Quaternion.Euler(0, Utils.RandomGaussNumber(0, 3), 0) * direction.normalized;
                        projectile.Launch(shooter, dir * projectileSpeed * shooting.projectileSpeed);
                        ammoSystem.Ammo--;
                    }
                }
            }
        }
    }
}
