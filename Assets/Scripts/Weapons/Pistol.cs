using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : Weapon
    {
        public Projectile projectilePrefab;
        public float projectileSpeed = 1f;
        public float triggerTimeout = 1f;

        private Color color = Color.red;
        public Pistol(Projectile projectileprefab)
        {
            projectilePrefab = projectileprefab;
        }

        public override void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            int dischargeCount = trigger.PullTrigger(shooting.triggerTimeout * triggerTimeout);
            if (projectilePrefab != null)
            {
                for (int i = 0; i < dischargeCount; i++)
                {
                    Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
                    projectile.color = color;
                    projectile.Launch(shooter, direction.normalized * projectileSpeed * shooting.projectileSpeed);
                }
            }
        }
    }
}
