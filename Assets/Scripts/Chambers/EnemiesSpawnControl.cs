using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawnControl", menuName = "ScriptableObjects/EnemiesSpawnControl")]
public class EnemiesSpawnControl : ScriptableObject
{
    [SerializeField] public EnemiesSpawnData enemiesSpawnData;
}

[System.Serializable]
public struct EnemiesSpawnData
{
    public int MinEnemiesInNormalChamber;
    public int MaxEnemiesInNormalChamber;
    public int MinEnemiesInOptionalChamber;
    public int MaxEnemiesInOptionalChamber;
    public int MinEnemiesInBossChamber;
    public int MaxEnemiesInBossChamber;
    public float BigEnemySpawnThreshold;
    public float MediumEnemySpawnThreshold;
}

public static class EnemiesData
{
    private static EnemiesSpawnData? data=null;
    public static EnemiesSpawnData GetData()
    {
        if(data==null)
        {
            EnemiesSpawnControl spawnData = Resources.Load<EnemiesSpawnControl>("ScriptableObjects/EnemiesSpawnControl");
            data = spawnData.enemiesSpawnData;
        }
        return data.Value;
    }
}
