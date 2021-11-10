using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : MonoBehaviour, IWeapon
    {
        public Projectile projectilePrefab;
        public float projectileSpeed = 1f;
        public float triggerTimeout = 1f;

        private ContinuousTrigger trigger = new ContinuousTrigger();
        public Pistol(Projectile projectileprefab)
        {
            projectilePrefab = projectileprefab;
        }
        void Start()
        {

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
                    Projectile projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
                    projectile.Launch(shooter, direction.normalized * projectileSpeed * shooting.projectileSpeed);
                }
            }
        }
    }
}
