using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeContainer", menuName = "ScriptableObjects/UpgradeContainer")]
public class UpgradeContainer : MonoBehaviour
{
    public List<UpgradeData> Upgrades;

    private Dictionary<UpgradeType, UpgradeData> mapping;
    public UpgradeData this[UpgradeType type]
    {
        get
        {
            if (mapping == null)
                mapping = Upgrades.ToDictionary(data => data.type);
            return mapping[type];
        }
    }
}

[Serializable]
public struct UpgradeData
{
    public UpgradeType type;
    public Sprite image;
}
