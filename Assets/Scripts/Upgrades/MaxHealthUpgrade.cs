using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MaxHealthUpgrade : UpgradeView
    {
        private int bonus = 50;
        private readonly Player Player;
        public MaxHealthUpgrade(Player player, Sprite sprite) 
            : base("Max health", "Increase max health", sprite) 
        {
            Player = player;
        }

        public override UpgradeType CommitUpdate()
        {
            Player.healthSystem.MaxHealth += bonus;
            return UpgradeType.MaxHealth;
        }
    }
}
