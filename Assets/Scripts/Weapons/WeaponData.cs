using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct WeaponType
{
    public string name;
    public Texture2D image;
    public Color color;
}

[CreateAssetMenu(fileName = "WeaponContainer", menuName = "ScriptableObjects/WeaponContainer")]
public class WeaponContainer: ScriptableObject
{
    public List<WeaponType> Weapons;
}