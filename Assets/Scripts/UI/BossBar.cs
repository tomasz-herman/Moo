using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] public Image bar, weapon;
    [SerializeField] public TMP_Text text;

    private HealthSystem healthSystem;
    private Enemy trackedEnemy;

    public Enemy TrackedEnemy
    {
        get { return trackedEnemy; }
        set
        {
            if (trackedEnemy != null)
            {
                healthSystem.HealthChanged -= UpdateBar;
            }
            trackedEnemy = value;
            if (value != null)
            {
                healthSystem = trackedEnemy.healthSystem;
                healthSystem.HealthChanged += UpdateBar;
                UpdateBar(this, (healthSystem.Health, healthSystem.MaxHealth));
            }
        }
    }

    private float timeToSwap = 0;
    private void Update()
    {
        if (timeToSwap <= 0)
        {
            TrackedEnemy = FindObjectOfType<Enemy>();
            timeToSwap = 5;
        }
        timeToSwap -= Time.deltaTime;
    }

    private void UpdateBar(object sender, (float health, float maxHealth) args)
    {
        bar.transform.localScale = new Vector3(args.health / args.maxHealth, 1, 1);

        int healthPercent = Mathf.CeilToInt(100 * args.health / args.maxHealth);
        text.text = $"{healthPercent}%";
    }
}
