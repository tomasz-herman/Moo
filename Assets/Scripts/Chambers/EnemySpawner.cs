using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner
{
    public static int MaxNumber = 0;
    private int numberOfAliveEnemys = 0;
    private static GameObject TeleportEffect = null;
    public void Spawn(List<SpawnLocationScript> spawnLocations, ChamberType chamberType, int chamberNumber)
    {
        int numberOfEnemys;
        switch (chamberType)
        {
            case ChamberType.Normal:
                numberOfEnemys = Utils.NumberBetween(2, spawnLocations.Count-1);
                foreach (var item in spawnLocations.OrderBy(x => Utils.RandomNumber()))
                {
                    SpawnEnemy(RandomEnemy(chamberNumber), item.transform.position);
                    numberOfEnemys--;
                    if (numberOfEnemys <= 0)
                        return;
                }
                break;
            case ChamberType.Boss:
                SpawnEnemy(EnemyTypes.Boss, spawnLocations[Utils.NumberBetween(0, spawnLocations.Count-1)].transform.position);
                break;
            case ChamberType.Optional:
                numberOfEnemys = Utils.NumberBetween(1, spawnLocations.Count/2);
                foreach (var item in spawnLocations.OrderBy(x => Utils.RandomNumber()))
                {
                    if(numberOfEnemys==1)
                        SpawnEnemy(EnemyTypes.MiniBoss, item.transform.position);
                    else
                    SpawnEnemy(RandomEnemy(chamberNumber), item.transform.position);
                    numberOfEnemys--;
                    if (numberOfEnemys <= 0)
                        return;
                }
                break;
            case ChamberType.Start:
            default:
                break;
        }
    }

    private void KillHandler(GameObject sender)
    {
        numberOfAliveEnemys--;
    }

    public bool AllEnemysDead()
    {
        if (numberOfAliveEnemys > 0)
            return false;
        return true;
    }

    private void SpawnEnemy(EnemyTypes type, Vector3 position)
    {
        EnemyPrefabInfo enemyinfo = Enemys.GetEnemyInfoFromType(type);
        var enemy = GameObject.Instantiate(enemyinfo.enemy, position, Quaternion.identity);
        numberOfAliveEnemys++;
        enemy.GetComponent<Enemy>().KillEvent.AddListener(KillHandler);
        var tele = GameObject.Instantiate(getTeleporterPrefab(), position, Quaternion.identity).GetComponent<TeleporterEffectScript>();
        tele.gameObject.transform.localScale *= enemyinfo.teleporterScale;
        tele.AddSpawnedObject(enemy);
    }

    private static GameObject getTeleporterPrefab()
    {
        if (TeleportEffect == null)
        {
            TeleportEffect = Resources.Load<GameObject>("TeleporterEffect");
        }
            return TeleportEffect;
    }

    private EnemyTypes RandomEnemy(int chamberNumber)
    {
        if (Utils.FloatBetween(0, chamberNumber*2 / MaxNumber) > 0.5)
            return EnemyTypes.Big;
        if (Utils.FloatBetween(0, chamberNumber*4 / MaxNumber) > 0.5)
            return EnemyTypes.Medium;
        return EnemyTypes.Small;
    }
}
