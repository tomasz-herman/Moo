using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MaxAmmoUpgrade : UpgradeView
    {
        private int bonus = 50;
        private readonly AmmoSystem ammoSystem;
        public MaxAmmoUpgrade(AmmoSystem ammosystem, Sprite sprite)
            : base("Max ammo", "Increase max ammo", sprite)
        {
            ammoSystem = ammosystem;
        }

        public override UpgradeType CommitUpdate()
        {
            ammoSystem.MaxAmmo += bonus;
            ammoSystem.Ammo += bonus;
            return UpgradeType.MaxAmmo;
        }
    }
}
