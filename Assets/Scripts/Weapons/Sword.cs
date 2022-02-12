using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Sword : Weapon
    {
        private readonly Blade bladePrefab;

        public Sword(Blade bladePrefab): base(WeaponType.Sword, SoundType.SwordSwing)
        {
            this.bladePrefab = bladePrefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Blade blade = Shooting.Instantiate(bladePrefab, position, Quaternion.identity);
            blade.color = color;
            blade.Launch(shooter, shooting.weaponDamageMultiplier * baseDamage, baseProjectileSpeed * shooting.projectileSpeedMultiplier);
        }
    }
}
