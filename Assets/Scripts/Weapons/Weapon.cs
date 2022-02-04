using System;
using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon : IDisposable
    {
        protected ContinuousTrigger trigger = new ContinuousTrigger();

        public abstract float projectileSpeed { get; set; }
        public abstract float triggerTimeout { get; set; }
        public abstract float baseDamage { get; set; }
        public abstract string Name { get; set; }
        protected abstract int ammoConsumption { get; }

        //public EventHandler<float> WeaponShoot;
        public EventHandler<(float timeout, WeaponType weaponType)> WeaponShoot;
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
        }

        public void DecreaseTime()
        {
            trigger.DecreaseTime(Time.deltaTime);
        }

        public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting, AmmoSystem ammoSystem)
        {
            int dischargeCount = trigger.PullTrigger(shooting.triggerTimeoutMultiplier * triggerTimeout);
            for (int i = 0; i < dischargeCount; i++)
            {
                if (ammoSystem.Ammo >= ammoConsumption)
                {
                    WeaponShoot?.Invoke(this, (triggerTimeout, WeaponType));
                    Shoot(shooter, position, direction, shooting);
                    PlayGunfireSound(position);
                    ammoSystem.Ammo -= ammoConsumption;
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
