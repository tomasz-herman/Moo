using Assets.Scripts.SoundManager;
using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    [HideInInspector]
    public SoundType SoundType;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1f;

    [Range(.1f, 3f)]
    public float Pitch = 1f;

    public bool Loop;

    /// <summary>
    /// Sets how much this AudioSource is affected by 3D spatialisation calculations (attenuation, doppler etc). 0.0 makes the sound full 2D, 1.0 makes it full 3D.
    /// </summary>
    [Range(0f, 1f)]
    public float SpatialBlend = 0f;

    [HideInInspector]
    public AudioSource Source;

    public void InitializeSound(GameObject gameObject, SoundType soundType)
    {
        SoundType = soundType;
        Source = gameObject.AddComponent<AudioSource>();
        Source.clip = Clip;
        Source.volume = Volume;
        Source.pitch = Pitch;
        Source.loop = Loop;
        Source.spatialBlend = SpatialBlend;
    }
}
