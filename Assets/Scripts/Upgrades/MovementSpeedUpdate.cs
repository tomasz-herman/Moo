using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MovementSpeedUpdate : UpgradeView
    {
        private readonly Player Player;
        public MovementSpeedUpdate(Player player, Sprite sprite)
            : base("Movement speed", "Increase movement speed", sprite)
        {
            Player = player;
        }

        public override UpgradeType CommitUpdate()
        {
            Player.movement.Speed += 0.2f;
            return UpgradeType.MaxHealth;
        }
    }
}
