using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBasedSpawner : MonoBehaviour
{
    [SerializeField] private LinkedList<SpawnRule> spawnRules = new LinkedList<SpawnRule>();
    [SerializeField] private GameObject debugObject;

    public float MinSpawnRadius = 1;
    public float MaxSpawnRadius = 5;
    public int NumberOfSpawnTriesBeforeFail = 20;

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

    private void Spawn(EnemyTypes type)
    {
        var controller = Enemys.GetEnemyInfoFromType(type).enemy.GetComponent<CharacterController>();
        float capsuleRadius = controller.radius;
        float capsuleCenterY = controller.center.y;
        Vector3 bottomToCenter = new Vector3(0, capsuleCenterY, 0);
        Vector3 centerToUpperSphere = new Vector3(0, controller.height / 2, 0);

        Vector3 spawnLocation = parent.transform.position;
        for (int i = 0; i < NumberOfSpawnTriesBeforeFail; i++)
        {
            Vector3 direction = new Vector3(Utils.FloatBetween(-1, 1), 0, Utils.FloatBetween(1, 1)).normalized;
            Vector3 tempSpawnLocation = parent.transform.position + direction * Utils.FloatBetween(MinSpawnRadius, MaxSpawnRadius);
            Vector3 capsuleCenter = spawnLocation + bottomToCenter;

            Instantiate(debugObject, capsuleCenter - centerToUpperSphere, Quaternion.identity);
            Instantiate(debugObject, capsuleCenter + centerToUpperSphere, Quaternion.identity);

            if(!Physics.CheckCapsule(capsuleCenter - centerToUpperSphere, capsuleCenter + centerToUpperSphere, capsuleRadius, Layers.TerrainLayers))
            {
                spawnLocation = tempSpawnLocation;
                break;
            }
        }

        Enemy child = parent.GameWorld.SpawnEnemy(type, spawnLocation, parent.Level);
        spawnedChildren.Add(child);
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
                Spawn(type);
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
