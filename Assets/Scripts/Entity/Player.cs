using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Shooting shooting;
    public PlayerMovement movement;
    public HealthSystem healthSystem;
    public UpgradeSystem upgradeSystem;
    public AmmoSystem ammoSystem;
    public ScoreSystem scoreSystem;
    public float TeleporterScale = 2;

    private DamagePostProcessing damagePostProcessing;
    void Start()
    {
        shooting = GetComponent<Shooting>();
        movement = GetComponent<PlayerMovement>();
        healthSystem = GetComponent<HealthSystem>();
        upgradeSystem = GetComponent<UpgradeSystem>();
        ammoSystem = GetComponent<AmmoSystem>();
        scoreSystem = GetComponent<ScoreSystem>();

        healthSystem.HealthChanged += CheckDeath;
        healthSystem.DamageReceived += OnDamageReceived;
        healthSystem.MaxHealth = ApplicationData.GameplayData.DefaultPlayerHealth;
        healthSystem.Health = healthSystem.MaxHealth;

        damagePostProcessing = FindObjectOfType<DamagePostProcessing>();
        damagePostProcessing.healthSystem = healthSystem;

        ammoSystem.MaxAmmo = ApplicationData.GameplayData.DefaultPlayerAmmo;
        ammoSystem.Ammo = ammoSystem.MaxAmmo;
    }

    public void Upgrade(int upgradeCount = 1)
    {
        upgradeSystem.AddUpgrade(upgradeCount);
    }

    public void CheckDeath(object sender, (float health, float maxHealth) args)
    {
        if(args.health <= 0)
        {
            GameWorld.EndGame(false, scoreSystem.GetScore());
        }
    }

    private void OnDamageReceived(object sender, float damage)
    {
        damagePostProcessing.ApplyVignette();
    }
}
