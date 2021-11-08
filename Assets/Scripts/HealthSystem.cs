using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int defaultHealth = 100;
    private int health, maxHealth;
    public int Health
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
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            HealthChanged?.Invoke(this, (health, maxHealth));
        }
    }

    public EventHandler<(float, float)> HealthChanged;

    void Start()
    {
        health = maxHealth = defaultHealth;
    }
}
