using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitTerrainParticles : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
