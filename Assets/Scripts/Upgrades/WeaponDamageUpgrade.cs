using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    class WeaponDamageUpgrade : UpgradeView
    {
        private readonly Player Player;
        public WeaponDamageUpgrade(Player player, Sprite sprite)
            : base("Weapon damage", "Increase damage", sprite)
        {
            Player = player;
        }

        public override UpgradeType CommitUpdate()
        {
            Player.shooting.weaponDamage += 2f;
            return UpgradeType.WeaponDamage;
        }
    }
}
