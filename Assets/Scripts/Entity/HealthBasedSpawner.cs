using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBasedSpawner : MonoBehaviour
{
    [SerializeField] private LinkedList<SpawnRule> spawnRules;

    private List<Enemy> spawnedChildren = new List<Enemy>();
    
    public void Start()
    {
        var ordered = spawnRules.OrderByDescending(rule => rule.HealthPercent).ToList();
        spawnRules.Clear();
        foreach (var rule in ordered)
            spawnRules.AddLast(rule);

        var enemy = GetComponent<Enemy>();

        enemy.healthSystem.HealthChanged += OnHealthChanged;
        enemy.KillEvent.AddListener(OnParentDeath);
        
    }

    private void OnHealthChanged(object sender, (float health, float maxHealth) args)
    {
        float healthPercent = 100 * args.health / args.maxHealth;
        while(spawnRules.Count > 0 && spawnRules.First.Value.HealthPercent >= healthPercent)
        {
            var toSpawn = spawnRules.First.Value.Enemies;
            spawnRules.RemoveFirst();

            foreach(var type in toSpawn)
            {
                //TODO: spawn enemies, set level, kill on parent death, add to spawnedChildren list
            }
        }
    }

    private void OnParentDeath(GameObject obj)
    {
        var enemy = obj.GetComponent<Enemy>();
        enemy.healthSystem.HealthChanged -= OnHealthChanged;
        enemy.KillEvent.RemoveListener(OnParentDeath);

        foreach(var child in spawnedChildren)
        {
            child.Kill();
        }
    }
}

public class SpawnRule
{
    public float HealthPercent;
    public List<EnemyTypes> Enemies = new List<EnemyTypes>();
}
