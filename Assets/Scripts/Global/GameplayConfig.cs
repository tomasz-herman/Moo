using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameplayConfig", menuName = "ScriptableObjects/GameplayConfig")]
public class GameplayConfig : ScriptableObject
{
    public float DefaultPlayerHealth = 100;
    public int DefaultPlayerAmmo = 100;
    public float scalingFactor = 1.5f;
    public float healthScalingMultiplier = 1f;
    public float damageScalingMultiplier = 1f;
    public float movementSpeedScalingMultiplier = 0.5f;
    public float projectileSpeedScalingMultiplier = 0.8f;
    public float scoreScalingMultiplier = 1f;
    public float triggerTimeoutScalingMultiplier = 0.7f;
    public float upgradeScalingMultiplier = 0.9f;

    private float GetScalingFactor(int level, float multiplier) { return (float) Math.Pow(scalingFactor * multiplier, level - 1); }
    public float GetHealthScalingMultiplier(int level) { return GetScalingFactor(level, healthScalingMultiplier); }
    public float GetDamageScalingMultiplier(int level) { return GetScalingFactor(level, damageScalingMultiplier); }
    public float GetMovementSpeedScalingMultiplier(int level) { return GetScalingFactor(level, movementSpeedScalingMultiplier); }
    public float GetProjectileSpeedScalingMultiplier(int level) { return GetScalingFactor(level, projectileSpeedScalingMultiplier); }
    public float GetScoreScalingMultiplier(int level) { return GetScalingFactor(level, scoreScalingMultiplier); }
    public float GetTriggerTimeoutScalingMultiplier(int level) { return GetScalingFactor(level, triggerTimeoutScalingMultiplier); }
    public float GetUpgradeScalingMultiplier (int level) { return GetScalingFactor(level, upgradeScalingMultiplier); }
}
