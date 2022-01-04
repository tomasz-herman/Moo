using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int pointsForKill = 1;
    
    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public DropSystem dropSystem;
    
    public UnityEngine.Events.UnityEvent KillEvent;

    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        dropSystem = GetComponent<DropSystem>();
    }

    public void TakeDamage(float damage, ScoreSystem system = null)
    {
        healthSystem.Health -= damage;
        if (healthSystem.Health > 0) return;
        Die(system);
    }

    private void Die(ScoreSystem system = null)
    {
        system?.AddScore(pointsForKill);
    
        //drop loot
        dropSystem.Drop();

        Destroy(gameObject);

        KillEvent.Invoke();

    }
}
