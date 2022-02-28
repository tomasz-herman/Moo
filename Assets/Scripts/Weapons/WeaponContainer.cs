using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponContainer", menuName = "ScriptableObjects/WeaponContainer")]
public class WeaponContainer : ScriptableObject
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

[Serializable]
public struct WeaponData
{
    public WeaponType type;
    public string name;
    public Sprite image;
    public Color color;
    public float projectileSpeed;
    public float triggerTimeout;
    public float damage;
    public float ammoConsumption;
}

public enum WeaponType { MachineGun, Shotgun, Pistol, Sword, GrenadeLauncher };