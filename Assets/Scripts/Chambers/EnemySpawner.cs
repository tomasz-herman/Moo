using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner
{
    public static int MaxNumber = 0;
    private int numberOfAliveEnemies = 0;
    public UnityEngine.Events.UnityEvent AllEnemiesKilled = new UnityEngine.Events.UnityEvent();

    /// <returns>Current boss (if spawned) or null (if not)</returns>
    public Enemy Spawn(List<SpawnLocationScript> spawnLocations, ChamberType chamberType, int chamberNumber)
    {
        EnemiesSpawnData currentSpawnData = EnemiesData.GetData();
        int numberOfEnemys;
        int chamberLevel = GetChamberLevel(chamberNumber);
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

    //TODO: Dummy method, we need API to get the actual chamber level
    private int GetChamberLevel(int chamberNumber)
    {
        return 1;
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
        EnemyPrefabInfo enemyinfo = Enemys.GetEnemyInfoFromType(type);
        var enemy = GameObject.Instantiate(enemyinfo.enemy, position, Quaternion.identity);
        numberOfAliveEnemies++;
        Enemy enemyClass = enemy.GetComponent<Enemy>();
        enemyClass.Level = level;
        enemyClass.Spawn();
        enemyClass.KillEvent.AddListener(KillHandler);
        TeleporterEffectScript.CreateTeleporterForEntity(enemy, enemyinfo.teleporterScale);
        return enemyClass;
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
