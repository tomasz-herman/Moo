using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner
{
    public static int MaxNumber = 0;
    private int numberOfAliveEnemies = 0;
    public UnityEngine.Events.UnityEvent AllEnemiesKilled = new UnityEngine.Events.UnityEvent();
    public void Spawn(List<SpawnLocationScript> spawnLocations, ChamberType chamberType, int chamberNumber)
    {
        EnemiesSpawnData currentSpawnData = EnemiesData.GetData();
        int numberOfEnemys;
        switch (chamberType)
        {
            case ChamberType.Normal:
                numberOfEnemys = Utils.NumberBetween(currentSpawnData.MinEnemiesInNormalChamber, (spawnLocations.Count - 1) < currentSpawnData.MaxEnemiesInNormalChamber ? spawnLocations.Count - 1 : currentSpawnData.MaxEnemiesInNormalChamber);
                SpawnNumberOfEnemies(numberOfEnemys, spawnLocations, chamberNumber, currentSpawnData);
                break;
            case ChamberType.Boss:
                numberOfEnemys = Utils.NumberBetween(currentSpawnData.MinEnemiesInBossChamber, (spawnLocations.Count - 1) < currentSpawnData.MaxEnemiesInBossChamber ? spawnLocations.Count - 1 : currentSpawnData.MaxEnemiesInBossChamber);
                SpawnNumberOfEnemies(numberOfEnemys, spawnLocations, chamberNumber, currentSpawnData, EnemyTypes.Boss);
                break;
            case ChamberType.Optional:
                numberOfEnemys = Utils.NumberBetween(currentSpawnData.MinEnemiesInOptionalChamber, (spawnLocations.Count - 1) < currentSpawnData.MaxEnemiesInOptionalChamber ? spawnLocations.Count - 1 : currentSpawnData.MaxEnemiesInOptionalChamber);
                SpawnNumberOfEnemies(numberOfEnemys, spawnLocations, chamberNumber, currentSpawnData, EnemyTypes.MiniBoss);
                break;
            case ChamberType.Start:
            default:
                break;
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

    private void SpawnEnemy(EnemyTypes type, Vector3 position)
    {
        EnemyPrefabInfo enemyinfo = Enemys.GetEnemyInfoFromType(type);
        var enemy = GameObject.Instantiate(enemyinfo.enemy, position, Quaternion.identity);
        numberOfAliveEnemies++;
        enemy.GetComponent<Enemy>().KillEvent.AddListener(KillHandler);
        TeleporterEffectScript.CreateTeleporterForEntity(enemy, enemyinfo.teleporterScale);
    }

    private EnemyTypes RandomEnemy(int chamberNumber, EnemiesSpawnData currData)
    {
        if (Utils.FloatBetween(0, (float)chamberNumber / (float)MaxNumber) > currData.BigEnemySpawnThreshold)
            return EnemyTypes.Big;
        if (Utils.FloatBetween(0, (float)chamberNumber / (float)MaxNumber) > currData.MediumEnemySpawnThreshold)
            return EnemyTypes.Medium;
        return EnemyTypes.Small;
    }

    private void SpawnNumberOfEnemies(int numberOfEnemies, List<SpawnLocationScript> spawnLocations, int chamberNumber, EnemiesSpawnData currData,  EnemyTypes? BossType = null)
    {

        foreach (var item in spawnLocations.OrderBy(x => Utils.RandomNumber()))
        {
            if (numberOfEnemies <= 0)
                return;
            if (numberOfEnemies == 1 && BossType != null)
                SpawnEnemy(BossType.Value, item.transform.position);
            else
                SpawnEnemy(RandomEnemy(chamberNumber, currData), item.transform.position);
            numberOfEnemies--;
        }
    }
}
