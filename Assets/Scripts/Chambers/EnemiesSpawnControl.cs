using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float BigEnemyStartFrequency;
    public float BigEnemyEndFrequency;
    public float MediumEnemyStartFrequency;
    public float MediumEnemyEndFrequency;

    public IEnumerable<EnemyTypes> GetRandomEnemiesForChamber(ChamberType type, float progress)
    {
        int min = 0, max = 0;
        switch(type)
        {
            case ChamberType.Normal:
                min = MinEnemiesInNormalChamber;
                max = MaxEnemiesInNormalChamber;
                break;
            case ChamberType.Optional:
                min = MinEnemiesInOptionalChamber;
                max = MaxEnemiesInOptionalChamber;
                break;
            case ChamberType.Boss:
                min = MinEnemiesInBossChamber;
                max = MaxEnemiesInBossChamber;
                break;
        }

        //big and normal enemies are stronger, so we want a constant number of them for each progress
        float bigFrequency = BigEnemyStartFrequency + (BigEnemyEndFrequency - BigEnemyStartFrequency) * progress;
        int bigEnemyCount = Mathf.FloorToInt(min * bigFrequency);

        float mediumFrequency = MediumEnemyStartFrequency + (MediumEnemyEndFrequency - MediumEnemyStartFrequency) * progress;
        int mediumEnemyCount = Mathf.FloorToInt(min * mediumFrequency);

        int smallEnemyCount = Utils.NumberBetween(min, max) - mediumEnemyCount - bigEnemyCount;

        return Enumerable.Repeat(EnemyTypes.Small, smallEnemyCount)
            .Concat(Enumerable.Repeat(EnemyTypes.Medium, mediumEnemyCount))
            .Concat(Enumerable.Repeat(EnemyTypes.Big, bigEnemyCount));
    }
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
