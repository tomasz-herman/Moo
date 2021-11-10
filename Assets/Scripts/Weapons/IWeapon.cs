using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public interface IWeapon
    {
        void DecreaseTime();
        void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting);
    }
}
