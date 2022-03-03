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
        public float projectileSpeedMultiplier { get; set; }
        public float ProjectileSpeed { get { return GetProjectileSpeed(projectileSpeedMultiplier); } }

        public float basetriggerTimeout { get; set; }
        public float triggerTimeoutMultiplier { get; set; }
        public float TriggerTimeout { get { return GetTriggerTimeout(triggerTimeoutMultiplier); } }

        public float baseDamage { get; set; }
        public float damageMultiplier { get; set; }
        public float Damage { get { return GetDamage(damageMultiplier); } }

        public string Name { get; set; }

        public float baseAmmoConsumption { get; set; }
        public float ammoConsumptionMultiplier { get; set; }
        public float AmmoConsumption { get { return GetAmmoConsumption(ammoConsumptionMultiplier); } }

        public Color color { get; set; }

        private GameObject _owner;
        public GameObject Owner
        {
            get => this._owner;
            set
            {
                _owner = value;
                Audio = AudioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, _owner.transform);
            }
        }

        public EventHandler<(float timeout, WeaponType weaponType)> WeaponShoot;
        public SoundTypeWithPlaybackSettings Sound { get; protected set; }

        public Audio Audio { get; protected set; }

        protected AudioManager AudioManager;
        public readonly WeaponType WeaponType;

        protected Weapon(WeaponType weaponType, SoundType soundType = SoundType.NoSound)
        {
            WeaponType = weaponType;

            WeaponData data = ApplicationData.WeaponData[WeaponType];
            baseAmmoConsumption = data.ammoConsumption;
            ammoConsumptionMultiplier = 1;
            triggerTimeoutMultiplier = 1;
            damageMultiplier = 1;
            projectileSpeedMultiplier = 1;
            baseDamage = data.damage;
            baseProjectileSpeed = data.projectileSpeed;
            basetriggerTimeout = data.triggerTimeout;
            Name = data.name;
            color = data.color;

            AudioManager = AudioManager.Instance;
            Sound = new SoundTypeWithPlaybackSettings
            {
                SoundType = soundType,
                PlaybackSettings = new PlaybackSettings
                {
                    //TODO: maybe set spatial blend to 0f if player is owner
                    SpatialBlend = 1f,
                    Volume = SoundHelpers.GetVolumeForSoundType(soundType)
                }
            };
        }

        protected Weapon(WeaponType weaponType, GameObject owner, SoundType soundType = SoundType.NoSound) : this(weaponType, soundType)
        {
            Owner = owner;
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
                    PlayGunfireSound();
                    ammoSystem.Ammo -= AmmoConsumption;
                }
            }
        }

        public abstract void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting);

        protected virtual void PlayGunfireSound()
        {
            Audio?.PlayOneShot();
        }

        public void Dispose()
        {
            Audio?.Dispose();
        }

        public bool HasEnoughAmmo(AmmoSystem ammoSystem)
        {
            return ammoSystem.Ammo >= AmmoConsumption;
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
            return ApplicationData.WeaponData[type].name;
        }

        public float GetAmmoConsumption(float multiplier) { return baseAmmoConsumption * multiplier; }
        public float GetTriggerTimeout(float multiplier) { return basetriggerTimeout * multiplier; }
        public float GetDamage(float multiplier) { return baseDamage * multiplier; }
        public float GetProjectileSpeed(float multiplier) { return baseProjectileSpeed * multiplier; }
    }
}
