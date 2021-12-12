using System;
using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    [Serializable]
    public class PlaybackSettings
    {
        /// <summary>
        /// The volume of the audio. Default = 1.
        /// </summary>
        [Range(0f, 1f)]
        public float Volume = 1f;

        /// <summary>
        /// Whether the audio will be lopped. Default = false.
        /// </summary>
        public bool Loop = false;

        /// <summary>
        /// The pitch of the audio. Default = 1.
        /// </summary>
        [Range(-3f, 3f)]
        public float Pitch = 1f;

        /// <summary>
        /// Sets the priority of the audio. Priority: 0 = most important, 256 = least important. Default = 128.
        /// </summary>
        [Range(0, 256)]
        public int Priority = 128;

        /// <summary>
        /// Sets how much this AudioSource is affected by 3D spatialisation calculations (attenuation, doppler etc). 0.0 makes the sound full 2D, 1.0 makes it full 3D. Default = 0.
        /// </summary>
        [Range(0f, 1f)]
        public float SpatialBlend = 0f;

        /// <summary>
        /// Pans a playing sound in a stereo way (left or right). This only applies to sounds that are Mono or Stereo. Default = 0.
        /// </summary>
        [Range(-1f, 1f)]
        public float StereoPan = 0f;

        /// <summary>
        /// The doppler scale of the audio. Default = 0.
        /// </summary>
        [Range(0f, 5f)]
        public float DopplerLevel = 0f;

        /// <summary>
        /// The spread angle (in degrees) of a 3d stereo or multichannel sound in speaker space. Default = 0;
        /// </summary>
        [Range(0f, 360f)]
        public float Spread = 0f;

        /// <summary>
        /// How the audio attenuates over distance. Default = Logarithmic.
        /// </summary>
        public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;

        /// <summary>
        /// Within the Min distance the audio will cease to grow louder in volume. Default = 1f;
        /// </summary>
        [Range(0f, 1000f)]
        public float Min3DDistance = 1f;

        /// <summary>
        /// (Logarithmic rolloff) MaxDistance is the distance a sound stops attenuating at. Default = 500f.
        /// </summary>
        [Range(0.01f, 1000f)]
        public float Max3DDistance = 500f;

        /// <summary>
        /// How many seconds it needs for the audio to fade in/ reach target volume (if higher than current). Default = 0.
        /// </summary>
        [Range(0f, 100f)]
        public float FadeInSeconds = 0f;

        /// <summary>
        /// How many seconds it needs for the audio to fade out/ reach target volume (if lower than current). Default = 0.
        /// </summary>
        [Range(0f, 100f)]
        public float FadeOutSeconds = 0f;

        /// <summary>
        /// Ignore all settings and use default ones
        /// </summary>
        public bool UseDefault = false;

        public PlaybackSettings()
        {
            SetDefaultSettings();
        }

        public void ApplyToAudioSource(AudioSource audioSource)
        {
            if (audioSource == null)
                return;

            if (UseDefault)
                SetDefaultSettings();

            audioSource.volume = Mathf.Clamp(Volume, 0f, 1f);
            audioSource.loop = Loop;
            audioSource.pitch = Mathf.Clamp(Pitch, -3f, 3f);
            audioSource.priority = Mathf.Clamp(Priority, 0, 256);
            audioSource.spatialBlend = Mathf.Clamp(SpatialBlend, 0f, 1f);
            audioSource.panStereo = Mathf.Clamp(StereoPan, -1f, 1f);
            audioSource.dopplerLevel = Mathf.Clamp(DopplerLevel, 0f, 5f);
            audioSource.spread = Mathf.Clamp(Spread, 0f, 360f);
            audioSource.rolloffMode = RolloffMode;
            audioSource.minDistance = Mathf.Max(Min3DDistance, 0);
            audioSource.maxDistance = Mathf.Max(Max3DDistance, 0.01f);
        }

        private void SetDefaultSettings()
        {
            Volume = 1f;
            Loop = false;
            Pitch = 1f;
            Priority = 128;
            SpatialBlend = 0f;
            StereoPan = 0f;
            DopplerLevel = 0f;
            Spread = 0f;
            RolloffMode = AudioRolloffMode.Logarithmic;
            Min3DDistance = 1f;
            Max3DDistance = 500f;
            FadeInSeconds = 0f;
            FadeOutSeconds = 0f;
        }
    }
}
