using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public class ShotgunProjectileDispersionUpgrade : UpgradeView
    {
        private const float Multiplier = 0.9f;

        private readonly Shotgun _shotgun;
        public ShotgunProjectileDispersionUpgrade(Shotgun w, Sprite sprite)
            : base($"{Weapon.GetWeaponName(w.WeaponType)} projectile dispersion",
                $"Decreases {Weapon.GetWeaponName(w.WeaponType)} projectiles dispersion by 10%",
                sprite, UpgradeType.ShotgunProjectileDispersion)
        {
            _shotgun = w;
        }

        public override UpgradeType CommitUpdate()
        {
            _shotgun.scatterFactor *= Multiplier;
            return this.upgradeType;
        }
    }
}
