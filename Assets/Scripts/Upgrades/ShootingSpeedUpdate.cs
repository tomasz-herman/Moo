using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    class ShootingSpeedUpdate : UpgradeView
    {
        private readonly Player Player;
        public ShootingSpeedUpdate(Player player, Sprite sprite)
            : base("Shooting speed", "Increase shooting speed", sprite)
        {
            Player = player;
        }

        public override UpgradeType CommitUpdate()
        {
            Player.shooting.triggerTimeout -= 0.05f;
            return UpgradeType.ShootingSpeed;
        }
    }
}
