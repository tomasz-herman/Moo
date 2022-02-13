using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesContainer", menuName = "ScriptableObjects/EnemiesContainer")]
public class EnemiesContainer : ScriptableObject
{
    [SerializeField] private List<EnemyPrefabInfo> enemies;
    [HideInInspector] public Dictionary<EnemyTypes, EnemyPrefabInfo> EnemiesDict = new Dictionary<EnemyTypes, EnemyPrefabInfo>();

    private void Awake()
    {
        EnemiesDict.Clear();
        foreach (var item in enemies)
        {
            EnemiesDict.Add(item.type, item);
        }
    }
}

public enum EnemyTypes { Small, Medium, Big, MiniBoss, Boss}

[System.Serializable]
public struct EnemyPrefabInfo
{
    public GameObject enemy;
    public EnemyTypes type;
    public float teleporterScale;
}

public static class Enemys
{
    private static Dictionary<EnemyTypes, EnemyPrefabInfo> enemies = new Dictionary<EnemyTypes, EnemyPrefabInfo>();
    public static EnemyPrefabInfo GetEnemyInfoFromType(EnemyTypes type)
    {
        if (enemies.Count == 0)
        {
            EnemiesContainer Container = Resources.Load<EnemiesContainer>("ScriptableObjects/EnemiesContainer");
            enemies = Container.EnemiesDict;
        }
        return enemies[type];
    }

    public static float GetPointsForKill(this EnemyTypes type, int level)
    {
        return ApplicationData.EnemyData[type].BaseScoreForKill * ApplicationData.GameplayData.GetScoreScalingMultiplier(level);
    }

    public static bool HasBossBar(this EnemyTypes type)
    {
        return type switch
        {
            EnemyTypes.MiniBoss => true,
            EnemyTypes.Boss => true,
            _ => false
        };
    }
}