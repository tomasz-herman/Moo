using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;
    public TMP_Text text;
    public float maxHealth = 1;
    public float health = 1;

    public void Start()
    {
        UpdateBar();
    }

    public void SetMaxHealth(float value)
    { 
        maxHealth = value;
        UpdateBar();
    }
    public void SetHealth(float value)
    { 
        health = value;
        UpdateBar();
    }

    private void UpdateBar()
    {
        fill.transform.localScale = new Vector3(1, health/maxHealth, 1);

        int displayHealth = Mathf.CeilToInt(health);
        int displayMaxHealth = Mathf.CeilToInt(maxHealth);
        text.text = $"{displayHealth}/{displayMaxHealth}";
    }
}
