using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionParticles : MonoBehaviour
{
    [SerializeField] private float lightTimeToLive = 1;
    [SerializeField] private float fullBrightnessFactor = 0.5f;
    private new Light light;
    private float elapsedTime = 0;
    private float baseIntensity;
    private float intensityDecreaseStartTime;
    void Start()
    {
        light = GetComponentInChildren<Light>();
        baseIntensity = light.intensity;
        elapsedTime = 0;
        intensityDecreaseStartTime = fullBrightnessFactor * lightTimeToLive;
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > intensityDecreaseStartTime)
        {
            float intensityFactor = (lightTimeToLive - (elapsedTime - intensityDecreaseStartTime)) / (lightTimeToLive - intensityDecreaseStartTime);
            light.intensity = Mathf.Max(0, baseIntensity * intensityFactor);
        }
    }
}
