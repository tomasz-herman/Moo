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
    public float ammoScalingMultiplier = 0.6f;

    //https://github.com/tomasz-herman/Moo/projects/6 see third column
    private float GetScalingFactor(int level, float multiplier) { return (float) Math.Pow(1 + (1 - scalingFactor * multiplier), level - 1); }
    public float GetHealthScalingMultiplier(int level) { return GetScalingFactor(level, healthScalingMultiplier); }
    public float GetDamageScalingMultiplier(int level) { return GetScalingFactor(level, damageScalingMultiplier); }
    public float GetMovementSpeedScalingMultiplier(int level) { return GetScalingFactor(level, movementSpeedScalingMultiplier); }
    public float GetProjectileSpeedScalingMultiplier(int level) { return GetScalingFactor(level, projectileSpeedScalingMultiplier); }
    public float GetScoreScalingMultiplier(int level) { return GetScalingFactor(level, scoreScalingMultiplier); }
    public float GetTriggerTimeoutScalingMultiplier(int level) { return GetScalingFactor(level, triggerTimeoutScalingMultiplier); }
    public float GetUpgradeScalingMultiplier (int level) { return GetScalingFactor(level, upgradeScalingMultiplier); }
    public float GetAmmoScalingMultiplier (int level) { return GetScalingFactor(level, ammoScalingMultiplier); }
}
