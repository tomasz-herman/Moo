using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;
    public TMP_Text text;
    public HealthSystem healthSystem;

    public void Start()
    {
        healthSystem.HealthChanged += (sender, args) => UpdateBar();
        UpdateBar();
    }

    private void UpdateBar()
    {
        float health = healthSystem.Health;
        float maxHealth = healthSystem.MaxHealth;
        fill.transform.localScale = new Vector3(health / maxHealth, 1, 1);

        int displayHealth = Mathf.CeilToInt(health);
        int displayMaxHealth = Mathf.CeilToInt(maxHealth);
        text.text = $"{displayHealth}/{displayMaxHealth}";
    }
}
