using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner
{
    public static int MaxNumber = 0;
    private int numberOfAliveEnemies = 0;
    public UnityEngine.Events.UnityEvent AllEnemiesKilled = new UnityEngine.Events.UnityEvent();
    private GameWorld world;

    public EnemySpawner(GameWorld world)
    {
        this.world = world;
    }

    public IEnumerable<Enemy> Spawn(List<SpawnLocationScript> spawnLocations, ChamberNode chamberNode)
    {
        EnemiesSpawnData currentSpawnData = EnemiesData.GetData();
        ChamberType chamberType = chamberNode.Type;
        int chamberLevel = chamberNode.Level;
        int chamberNumber = chamberNode.Number;

        var enemies = currentSpawnData.GetEnemiesForChamber(chamberNode);

        var spawned = SpawnEnemies(enemies, spawnLocations, chamberNumber, chamberLevel);

        return spawned;
    }

    private void KillHandler(GameObject sender)
    {
        sender.GetComponent<Enemy>().KillEvent.RemoveListener(KillHandler);
        numberOfAliveEnemies--;
        if (AllEnemiesDead())
            AllEnemiesKilled.Invoke();
    }

    public bool AllEnemiesDead()
    {
        return numberOfAliveEnemies <= 0;
    }

    private Enemy SpawnEnemy(EnemyTypes type, Vector3 position, int level)
    {
        var enemy = world.SpawnEnemy(type, position, level);
        numberOfAliveEnemies++;
        enemy.KillEvent.AddListener(KillHandler);
        return enemy;
    }

    private IEnumerable<Enemy> SpawnEnemies(IEnumerable<EnemyTypes> enemies, List<SpawnLocationScript> spawnLocations, int chamberNumber, int level)
    {
        var spawned = new List<Enemy>();

        var shuffled = spawnLocations.OrderBy(x => Utils.RandomNumber()).ToList();
        int i = 0;

        foreach (var item in enemies)
        {
            var spawn = shuffled[i];
            spawned.Add(SpawnEnemy(item, spawn.transform.position, level));
            i = (i + 1) % shuffled.Count;
        }

        return spawned;
    }
}
