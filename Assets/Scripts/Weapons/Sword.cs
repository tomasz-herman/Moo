using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Sword : MonoBehaviour, IWeapon
    {
        public Blade bladePrefab;
        public float projectileSpeed = 1f;
        public float triggerTimeout = 2f;

        private ContinuousTrigger trigger = new ContinuousTrigger();
        private Color color = Color.green;
        public Sword(Blade bladeprefab)
        {
            bladePrefab = bladeprefab;
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
            if (bladePrefab != null)
            {
                for (int i = 0; i < dischargeCount; i++)
                {
                    Blade blade = Instantiate(bladePrefab, position, Quaternion.identity);
                    blade.color = color;
                    blade.Launch(shooter, direction.normalized, projectileSpeed * shooting.projectileSpeed);
                }
            }
        }
    }
}
