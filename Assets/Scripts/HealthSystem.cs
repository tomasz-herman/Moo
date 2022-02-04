using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [HideInInspector] public float defaultHealth = 100f;
    private float health, maxHealth;
    public float Health
    {
        get { return health; }
        set
        {
            float previousHealth = health;
            health = value;
            if (health > maxHealth)
                health = maxHealth;
            HealthChanged?.Invoke(this, (health, maxHealth));

            if (health < previousHealth)
                DamageReceived?.Invoke(this, previousHealth - health);
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
    public EventHandler<float> DamageReceived;

    void Awake()
    {
        health = maxHealth = defaultHealth;
    }
}
