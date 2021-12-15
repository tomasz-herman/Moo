using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class GrenadeLauncher : Weapon
    {
        private readonly Grenade grenadePrefab;
        private Color color = Color.magenta;

        protected override float projectileSpeed => 2f;
        protected override float triggerTimeout => 7f;
        protected override float baseDamage => 10f;
        protected override int ammoConsumption => 7;

        public GrenadeLauncher(Grenade grenadeprefab) : base(SoundType.GrenadeLauncherShoot)
        {
            grenadePrefab = grenadeprefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            if (grenadePrefab != null)
            {
                Grenade grenade = Shooting.Instantiate(grenadePrefab, position, Quaternion.identity);
                grenade.color = color;
                grenade.Launch(shooter, direction.normalized * projectileSpeed * shooting.projectileSpeed, baseDamage);
            }
        }
    }
}
