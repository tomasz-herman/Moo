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
    private bool visible;

    public bool Visible
    {
        get { return visible; }
        set
        {
            visible = value;
            CalculateVisibility();
        }
    }

    public Enemy TrackedEnemy
    {
        get { return trackedEnemy; }
        set
        {
            if (trackedEnemy != null)
            {
                healthSystem.HealthChanged -= UpdateBar;
                trackedEnemy.KillEvent.RemoveListener(OnKill);
            }
            trackedEnemy = value;
            if (value != null)
            {
                healthSystem = trackedEnemy.healthSystem;
                healthSystem.HealthChanged += UpdateBar;
                trackedEnemy.KillEvent.AddListener(OnKill);
                UpdateBar(this, (healthSystem.Health, healthSystem.MaxHealth));
            }
            CalculateVisibility();
        }
    }

    public Color Color { get { return bar.color; } set { bar.color = value; } }

    private void Start()
    {
        Visible = true;
        CalculateVisibility();
    }

    private float timeToSwap = 0;
    private void Update()
    {
        if (timeToSwap <= 0)
        {
            TrackedEnemy = FindObjectOfType<Enemy>();
            Color = new Color(Random.value, Random.value, Random.value);
            timeToSwap = 5;
        }
        timeToSwap -= Time.deltaTime;
    }

    private void CalculateVisibility()
    {
        bool show = visible && trackedEnemy != null;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(show);
        }
    }

    private void UpdateBar(object sender, (float health, float maxHealth) args)
    {
        bar.transform.localScale = new Vector3(args.health / args.maxHealth, 1, 1);

        int healthPercent = Mathf.CeilToInt(100 * args.health / args.maxHealth);
        text.text = $"{healthPercent}%";
    }

    private void OnKill(GameObject obj)
    {
        TrackedEnemy = null;
    }
}
