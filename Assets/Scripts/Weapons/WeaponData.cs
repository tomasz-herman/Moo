using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct WeaponData
{
    public WeaponType type;
    public Sprite image;
    public Color color;
}

[CreateAssetMenu(fileName = "WeaponContainer", menuName = "ScriptableObjects/WeaponContainer")]
public class WeaponContainer: ScriptableObject
{
    public List<WeaponData> Weapons;

    private Dictionary<WeaponType, WeaponData> mapping;
    public WeaponData this[WeaponType type]
    {
        get
        {
            if (mapping == null)
                mapping = Weapons.ToDictionary(data => data.type);
            return mapping[type];
        }
    }
}

public enum WeaponType { MachineGun, Shotgun, Pistol, Sword, GrenadeLauncher };