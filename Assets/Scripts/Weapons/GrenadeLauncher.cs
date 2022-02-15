using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GrenadeLauncher : Weapon
    {
        public Grenade grenadePrefab { get; protected set; }

        public GrenadeLauncher(Grenade grenadePrefab) : base(WeaponType.GrenadeLauncher, SoundType.GrenadeLauncherShot)
        {
            this.grenadePrefab = grenadePrefab;
        }

        public GrenadeLauncher(GameObject owner, Grenade grenadePrefab) : base(WeaponType.GrenadeLauncher, owner, SoundType.GrenadeLauncherShot)
        {
            this.grenadePrefab = grenadePrefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Grenade grenade = Object.Instantiate(grenadePrefab, position, Quaternion.identity);
            grenade.color = color;
            grenade.SetUpgrades(projectileUpgrades);
            grenade.Launch(shooter, direction.normalized * ProjectileSpeed, Damage);
        }
    }
}
