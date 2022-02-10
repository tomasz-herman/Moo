using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GrenadeLauncher : Weapon
    {
        private readonly Grenade grenadePrefab;

        public GrenadeLauncher(Grenade grenadeprefab) : base(WeaponType.GrenadeLauncher, SoundType.GrenadeLauncherShot)
        {
            grenadePrefab = grenadeprefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Grenade grenade = Shooting.Instantiate(grenadePrefab, position, Quaternion.identity);
            grenade.color = color;
            grenade.Launch(shooter, direction.normalized * baseProjectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * baseDamage);
        }
    }
}
