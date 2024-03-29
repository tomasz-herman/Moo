using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameplayConfig", menuName = "ScriptableObjects/GameplayConfig")]
public class GameplayConfig : ScriptableObject
{
    public bool Debug = false;
    public float DefaultPlayerHealth = 100;
    public int DefaultPlayerAmmo = 100;

    public float ScalingFactor = 1.5f;
    public float PlayerHealthScalingMultiplier = 1f;
    public float EnemyHealthScalingMultiplier = 1f;
    public float PlayerDamageScalingMultiplier = 1f;
    public float EnemyDamageScalingMultiplier = 1f;
    public float PlayerMovementSpeedScalingMultiplier = 1f;
    public float EnemyMovementSpeedScalingMultiplier = 0.25f;
    public float PlayerProjectileSpeedScalingMultiplier = 0.75f;
    public float EnemyProjectileSpeedScalingMultiplier = 0.25f;
    public float ScoreScalingMultiplier = 1f;
    public float PlayerTriggerTimeoutScalingMultiplier = 1f;
    public float EnemyTriggerTimeoutScalingMultiplier = 0.25f;
    public float UpgradeScalingMultiplier = 1f;
    public float PlayerAmmoScalingMultiplier = 1f;
    public float EnemyAmmoScalingMultiplier = 1f;

    /// <summary>
    /// Each upgrade multiplier shall scale with ScalingFactor and UpgradeScalingMultiplier, but because there are many of them before each boss,
    /// the multiplier is divided by total number of upgrades for each boss (including optional chambers)
    /// Because such mechanic makes upgrades worthless (but scales well for arbitrary optional chamber count), we introduce correction factor
    /// This number should be considered as "how many upgrades do I need to pick up to fully scale my character to be equivalent to next-level enemies
    /// 
    /// Consider this: Boss's weapon damage depends on their level, e.g. 1 level higher equals 5 weapons upgraded, which equals to 5 upgrades collected
    /// therefore upgrade correction factor for weapons themselves equals to 5, but damage is not the only upgradable statistic
    /// 
    /// This value is not meant to be changed arbitrarily like the other multipliers, but rather tweaked in order to balance the fact
    /// that there are multiple upgrades, therefore available types of upgrades are the only parameter in determining this factor
    /// 
    /// Correction factor shall equal to N if there are N different types of upgrades that all need to be upgraded once to match enemy's 1-level increase in scaling
    /// Note that UpgradeScalingMultiplier &lt; 1 makes sure that the game gets progressively harder on purpose and upgrade correction factor shall not correct for this
    /// When determining this factor, assume the upgrade scaling multiplier is set to 1
    /// 
    /// If the upgrades shall ever feel "too weak", this is the value to change
    /// </summary>
    public float UpgradeCorrectionFactor = 10f;

    //Expected spent time in seconds from entry to exit for a chamber type in level 1 chambers
    public float NormalChamberClearTime = 20f;
    public float OptionalChamberClearTime = 40f;
    public float BossChamberClearTime = 60f;
    public float ChamberClearTimeScalingMultiplier = 0.5f;
    public float QuickWinScoreFactor = 2.5f;

    public int NumberOfChambersBeforeBoss = 2;
    public int NumberOfOptionalChambersBeforeBoss = 5;
    public int NumberOfBossChambers = 3;

    public float GetAscendingScalingFactor(int level, float multiplier, float secondaryMultiplier = 1)
    {
        float a, b, c, x;
        a = b = (ScalingFactor - 1) * multiplier / 2;
        c = 1;
        x = level - 1;
        float quadratic = (a * x + b) * x + c;
        return 1 + (quadratic - 1) * secondaryMultiplier;
    }
    public float GetDescendingScalingFactor(int level, float multiplier, float secondaryMultiplier = 1) { return 1 / GetAscendingScalingFactor(level, multiplier, secondaryMultiplier); }
    public float GetEnemyHealthScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, EnemyHealthScalingMultiplier, secondaryMultiplier); }
    public float GetPlayerHealthScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, PlayerHealthScalingMultiplier, secondaryMultiplier); }
    public float GetEnemyDamageScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, EnemyDamageScalingMultiplier, secondaryMultiplier); }
    public float GetPlayerDamageScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, PlayerDamageScalingMultiplier, secondaryMultiplier); }
    public float GetEnemyMovementSpeedScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, EnemyMovementSpeedScalingMultiplier, secondaryMultiplier); }
    public float GetPlayerMovementSpeedScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, PlayerMovementSpeedScalingMultiplier, secondaryMultiplier); }
    public float GetEnemyProjectileSpeedScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, EnemyProjectileSpeedScalingMultiplier, secondaryMultiplier); }
    public float GetPlayerProjectileSpeedScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, PlayerProjectileSpeedScalingMultiplier, secondaryMultiplier); }
    public float GetScoreScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, ScoreScalingMultiplier, secondaryMultiplier); }
    public float GetEnemyTriggerTimeoutScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetDescendingScalingFactor(level, EnemyTriggerTimeoutScalingMultiplier, secondaryMultiplier); }
    public float GetPlayerTriggerTimeoutScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetDescendingScalingFactor(level, PlayerTriggerTimeoutScalingMultiplier, secondaryMultiplier); }
    public float GetEnemyAmmoScalingMultiplier (int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, EnemyAmmoScalingMultiplier, secondaryMultiplier); }
    public float GetPlayerAmmoScalingMultiplier (int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, PlayerAmmoScalingMultiplier, secondaryMultiplier); }
    public float GetChamberClearTimeScalingMultiplier (int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, ChamberClearTimeScalingMultiplier, secondaryMultiplier); }
    public float GetSecondaryUpgradeMultiplier()
    {
        //TODO: fix this if the game shall ever go into real production
        //in order to do this properly we'd technically need to add Minibosses/Bosses per chamber type variables
        //and count expected number of upgrades dropped by non-boss enemies, but since we don't have such architecture this shall do
        var bossData = ApplicationData.EnemyData[EnemyTypes.Boss];
        var minibossData = ApplicationData.EnemyData[EnemyTypes.MiniBoss];

        float miniBossUpgradesPerBoss = NumberOfOptionalChambersBeforeBoss * minibossData.UpgradeDropChance * minibossData.UpgradeDropCount;
        float bossUpgradesPerBoss = bossData.UpgradeDropChance * bossData.UpgradeDropCount;

        return UpgradeScalingMultiplier * UpgradeCorrectionFactor / (miniBossUpgradesPerBoss + bossUpgradesPerBoss);
    }
}
