using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public class ShotgunProjectileCountUpgrade : UpgradeView
    {
        private float multiplier = 1.2f;

        private readonly Shotgun shotgun;
        public ShotgunProjectileCountUpgrade(Shotgun w, Sprite sprite)
            : base($"SHOTGUN projectile count", $"Increase number of SHOTGUN projectiles in single shot by 20%", sprite)
        {
            shotgun = w;
        }

        public override UpgradeType CommitUpdate()
        {
            shotgun.projectileCount = (int)(shotgun.projectileCount * multiplier);
            return UpgradeType.ShotgunProjectileCount;
        }
    }

}
