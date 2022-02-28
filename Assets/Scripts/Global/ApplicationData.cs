using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

public static class ApplicationData
{
    public static readonly string leaderboardPath = "Playerdata/leaderboard.txt";
    public static readonly string configPath = "Playerdata/config.json";

    public static WeaponContainer WeaponData = Resources.Load<WeaponContainer>("ScriptableObjects/WeaponData");
    public static WeaponAIConfig WeaponAIData = Resources.Load<WeaponAIConfig>("ScriptableObjects/WeaponAIData");
    public static GameplayConfig GameplayData = Resources.Load<GameplayConfig>("ScriptableObjects/GameplayData");
    public static EnemyConfig EnemyData = Resources.Load<EnemyConfig>("ScriptableObjects/EnemyData");
    public static EnemiesSpawnControl SpawnData = Resources.Load<EnemiesSpawnControl>("ScriptableObjects/EnemiesSpawnControl");
    public static UiConfig UiData = Resources.Load<UiConfig>("ScriptableObjects/UiConfig");
    public static UpgradeContainer UpgradeData = Resources.Load<UpgradeContainer>("ScriptableObjects/UpgradeData");


    public static readonly UnityEvent<bool> DebugChanged = new UnityEvent<bool>();
    private static bool debug;
    public static bool Debug
    {
        get { return debug; }
        set { debug = value; DebugChanged.Invoke(debug); }
    }
}
