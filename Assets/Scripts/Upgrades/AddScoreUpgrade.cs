using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class AddScoreUpgrade : UpgradeView
    {
        private readonly Player Player;
        public AddScoreUpgrade(Player player, Sprite sprite)
            : base("Add score", "Increase score", sprite)
        {
            Player = player;
        }

        public override UpgradeType CommitUpdate()
        {
            Player.scoreSystem.AddScore(Utils.NumberBetween(1, 10));
            return UpgradeType.Score;
        }
    }
}
