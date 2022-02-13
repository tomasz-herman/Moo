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
        int numberOfEnemys;
        ChamberType chamberType = chamberNode.Type;
        int chamberLevel = chamberNode.Level;
        int chamberNumber = chamberNode.Number;
        switch (chamberType)
        {
            case ChamberType.Normal:
                numberOfEnemys = Utils.NumberBetween(currentSpawnData.MinEnemiesInNormalChamber, (spawnLocations.Count - 1) < currentSpawnData.MaxEnemiesInNormalChamber ? spawnLocations.Count - 1 : currentSpawnData.MaxEnemiesInNormalChamber);
                return SpawnNumberOfEnemies(numberOfEnemys, spawnLocations, chamberNumber, currentSpawnData, chamberLevel);
            case ChamberType.Boss:
                numberOfEnemys = Utils.NumberBetween(currentSpawnData.MinEnemiesInBossChamber, (spawnLocations.Count - 1) < currentSpawnData.MaxEnemiesInBossChamber ? spawnLocations.Count - 1 : currentSpawnData.MaxEnemiesInBossChamber);
                return SpawnNumberOfEnemies(numberOfEnemys, spawnLocations, chamberNumber, currentSpawnData, chamberLevel, EnemyTypes.Boss);
            case ChamberType.Optional:
                numberOfEnemys = Utils.NumberBetween(currentSpawnData.MinEnemiesInOptionalChamber, (spawnLocations.Count - 1) < currentSpawnData.MaxEnemiesInOptionalChamber ? spawnLocations.Count - 1 : currentSpawnData.MaxEnemiesInOptionalChamber);
                return SpawnNumberOfEnemies(numberOfEnemys, spawnLocations, chamberNumber, currentSpawnData, chamberLevel, EnemyTypes.MiniBoss);
            case ChamberType.Start:
            default:
                return null;
        }
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

    private EnemyTypes RandomEnemy(int chamberNumber, EnemiesSpawnData currData)
    {
        if (Utils.FloatBetween(0, (float)chamberNumber / (float)MaxNumber) > currData.BigEnemySpawnThreshold)
            return EnemyTypes.Big;
        if (Utils.FloatBetween(0, (float)chamberNumber / (float)MaxNumber) > currData.MediumEnemySpawnThreshold)
            return EnemyTypes.Medium;
        return EnemyTypes.Small;
    }

    private Enemy SpawnNumberOfEnemies(int numberOfEnemies, List<SpawnLocationScript> spawnLocations, int chamberNumber, EnemiesSpawnData currData, int level, EnemyTypes? BossType = null)
    {
        Enemy boss = null;
        foreach (var item in spawnLocations.OrderBy(x => Utils.RandomNumber()))
        {
            if (numberOfEnemies <= 0)
                return boss;
            if (numberOfEnemies == 1 && BossType != null)
                boss = SpawnEnemy(BossType.Value, item.transform.position, level);
            else
                SpawnEnemy(RandomEnemy(chamberNumber, currData), item.transform.position, level);
            numberOfEnemies--;
        }
        return boss;
    }
}
