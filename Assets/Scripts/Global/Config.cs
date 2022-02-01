using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ConfigEntry
{
    public float musicVolume, uiVolume, soundVolume;
}

public static class Config
{
    private static ConfigEntry DefaultConfig = new ConfigEntry()
    {
        musicVolume = 1,
        uiVolume = 1,
        soundVolume = 1
    };

    public static ConfigEntry Entry { get; private set; } = DefaultConfig;
    public static bool Load(string file)
    {
        try
        {
            Entry = JsonUtility.FromJson<ConfigEntry>(File.ReadAllText(file));
        }
        catch(Exception)
        {
            return false;
        }
        return true;
    }

    public static bool Save(string file)
    {
        try
        {
            File.WriteAllText(file, JsonUtility.ToJson(Entry));
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}
