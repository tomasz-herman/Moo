using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public class ShotgunProjectileDispersionUpgrade : UpgradeView
    {
        private float multiplier = 0.9f;

        private readonly Shotgun shotgun;
        public ShotgunProjectileDispersionUpgrade(Shotgun w, Sprite sprite)
            : base($"SHOTGUN projectile dispersion", "Decreases shotgun projectiles dispersion by 10%", sprite)
        {
            shotgun = w;
        }

        public override UpgradeType CommitUpdate()
        {
            shotgun.scatterFactor *= multiplier;
            return UpgradeType.ShotgunProjectileDispersion;
        }
    }

}
