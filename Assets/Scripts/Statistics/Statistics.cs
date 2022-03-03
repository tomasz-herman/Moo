using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{

    public StatisticsSystem statisticsSystem;

    public AmmoSystem ammoSystem;

    public HealthSystem healthSystem;

    public PlayerMovement playerMovement;

    public UpgradeSystem upgradeSystem;

    void Start()
    {
        statisticsSystem = GetComponent<StatisticsSystem>();
        ammoSystem = GetComponent<AmmoSystem>();
        healthSystem = GetComponent<HealthSystem>();
        playerMovement = GetComponent<PlayerMovement>();
        upgradeSystem = GetComponent<UpgradeSystem>();

        ammoSystem.AmmoChanged += UpdateAmmo;
        healthSystem.HealthChanged += UpdateHealth;
        playerMovement.SpeedChanged += UpdateMovement;
        upgradeSystem.Upgraded += UpdateWeapon;

        UpdateAmmo(this, (ammoSystem.Ammo, ammoSystem.MaxAmmo));
        UpdateHealth(this, (healthSystem.Health, healthSystem.MaxHealth));
        UpdateMovement(this, playerMovement.Speed);

        //TODO:
        var shooting = GetComponent<Shooting>();
        var dict = new Dictionary<UpgradeType, (StatisticType, Func<float>)>();
        dict.Add(UpgradeType.PistolDamage, (StatisticType.PistolDamage, () => shooting[WeaponType.Pistol].Damage));
        dict.Add(UpgradeType.PistolCooldown, (StatisticType.PistolCooldown, () => shooting[WeaponType.Pistol].TriggerTimeout));
        dict.Add(UpgradeType.PistolProjectileSpeed, (StatisticType.PistolProjectileSpeed, () => shooting[WeaponType.Pistol].ProjectileSpeed));
        dict.Add(UpgradeType.PistolAmmoCost, (StatisticType.PistolAmmoCost, () => shooting[WeaponType.Pistol].AmmoConsumption));

        dict.Add(UpgradeType.ShotgunDamage, (StatisticType.ShotgunDamage, () => shooting[WeaponType.Shotgun].Damage));
        dict.Add(UpgradeType.ShotgunCooldown, (StatisticType.ShotgunCooldown, () => shooting[WeaponType.Shotgun].TriggerTimeout));
        dict.Add(UpgradeType.ShotgunProjectileSpeed, (StatisticType.ShotgunProjectileSpeed, () => shooting[WeaponType.Shotgun].ProjectileSpeed));
        dict.Add(UpgradeType.ShotgunAmmoCost, (StatisticType.ShotgunAmmoCost, () => shooting[WeaponType.Shotgun].AmmoConsumption));
        dict.Add(UpgradeType.ShotgunProjectileCount, (StatisticType.ShotgunProjectileCount, () => ((Shotgun)shooting[WeaponType.Shotgun]).ProjectileCount));
        dict.Add(UpgradeType.ShotgunProjectileDispersion, (StatisticType.ShotgunProjectileDispersion, () => ((Shotgun)shooting[WeaponType.Shotgun]).ScatterAngle));

        dict.Add(UpgradeType.MachineGunDamage, (StatisticType.MachineGunDamage, () => shooting[WeaponType.MachineGun].Damage));
        dict.Add(UpgradeType.MachineGunCooldown, (StatisticType.MachineGunCooldown, () => shooting[WeaponType.MachineGun].TriggerTimeout));
        dict.Add(UpgradeType.MachineGunProjectileSpeed, (StatisticType.MachineGunProjectileSpeed, () => shooting[WeaponType.MachineGun].ProjectileSpeed));
        dict.Add(UpgradeType.MachineGunAmmoCost, (StatisticType.MachineGunAmmoCost, () => shooting[WeaponType.MachineGun].AmmoConsumption));

        dict.Add(UpgradeType.GrenadeLauncherDamage, (StatisticType.GrenadeLauncherDamage, () => shooting[WeaponType.GrenadeLauncher].Damage));
        dict.Add(UpgradeType.GrenadeLauncherCooldown, (StatisticType.GrenadeLauncherCooldown, () => shooting[WeaponType.GrenadeLauncher].TriggerTimeout));
        dict.Add(UpgradeType.GrenadeLauncherProjectileSpeed, (StatisticType.GrenadeLauncherProjectileSpeed, () => shooting[WeaponType.GrenadeLauncher].ProjectileSpeed));
        dict.Add(UpgradeType.GrenadeLauncherAmmoCost, (StatisticType.GrenadeLauncherAmmoCost, () => shooting[WeaponType.GrenadeLauncher].AmmoConsumption));

        dict.Add(UpgradeType.SwordDamage, (StatisticType.SwordDamage, () => shooting[WeaponType.Sword].Damage));
        dict.Add(UpgradeType.SwordCooldown, (StatisticType.SwordCooldown, () => shooting[WeaponType.Sword].TriggerTimeout));

        updateWeaponDictionary = dict;

        //rewrite values into array to improve performance and initialize Statistic
        foreach (var upgrade in dict)
        {
            UpdateWeapon(this, (upgrade.Key, 0));
        }
    }


    void UpdateAmmo(object sender, (float ammo, float maxAmmo) args)
    {
        statisticsSystem.SetStatistic(StatisticType.MaxAmmo, args.maxAmmo);
    }

    void UpdateHealth(object sender, (float health, float maxHealth) args)
    {
        statisticsSystem.SetStatistic(StatisticType.MaxHealth, args.maxHealth);
    }

    void UpdateMovement(object sender, float speed)
    {
        statisticsSystem.SetStatistic(StatisticType.Speed, speed);
    }

    private Dictionary<UpgradeType, (StatisticType type, Func<float> getValue)> updateWeaponDictionary;

    void UpdateWeapon(object sender, (UpgradeType type, int Count) tuple)
    {
        if (!updateWeaponDictionary.ContainsKey(tuple.type)) return;

        var type = updateWeaponDictionary[tuple.type].type;
        var value = updateWeaponDictionary[tuple.type].getValue();
        statisticsSystem.SetStatistic(type, value);
    }
}
