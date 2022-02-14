using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameplayConfig", menuName = "ScriptableObjects/GameplayConfig")]
public class GameplayConfig : ScriptableObject
{
    public float DefaultPlayerHealth = 100;
    public int DefaultPlayerAmmo = 100;

    public float ScalingFactor = 1.5f;
    public float HealthScalingMultiplier = 1f;
    public float DamageScalingMultiplier = 1f;
    public float MovementSpeedScalingMultiplier = 0.2f;
    public float ProjectileSpeedScalingMultiplier = 0.8f;
    public float ScoreScalingMultiplier = 1f;
    public float TriggerTimeoutScalingMultiplier = 0.7f;
    public float UpgradeScalingMultiplier = 0.9f;
    public float AmmoScalingMultiplier = 0.6f;

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
    public float GetHealthScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, HealthScalingMultiplier, secondaryMultiplier); }
    public float GetDamageScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, DamageScalingMultiplier, secondaryMultiplier); }
    public float GetMovementSpeedScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, MovementSpeedScalingMultiplier, secondaryMultiplier); }
    public float GetProjectileSpeedScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, ProjectileSpeedScalingMultiplier, secondaryMultiplier); }
    public float GetScoreScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, ScoreScalingMultiplier, secondaryMultiplier); }
    public float GetTriggerTimeoutScalingMultiplier(int level, float secondaryMultiplier = 1) { return GetDescendingScalingFactor(level, TriggerTimeoutScalingMultiplier, secondaryMultiplier); }
    public float GetAmmoScalingMultiplier (int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, AmmoScalingMultiplier, secondaryMultiplier); }
    public float GetChamberClearTimeScalingMultiplier (int level, float secondaryMultiplier = 1) { return GetAscendingScalingFactor(level, ChamberClearTimeScalingMultiplier, secondaryMultiplier); }
}
