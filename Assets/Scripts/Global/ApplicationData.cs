using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationData
{
    public static readonly string leaderboardPath = "Playerdata/leaderboard.txt";
    public static readonly string configPath = "Playerdata/config.json";

    public static WeaponContainer WeaponData = Resources.Load<WeaponContainer>("ScriptableObjects/WeaponData");
}
