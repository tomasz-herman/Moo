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
        public void DecreaseTime()
        {
            trigger.DecreaseTime(Time.deltaTime);
        }
        public abstract void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting);
    }
}
