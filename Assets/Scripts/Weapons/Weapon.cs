using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon
    {
        protected ContinuousTrigger trigger = new ContinuousTrigger();

        protected abstract float projectileSpeed { get; }
        protected abstract float triggerTimeout { get; }
        protected abstract float baseDamage { get; }
        protected abstract int ammoConsumption { get; }

        public void DecreaseTime()
        {
            trigger.DecreaseTime(Time.deltaTime);
        }
        public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting, AmmoSystem ammoSystem)
        {
            int dischargeCount = trigger.PullTrigger(shooting.triggerTimeout * triggerTimeout);
            for (int i = 0; i < dischargeCount; i++)
            {
                if (ammoSystem.Ammo >= ammoConsumption)
                {
                    Shoot(shooter, position, direction, shooting);
                    ammoSystem.Ammo -= ammoConsumption;
                }
            }
        }
        public abstract void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting);
    }
}
