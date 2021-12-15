using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : Weapon
    {
        private readonly Projectile projectilePrefab;
        private Color color = Color.red;

        protected override float projectileSpeed => 1f;
        protected override float triggerTimeout => 1f;
        protected override float baseDamage => 1f;
        protected override int ammoConsumption => 1;

        public Pistol(Projectile projectileprefab) : base(SoundType.PistolSound)
        {
            projectilePrefab = projectileprefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            if (projectilePrefab != null)
            {
                Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
                projectile.color = color;
                projectile.Launch(shooter, direction.normalized * projectileSpeed * shooting.projectileSpeed, baseDamage);
            }
        }
    }
}
