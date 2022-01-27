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

        public abstract float projectileSpeed { get; set; }
        public abstract float triggerTimeout { get; set; }
        public abstract float baseDamage { get; set; }
        protected abstract int ammoConsumption { get; }

        public abstract string Name { get; set; }

        public EventHandler<float> WeaponShoot;
        //public EventHandler<(float f1, string name)> WeaponShoot;
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
                    WeaponShoot?.Invoke(this, triggerTimeout);
                    Shoot(shooter, position, direction, shooting);
                    ammoSystem.Ammo -= ammoConsumption;
                }
            }
        }
        public abstract void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting);
    }
}
