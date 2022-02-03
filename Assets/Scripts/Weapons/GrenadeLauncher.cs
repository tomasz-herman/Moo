using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GrenadeLauncher : Weapon
    {
        private readonly Grenade grenadePrefab;
        private Color color = Color.magenta;

        public override float projectileSpeed { get; set; } = 20f;
        public override float triggerTimeout { get; set; } = 7f;
        public override float baseDamage { get; set; } = 10f;
        protected override int ammoConsumption => 7;

        public GrenadeLauncher(Grenade grenadeprefab) : base(WeaponType.GrenadeLauncher, SoundType.GrenadeLauncherShot)
        {
            grenadePrefab = grenadeprefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Grenade grenade = Shooting.Instantiate(grenadePrefab, position, Quaternion.identity);
            grenade.color = color;
            grenade.Launch(shooter, direction.normalized * projectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * baseDamage);
        }
    }
}
