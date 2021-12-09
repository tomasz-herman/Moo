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
        private readonly Player Player;
        public MaxAmmoUpgrade(Player player, Sprite sprite)
            : base("Max ammo", "Increase max ammo", sprite)
        {
            Player = player;
        }

        public override UpgradeType CommitUpdate()
        {
            Player.ammoSystem.MaxAmmo += 50;
            return UpgradeType.MaxAmmo;
        }
    }
}
