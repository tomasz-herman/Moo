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

    //https://github.com/tomasz-herman/Moo/projects/6 see third column
    private float GetScalingFactor(int level, float multiplier) { return (float) Math.Pow(1 + ((ScalingFactor - 1) * multiplier), level - 1); }
    public float GetHealthScalingMultiplier(int level) { return GetScalingFactor(level, HealthScalingMultiplier); }
    public float GetDamageScalingMultiplier(int level) { return GetScalingFactor(level, DamageScalingMultiplier); }
    public float GetMovementSpeedScalingMultiplier(int level) { return GetScalingFactor(level, MovementSpeedScalingMultiplier); }
    public float GetProjectileSpeedScalingMultiplier(int level) { return GetScalingFactor(level, ProjectileSpeedScalingMultiplier); }
    public float GetScoreScalingMultiplier(int level) { return GetScalingFactor(level, ScoreScalingMultiplier); }
    public float GetTriggerTimeoutScalingMultiplier(int level) { return GetScalingFactor(level, TriggerTimeoutScalingMultiplier); }
    public float GetUpgradeScalingMultiplier (int level) { return GetScalingFactor(level, UpgradeScalingMultiplier); }
    public float GetAmmoScalingMultiplier (int level) { return GetScalingFactor(level, AmmoScalingMultiplier); }
    public float GetChamberClearTimeScalingMultiplier (int level) { return GetScalingFactor(level, ChamberClearTimeScalingMultiplier); }
}
