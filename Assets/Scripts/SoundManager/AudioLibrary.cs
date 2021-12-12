using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    public class AudioLibrary
    {
        public Dictionary<SoundType, AudioClip> Sounds = new Dictionary<SoundType, AudioClip>();

        public static AudioLibrary Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLock)
                    {
                        _instance ??= new AudioLibrary();
                    }
                }
                return _instance;
            }
        }
        private static AudioLibrary _instance;

        private static readonly string[] SoundsDirectories = { @"Audio/Music", @"Audio/Sounds", @"Audio/UISounds" };
        private static readonly object InstanceLock = new object();

        public AudioLibrary()
        {
            Debug.Log("AudioLibrary initialize");
            foreach (var directory in SoundsDirectories)
            {
                var resources = Resources.LoadAll(directory, typeof(AudioClip));

                foreach (var res in resources)
                {
                    if (!(res is AudioClip clip)) continue;
                    if (Enum.TryParse(clip.name, true, out SoundType soundType))
                    {
                        Sounds.Add(soundType, clip);
                    }
                }
            }
            Debug.Log($"AudioLibrary initialized with {Sounds.Count} audio clips");
        }
    }
}
