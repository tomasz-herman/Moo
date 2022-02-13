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

    /// <returns>Current boss (if spawned) or null (if not)</returns>
    public Enemy Spawn(List<SpawnLocationScript> spawnLocations, ChamberNode chamberNode)
    {
        EnemiesSpawnData currentSpawnData = EnemiesData.GetData();
        ChamberType chamberType = chamberNode.Type;
        int chamberLevel = chamberNode.Level;
        int chamberNumber = chamberNode.Number;

        var enemies = currentSpawnData.GetRandomEnemiesForChamber(chamberType, chamberNode.MainProgress);

        EnemyTypes? bossType = chamberType switch
        {
            ChamberType.Boss => EnemyTypes.Boss,
            ChamberType.Optional => EnemyTypes.MiniBoss,
            _ => null
        };

        var boss = SpawnEnemies(enemies, spawnLocations, chamberNumber, chamberLevel, bossType);

        return boss;
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

    private Enemy SpawnEnemies(IEnumerable<EnemyTypes> enemies, List<SpawnLocationScript> spawnLocations, int chamberNumber, int level, EnemyTypes? bossType = null)
    {
        var shuffled = spawnLocations.OrderBy(x => Utils.RandomNumber()).ToList();
        int i = 0;

        foreach (var item in enemies)
        {
            var spawn = shuffled[i];
            SpawnEnemy(item, spawn.transform.position, level);
            i = (i + 1) % shuffled.Count;
        }

        if (bossType != null)
            return SpawnEnemy(bossType.Value, shuffled[i].transform.position, level);

        return null;
    }
}
