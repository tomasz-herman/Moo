using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public GameObject deathSummon;
    public Vector3 summonPos1, summonPos2;
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

        //spawn new enemy
        Instantiate(deathSummon, new Vector3(Utils.FloatBetween(summonPos1.x, summonPos2.x),
            Utils.FloatBetween(summonPos1.y, summonPos2.y), Utils.FloatBetween(summonPos1.z, summonPos2.z)), Quaternion.identity);

        //drop loot
        dropSystem.Drop();

        Destroy(gameObject);

        //TODO: delete this when vignette use case is implemented
        FindObjectOfType<DamagePostProcessing>().ApplyVignette();

        KillEvent.Invoke();

    }
}
