using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons;
using UnityEngine;

public class BossEnemyAI : SimpleEnemyAI
{
    public int phases = 3;
    private List<WeaponAI> weapons;
    
    private new void Start()
    {
        GetRandomWeapons();
        base.Start();
    }

    private void GetRandomWeapons()
    {
        WeaponAI[] availableWeapons = (WeaponAI[])Enum.GetValues(typeof(WeaponAI));
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
        foreach (var weapon in weapons)
        {
            Debug.Log("Got : " + weapon);
        }    
    }

    private new void Update()
    {
        weaponAI = weapons[GetPhase()];
        weaponAIProperties = WeaponAIProperties.Get(weaponAI);
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
