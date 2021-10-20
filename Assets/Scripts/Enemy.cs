using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathSummon;
    public Vector3 summonPos1, summonPos2;
    public int pointsForKill = 1;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GetKilled(ScoreSystem system = null)
    {
        system?.AddScore(pointsForKill);
        if (deathSummon != null)
            Instantiate(deathSummon, new Vector3(Utils.FloatBetween(summonPos1.x, summonPos2.x),
                Utils.FloatBetween(summonPos1.y, summonPos2.y), Utils.FloatBetween(summonPos1.z, summonPos2.z)), Quaternion.identity);
        Destroy(gameObject);
    }
}
