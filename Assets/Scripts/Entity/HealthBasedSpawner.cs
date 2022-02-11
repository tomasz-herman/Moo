using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBasedSpawner : MonoBehaviour
{
    [SerializeField] private LinkedList<SpawnRule> spawnRules = new LinkedList<SpawnRule>();

    private List<Enemy> spawnedChildren = new List<Enemy>();
    private Enemy parent;
    public void Start()
    {
        for (int i = 80; i >= 20; i -= 20)
            spawnRules.AddLast(new SpawnRule() { HealthPercent = i, Enemies = new List<EnemyTypes>() { EnemyTypes.Small, EnemyTypes.Small, EnemyTypes.Medium, EnemyTypes.Big } });

        var ordered = spawnRules.OrderByDescending(rule => rule.HealthPercent).ToList();
        spawnRules.Clear();
        foreach (var rule in ordered)
            spawnRules.AddLast(rule);

        parent = GetComponent<Enemy>();

        parent.healthSystem.HealthChanged += OnHealthChanged;
        parent.KillEvent.AddListener(OnParentDeath);
    }

    private void OnHealthChanged(object sender, (float health, float maxHealth) args)
    {
        var world = parent.GameWorld;
        var position = parent.transform.position;
        var level = parent.Level;

        float healthPercent = 100 * args.health / args.maxHealth;
        while(spawnRules.Count > 0 && spawnRules.First.Value.HealthPercent >= healthPercent)
        {
            var toSpawn = spawnRules.First.Value.Enemies;
            spawnRules.RemoveFirst();

            foreach(var type in toSpawn)
            {
                Enemy child = world.SpawnEnemy(type, position, level);
                spawnedChildren.Add(child);
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
