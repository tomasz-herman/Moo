using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadUpDisplay : MonoBehaviour
{
    public ScoreBar scoreBar;
    public AmmoBar ammoBar;
    public HealthBar healthBar;
    public WeaponBar weaponBar;
    public TimerBar timerBar;
    public BossBar bossBar;
    public DamagePostProcessing damagePostProcessing;

    public void Awake()
    {
        SetHeight(healthBar, ApplicationData.UiData.HealthBarHeight);
        SetHeight(ammoBar, ApplicationData.UiData.AmmoBarHeight);
        SetHeight(bossBar, ApplicationData.UiData.BossBarHeight);
        SetHeight(timerBar, ApplicationData.UiData.TimerBarHeight);
        SetHeight(scoreBar, ApplicationData.UiData.ScoreBarHeight);
    }

    private void SetHeight(MonoBehaviour obj, float height)
    {
        var rect = obj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);
    }
}
