using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons;
using UnityEngine;
using Weapons;

public class BossEnemyAI : SimpleEnemyAI
{
    public int phases = 3;
    private List<WeaponAIProperties> weapons;
    
    private new void Start()
    {
        GetRandomWeapons();
        base.Start();
    }

    private void GetRandomWeapons()
    {
        WeaponAIProperties[] availableWeapons = ApplicationData.WeaponAIData.Data.ToArray();
        for (var n = 0; n < availableWeapons.Length; n++)
        {
            var k = Utils.NumberBetween(0, availableWeapons.Length - 1);
            (availableWeapons[n], availableWeapons[k]) = (availableWeapons[k], availableWeapons[n]);
        }
        foreach (var availableWeapon in availableWeapons)
        {
            Debug.Log(availableWeapon);
        }

        weapons = availableWeapons.Take(phases).ToList();
    }

    private new void Update()
    {
        weaponAIProperties = weapons[GetPhase()];
        base.Update();
    }

    private int GetPhase()
    {
        float health = healthSystem.Health / healthSystem.MaxHealth;
        if (health <= 0) return 0;
        if (health >= 1) return phases - 1;
        return (int)(health * phases);
    }
}
