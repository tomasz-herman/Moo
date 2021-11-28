using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class GrenadeLauncher : Weapon
    {
        public Grenade grenadePrefab;
        public float projectileSpeed = 2f;
        public float triggerTimeout = 7f;

        private Color color = Color.magenta;
        public GrenadeLauncher(Grenade grenadeprefab)
        {
            grenadePrefab = grenadeprefab;
        }

        public override void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting, AmmoSystem ammoSystem)
        {
            if (ammoSystem.Ammo == 0)
                return;
            int dischargeCount = trigger.PullTrigger(shooting.triggerTimeout * triggerTimeout);
            if (grenadePrefab != null)
            {
                for (int i = 0; i < dischargeCount; i++)
                {
                    if (ammoSystem.Ammo > 0)
                    {
                        Grenade projectile = Shooting.Instantiate(grenadePrefab, position, Quaternion.identity);
                        projectile.color = color;
                        projectile.Launch(shooter, direction.normalized * projectileSpeed * shooting.projectileSpeed);
                        ammoSystem.Ammo--;
                    }
                }
            }
        }
    }
}
