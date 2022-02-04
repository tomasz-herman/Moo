using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstParticles : MonoBehaviour
{
    protected virtual void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
