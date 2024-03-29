﻿using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : Weapon
    {
        public Projectile projectilePrefab { get; protected set; }

        public Pistol(Projectile projectilePrefab) : base(WeaponType.Pistol, SoundType.PistolShot)
        {
            this.projectilePrefab = projectilePrefab;
            Sound.PlaybackSettings.Pitch = 0.8f;
        }

        public Pistol(GameObject owner, Projectile projectilePrefab) : base(WeaponType.Pistol, owner, SoundType.PistolShot)
        {
            this.projectilePrefab = projectilePrefab;
            Sound.PlaybackSettings.Pitch = 0.8f;
        }

        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Projectile projectile = Object.Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.color = color;
            projectile.SetUpgrades(projectileUpgrades);
            projectile.Launch(shooter, direction.normalized * ProjectileSpeed, Damage);
        }
    }
}
