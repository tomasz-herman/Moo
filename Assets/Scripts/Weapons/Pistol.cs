using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : Weapon
    {
        private readonly Projectile projectilePrefab;
        private Color color = Color.red;

        public override float projectileSpeed { get; set; } = 1f;
        public override float triggerTimeout { get; set; } = 1f;
        public override float baseDamage { get; set; } = 1f;
        protected override int ammoConsumption => 1;

        public Pistol(Projectile projectileprefab) : base(SoundType.PistolShot)
        {
            projectilePrefab = projectileprefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.color = color;
            projectile.Launch(shooter, direction.normalized * projectileSpeed * shooting.projectileSpeed, shooting.weaponDamage * baseDamage);
        }
    }
}
