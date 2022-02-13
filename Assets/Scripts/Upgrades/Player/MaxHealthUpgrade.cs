using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MaxHealthUpgrade : UpgradeView
    {
        private const int Bonus = 50;

        private readonly HealthSystem _healthSystem;

        public MaxHealthUpgrade(HealthSystem system, Sprite sprite)
            : base("Max health", "Increase max health", sprite, UpgradeType.MaxHealth)
        {
            _healthSystem = system;
        }

        public override UpgradeType CommitUpdate()
        {
            _healthSystem.MaxHealth += Bonus;
            _healthSystem.Health += Bonus;
            return this.upgradeType;
        }
    }
}
