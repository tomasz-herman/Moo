using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstParticles : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
