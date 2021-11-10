using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Shotgun : MonoBehaviour, IWeapon
    {
        public Projectile projectilePrefab;
        public float projectileSpeed = 2f;
        public float triggerTimeout = 3f;

        private ContinuousTrigger trigger = new ContinuousTrigger();
        public Shotgun(Projectile projectileprefab)
        {
            projectilePrefab = projectileprefab;
        }
        public void DecreaseTime()
        {
            trigger.DecreaseTime(Time.deltaTime);
        }

        public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            int dischargeCount = trigger.PullTrigger(shooting.triggerTimeout * triggerTimeout);
            if (projectilePrefab != null)
            {
                for (int i = 0; i < dischargeCount; i++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        var rand = new System.Random();

                        Projectile projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
                        var dir = Quaternion.Euler(0, -30+(k*rand.Next(5,15)), 0) * direction.normalized;
                        projectile.Launch(shooter, dir * projectileSpeed * shooting.projectileSpeed);
                    }
                }
            }
        }
    }
}
