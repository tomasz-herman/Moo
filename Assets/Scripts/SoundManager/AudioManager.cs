using Assets.Scripts.SoundManager;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public TypedSound[] SoundsArray;

    [HideInInspector]
    public Dictionary<SoundType, Sound> Sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Sounds = new Dictionary<SoundType, Sound>();

        foreach (var s in SoundsArray)
        {
            if (!Sounds.ContainsKey(s.SoundType))
            {
                Sounds.Add(s.SoundType, s.Sound);
                s.Sound.InitializeSound(gameObject, s.SoundType);
                continue;
            }
            Debug.LogWarning($"There already exists sound of type {s.SoundType}");
        }
    }

    private void Start()
    {
        Play(SoundType.BackgroundTheme);
    }

    public void Play(SoundType soundType)
    {
        if (Sounds.TryGetValue(soundType, out Sound sound))
        {
            sound.Source.Play();
            return;
        }

        Debug.LogWarning($"Sound type {soundType} is unknown");
    }

    public void Pause(SoundType soundType)
    {
        if (Sounds.TryGetValue(soundType, out Sound sound))
        {
            sound.Source.Pause();
            return;
        }

        Debug.LogWarning($"Sound type {soundType} is unknown");
    }

    public void Stop(SoundType soundType)
    {
        if (Sounds.TryGetValue(soundType, out Sound sound))
        {
            sound.Source.Stop();
            return;
        }

        Debug.LogWarning($"Sound type {soundType} is unknown");
    }
}
