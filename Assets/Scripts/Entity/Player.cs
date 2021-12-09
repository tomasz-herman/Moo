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
    void Start()
    {
        shooting = GetComponent<Shooting>();
        movement = GetComponent<PlayerMovement>();
        healthSystem = GetComponent<HealthSystem>();
        upgradeSystem = GetComponent<UpgradeSystem>();
        ammoSystem = GetComponent<AmmoSystem>();
        scoreSystem = GetComponent<ScoreSystem>();

        healthSystem.HealthChanged += CheckDeath;
    }

    public void Upgrade()
    {
        upgradeSystem.AddUpgrade();
    }

    public void CheckDeath(object sender, (float health, float maxHealth) args)
    {
        if(args.health <= 0)
        {
            GameWorld.EndGame(false, scoreSystem.GetScore());
        }
    }
}
