using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    public class MaxAmmoUpgrade : UpgradeView
    {
        private const int Bonus = 50;

        private readonly AmmoSystem _ammoSystem;

        public MaxAmmoUpgrade(AmmoSystem ammoSystem, Sprite sprite)
            : base("Max ammo", "Increase max ammo", sprite, UpgradeType.MaxAmmo)
        {
            _ammoSystem = ammoSystem;
        }

        public override UpgradeType CommitUpdate()
        {
            _ammoSystem.MaxAmmo += Bonus;
            _ammoSystem.Ammo += Bonus;
            return this.upgradeType;
        }
    }
}
