using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeContainer", menuName = "ScriptableObjects/UpgradeContainer")]
public class UpgradeContainer : ScriptableObject
{
    public Color OneTimeUpgradeColor;
    public List<UpgradeIconData> Upgrades;

    private Dictionary<UpgradeIcon, UpgradeIconData> mapping;
    public UpgradeIconData this[UpgradeIcon type]
    {
        get
        {
            if (mapping == null)
                mapping = Upgrades.ToDictionary(data => data.iconType);
            return mapping[type];
        }
    }
}

[Serializable]
public struct UpgradeIconData
{
    public UpgradeIcon iconType;
    public Sprite image;
}
