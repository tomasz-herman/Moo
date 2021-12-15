using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MachineGun : Weapon
    {
        private readonly Projectile projectilePrefab;
        private Color color = Color.cyan;

        private float scatterFactor = 3f;

        protected override float projectileSpeed => 1.5f;
        protected override float triggerTimeout => 0.3f;
        protected override float baseDamage => 1f;
        protected override int ammoConsumption => 1;

        public MachineGun(Projectile projectileprefab) : base(SoundType.PistolSound)
        {
            projectilePrefab = projectileprefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            if (projectilePrefab != null)
            {
                Projectile projectile = Shooting.Instantiate(projectilePrefab, position, Quaternion.identity);
                projectile.color = color;
                var dir = Quaternion.Euler(0, Utils.RandomGaussNumber(0, scatterFactor), 0) * direction.normalized;
                projectile.Launch(shooter, dir * projectileSpeed * shooting.projectileSpeed, baseDamage);
            }
        }
    }
}
