using System;
using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon : IDisposable
    {
        protected ContinuousTrigger trigger = new ContinuousTrigger();

        public float baseProjectileSpeed { get; set; }
        public float basetriggerTimeout { get; set; }
        public float baseDamage { get; set; }
        public string Name { get; set; }
        public int baseAmmoConsumption { get; set; }
        public Color color { get; set; }

        //public EventHandler<float> WeaponShoot;
        public EventHandler<(float f1, string name)> WeaponShoot;
        public SoundTypeWithPlaybackSettings Sound { get; protected set; }

        public Audio Audio { get; protected set; }

        protected AudioManager AudioManager;
        public readonly WeaponType WeaponType;

        protected Weapon(WeaponType weaponType, SoundType soundType = SoundType.NoSound)
        {
            WeaponType = weaponType;
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
                if (ammoSystem.Ammo >= baseAmmoConsumption)
                {
                    WeaponShoot?.Invoke(this, (triggerTimeout, Name));
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
    }
}
