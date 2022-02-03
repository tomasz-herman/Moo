using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public List<EnemyData> Data = new List<EnemyData>();

    private Dictionary<EnemyTypes, EnemyData> mapping;
    public EnemyData this[EnemyTypes type]
    {
        get
        {
            if (mapping == null)
                mapping = Data.ToDictionary(data => data.Type);
            return mapping[type];
        }
    }
}

[Serializable]
public class EnemyData
{
    public EnemyTypes Type;
    public float BaseHealth = 100;
    public float BaseDamageMultiplier = 1;
    public float BaseProjectileSpeedMultiplier = 1;
    public float BaseMovementSpeed = 1;
}
