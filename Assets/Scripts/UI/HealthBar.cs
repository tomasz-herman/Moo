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
        healthSystem.HealthChanged += UpdateBar;
        UpdateBar(this, (healthSystem.Health, healthSystem.MaxHealth));
    }

    private void UpdateBar(object sender, (float health, float maxHealth) args)
    {
        fill.transform.localScale = new Vector3(args.health / args.maxHealth, 1, 1);

        int displayHealth = Mathf.CeilToInt(args.health);
        int displayMaxHealth = Mathf.CeilToInt(args.maxHealth);
        text.text = $"{displayHealth}/{displayMaxHealth}";
    }
}
