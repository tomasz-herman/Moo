using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemysContainer", menuName = "ScriptableObjects/EnemysContainer")]
public class EnemysContainer : ScriptableObject
{
    [SerializeField] private List<EnemyPrefabInfo> enemys;
    [HideInInspector] public Dictionary<EnemyTypes, EnemyPrefabInfo> EnemysDict = new Dictionary<EnemyTypes, EnemyPrefabInfo>();

    private void OnValidate()
    {
        EnemysDict.Clear();
        foreach (var item in enemys)
        {
            EnemysDict.Add(item.type, item);
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
    private static Dictionary<EnemyTypes, EnemyPrefabInfo> enemys = new Dictionary<EnemyTypes, EnemyPrefabInfo>();
    public static EnemyPrefabInfo GetEnemyInfoFromType(EnemyTypes type)
    {
        if (enemys.Count == 0)
        {
            EnemysContainer Container = Resources.Load<EnemysContainer>("ScriptableObjects/EnemysContainer");
            enemys = Container.EnemysDict;
        }
        return enemys[type];
    }
}