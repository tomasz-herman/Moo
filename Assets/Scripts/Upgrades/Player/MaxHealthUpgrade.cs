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
        private readonly HealthSystem healthSystem;
        public MaxHealthUpgrade(HealthSystem system, Sprite sprite) 
            : base("Max health", "Increase max health", sprite) 
        {
            healthSystem = system;
        }

        public override UpgradeType CommitUpdate()
        {
            healthSystem.MaxHealth += bonus;
            return UpgradeType.MaxHealth;
        }
    }
}
