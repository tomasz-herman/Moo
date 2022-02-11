using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UiConfig", menuName = "ScriptableObjects/UiConfig")]
public class UiConfig : ScriptableObject
{
    public float HealthBarHeight = 80f;
    public float AmmoBarHeight = 80f;
    public float BossBarHeight = 100f;
    public float TimerBarHeight = 50f;
    public float ScoreBarHeight = 50f;
}
