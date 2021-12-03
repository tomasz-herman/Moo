using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public GameObject deathSummon;
    public GameObject dropItem;
    public float dropChance = 0.5f;
    public Vector3 summonPos1, summonPos2;
    public int pointsForKill = 1;
    
    public HealthSystem healthSystem;
    
    public UnityEngine.Events.UnityEvent KillEvent;

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
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
        if (deathSummon != null)
            Instantiate(deathSummon, new Vector3(Utils.FloatBetween(summonPos1.x, summonPos2.x),
                Utils.FloatBetween(summonPos1.y, summonPos2.y), Utils.FloatBetween(summonPos1.z, summonPos2.z)), Quaternion.identity);
        if (dropItem != null)
        {
            if (Utils.FloatBetween(0, 1) <= dropChance)
                Instantiate(dropItem, transform.position, transform.rotation);
        }
        Destroy(gameObject);

        //TODO: delete this when vignette use case is implemented
        FindObjectOfType<DamagePostProcessing>().ApplyVignette();

        KillEvent.Invoke();

    }
}