using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MachineGun : Weapon
    {
        public Projectile projectilePrefab { get; protected set; }

        private readonly float _scatterFactor = 3f;

        public MachineGun(Projectile projectilePrefab) : base(WeaponType.MachineGun, SoundType.PistolShot)
        {
            this.projectilePrefab = projectilePrefab;
        }

        public MachineGun(GameObject owner, Projectile projectilePrefab) : base(WeaponType.MachineGun, owner, SoundType.PistolShot)
        {
            this.projectilePrefab = projectilePrefab;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Projectile projectile = Object.Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.color = color;
            projectile.SetUpgrades(projectileUpgrades);
            var dir = Quaternion.Euler(0, Utils.RandomGaussNumber(0, _scatterFactor), 0) * direction.normalized;
            projectile.Launch(shooter, dir * ProjectileSpeed * shooting.projectileSpeedMultiplier, shooting.weaponDamageMultiplier * Damage);
        }
    }
}
