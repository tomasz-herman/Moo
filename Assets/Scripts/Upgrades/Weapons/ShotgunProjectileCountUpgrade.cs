using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.Weapons
{
    public class ShotgunProjectileCountUpgrade : UpgradeView
    {
        private const float Multiplier = 1.2f;

        private readonly Shotgun _shotgun;
        public ShotgunProjectileCountUpgrade(Shotgun w, Sprite sprite)
            : base($"{Weapon.GetWeaponName(w.WeaponType)} projectile count",
                $"Increase number of {Weapon.GetWeaponName(w.WeaponType)} projectiles in single shot by 20%",
                sprite, UpgradeType.ShotgunProjectileCount)
        {
            _shotgun = w;
        }

        public override UpgradeType CommitUpdate()
        {
            _shotgun.projectileCount = (int)(_shotgun.projectileCount * Multiplier);
            return this.upgradeType;
        }
    }
}
