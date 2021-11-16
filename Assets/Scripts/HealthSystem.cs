using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float defaultHealth = 100f;
    private float health, maxHealth;
    public float Health
    {
        get { return health; }
        set
        { 
            health = value;
            if (health > maxHealth)
                health = maxHealth;
            HealthChanged?.Invoke(this, (health, maxHealth));
        }
    }
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            HealthChanged?.Invoke(this, (health, maxHealth));
        }
    }

    public EventHandler<(float health, float maxHealth)> HealthChanged;

    void Start()
    {
        health = maxHealth = defaultHealth;
    }
}
