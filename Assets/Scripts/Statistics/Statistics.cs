using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{

    public StatisticsSystem statisticsSystem;

    public AmmoSystem ammoSystem;

    public HealthSystem healthSystem;

    public PlayerMovement playerMovement;

    void Start()
    {
        statisticsSystem = GetComponent<StatisticsSystem>();
        ammoSystem = GetComponent<AmmoSystem>();
        healthSystem = GetComponent<HealthSystem>();
        playerMovement = GetComponent<PlayerMovement>();

        ammoSystem.AmmoChanged += UpdateAmmo;
        healthSystem.HealthChanged += UpdateHealth;
        playerMovement.SpeedChanged += UpdateMovement;
        UpdateAmmo(this, (ammoSystem.Ammo, ammoSystem.MaxAmmo));
        UpdateHealth(this, (healthSystem.Health, healthSystem.MaxHealth));
        UpdateMovement(this, playerMovement.Speed);
    }


    void UpdateAmmo(object sender, (int ammo, int maxAmmo) args)
    {
        statisticsSystem.SetStatistic(StatisticType.Ammo, args.ammo);
        statisticsSystem.SetStatistic(StatisticType.MaxAmmo, args.maxAmmo);
    }

    void UpdateHealth(object sender, (float health, float maxHealth) args)
    {
        statisticsSystem.SetStatistic(StatisticType.Health, args.health);
        statisticsSystem.SetStatistic(StatisticType.MaxHealth, args.maxHealth);
    }

    void UpdateMovement(object sender, float speed)
    {
        statisticsSystem.SetStatistic(StatisticType.Speed, speed);
    }
}
