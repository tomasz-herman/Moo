using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawnControl", menuName = "ScriptableObjects/EnemiesSpawnControl")]
public class EnemiesSpawnControl : ScriptableObject
{
    [SerializeField] EnemiesSpawnData enemiesSpawnData;
    private static EnemiesSpawnControl instance = null;
    private void OnEnable()
    {
        if (instance != null)
        {
            Debug.LogError($"There can be only one object of class 'EnemiesSpawnControl', current active object name is {instance.name}");
            DestroyImmediate(this);
        }
        else
            instance = this;
    }
    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    public static EnemiesSpawnData GetData()
    {
        return instance.enemiesSpawnData;
    }
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
