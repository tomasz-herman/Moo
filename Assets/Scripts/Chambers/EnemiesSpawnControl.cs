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
    public int EnemiesInNormalChamber;
    public int EnemiesInOptionalChamber;
    public int EnemiesInBossChamber;
    public float BigEnemyStartFrequency;
    public float BigEnemyEndFrequency;
    public float MediumEnemyStartFrequency;
    public float MediumEnemyEndFrequency;

    public IEnumerable<EnemyTypes> GetEnemiesForChamber(ChamberNode node)
    {
        var type = node.Type;
        var progress = node.MainProgress;

        int totalEnemies = type switch
        {
            ChamberType.Normal => EnemiesInNormalChamber,
            ChamberType.Optional => EnemiesInOptionalChamber,
            ChamberType.Boss => EnemiesInBossChamber,
            _ => 0
        };

        var bosses = type switch
        {
            ChamberType.Optional => Enumerable.Repeat(EnemyTypes.MiniBoss, 1),
            ChamberType.Boss => Enumerable.Repeat(EnemyTypes.Boss, 1),
            _ => Enumerable.Empty<EnemyTypes>()
        };

        float bigFrequency = BigEnemyStartFrequency + (BigEnemyEndFrequency - BigEnemyStartFrequency) * progress;
        int bigEnemyCount = Mathf.FloorToInt(totalEnemies * bigFrequency);

        float mediumFrequency = MediumEnemyStartFrequency + (MediumEnemyEndFrequency - MediumEnemyStartFrequency) * progress;
        int mediumEnemyCount = Mathf.FloorToInt(totalEnemies * mediumFrequency);

        int smallEnemyCount = totalEnemies - bigEnemyCount - mediumEnemyCount;

        return Enumerable.Repeat(EnemyTypes.Small, smallEnemyCount)
            .Concat(Enumerable.Repeat(EnemyTypes.Medium, mediumEnemyCount))
            .Concat(Enumerable.Repeat(EnemyTypes.Big, bigEnemyCount))
            .Concat(bosses);
    }

    public float GetExpectedEnemyScoreForChamber(ChamberNode node)
    {
        float score = 0;
        foreach(var type in GetEnemiesForChamber(node))
        {
            score += type.GetPointsForKill(node.Level);
        }

        return score;
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
