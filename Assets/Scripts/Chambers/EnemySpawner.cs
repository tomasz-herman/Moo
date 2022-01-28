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
        int numberOfEnemys;
        switch (chamberType)
        {
            case ChamberType.Normal:
                numberOfEnemys = Utils.NumberBetween(2, spawnLocations.Count-1);
                foreach (var item in spawnLocations.OrderBy(x => Utils.RandomNumber()))
                {
                    if (numberOfEnemys <= 0)
                        return;
                    SpawnEnemy(RandomEnemy(chamberNumber), item.transform.position);
                    numberOfEnemys--;
                }
                break;
            case ChamberType.Boss:
                SpawnEnemy(EnemyTypes.Boss, spawnLocations[Utils.NumberBetween(0, spawnLocations.Count-1)].transform.position);
                break;
            case ChamberType.Optional:
                numberOfEnemys = Utils.NumberBetween(1, spawnLocations.Count/2);
                foreach (var item in spawnLocations.OrderBy(x => Utils.RandomNumber()))
                {
                    if (numberOfEnemys <= 0)
                        return;
                    if(numberOfEnemys==1)
                        SpawnEnemy(EnemyTypes.MiniBoss, item.transform.position);
                    else
                    SpawnEnemy(RandomEnemy(chamberNumber), item.transform.position);
                    numberOfEnemys--;
                }
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

    private EnemyTypes RandomEnemy(int chamberNumber)
    {
        if (Utils.FloatBetween(0, (float)chamberNumber / (float)MaxNumber) > 0.25)
            return EnemyTypes.Big;
        if (Utils.FloatBetween(0, (float)chamberNumber / (float)MaxNumber) > 0.125)
            return EnemyTypes.Medium;
        return EnemyTypes.Small;
    }
}
