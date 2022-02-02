using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionParticles : MonoBehaviour
{
    void Start()
    {
        float timeToLive = GetComponentsInChildren<ParticleSystem>().Max(ps => ps.main.duration);
        Destroy(gameObject, timeToLive);
    }
}
