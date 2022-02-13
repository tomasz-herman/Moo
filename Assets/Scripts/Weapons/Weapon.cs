using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.SoundManager;
using Assets.Scripts.Upgrades.OneTime.Handlers;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon : IDisposable
    {
        protected ContinuousTrigger trigger = new ContinuousTrigger();

        /// <summary>
        /// Do not modify this collection
        /// </summary>
        public List<IOneTimeProjectileUpgradeHandler> projectileUpgrades { get; } = new List<IOneTimeProjectileUpgradeHandler>();

        public float baseProjectileSpeed { get; set; }
        public float basetriggerTimeout { get; set; }
        public float baseDamage { get; set; }
        public string Name { get; set; }
        public float baseAmmoConsumption { get; set; }
        public Color color { get; set; }
        public GameObject owner { get; set; }

        public EventHandler<(float timeout, WeaponType weaponType)> WeaponShoot;
        public SoundTypeWithPlaybackSettings Sound { get; protected set; }

        public Audio Audio { get; protected set; }

        protected AudioManager AudioManager;
        public readonly WeaponType WeaponType;

        protected Weapon(WeaponType weaponType, GameObject owner, SoundType soundType = SoundType.NoSound)
        {
            WeaponType = weaponType;
            this.owner = owner;
            //TODO: update this section when Weapon will derive from MonoBehaviour
            AudioManager = AudioManager.Instance;
            Sound = new SoundTypeWithPlaybackSettings
            {
                SoundType = soundType,
                PlaybackSettings = new PlaybackSettings
                {
                    SpatialBlend = 1f,
                    Volume = SoundTypeSettings.GetVolumeForSoundType(soundType)
                }
            };

            Audio = AudioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, null);

            WeaponData data = ApplicationData.WeaponData[WeaponType];
            baseAmmoConsumption = data.ammoConsumption;
            baseDamage = data.damage;
            baseProjectileSpeed = data.projectileSpeed;
            basetriggerTimeout = data.triggerTimeout;
            Name = data.name;
            color = data.color;
        }

        public void DecreaseTime()
        {
            trigger.DecreaseTime(Time.deltaTime);
        }

        public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting, AmmoSystem ammoSystem)
        {
            float triggerTimeout = shooting.GetTriggerTimeout(WeaponType);
            int dischargeCount = trigger.PullTrigger(triggerTimeout);
            for (int i = 0; i < dischargeCount; i++)
            {
                if (HasEnoughAmmo(ammoSystem))
                {
                    WeaponShoot?.Invoke(this, (triggerTimeout, WeaponType));
                    Shoot(shooter, position, direction, shooting);
                    PlayGunfireSound(position);
                    ammoSystem.Ammo -= baseAmmoConsumption;
                }
            }
        }

        public abstract void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting);

        protected virtual void PlayGunfireSound(Vector3 position)
        {
            //TODO: I don't like it, if it is possible weapon class should have player field or derive from MonoBehaviour this also is not affected by volume sliders
            Audio?.PlayClipAtPoint(position, SoundTypeSettings.GetVolumeForSoundType(Sound.SoundType));
        }

        public void Dispose()
        {
            Audio?.Dispose();
        }

        public bool HasEnoughAmmo(AmmoSystem ammoSystem)
        {
            return ammoSystem.Ammo >= baseAmmoConsumption;
        }

        public void AddUpgrade<T>(T handler) where T : IOneTimeProjectileUpgradeHandler
        {
            if (ContainsUpgrade<T>()) return;

            projectileUpgrades.Add(handler);
        }

        public bool ContainsUpgrade<T>() where T : IOneTimeProjectileUpgradeHandler
        {
            return projectileUpgrades.OfType<T>().Any();
        }

        public static string GetWeaponName(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.MachineGun:
                    return "MACHINEGUN";
                case WeaponType.Shotgun:
                    return "SHOTGUN";
                case WeaponType.Pistol:
                    return "PISTOL";
                case WeaponType.Sword:
                    return "SWORD";
                case WeaponType.GrenadeLauncher:
                    return "GRENADE LAUNCHER";
                default:
                    return "";
            }
        }
    }
}
