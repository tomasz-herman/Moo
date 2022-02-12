using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ExplosionParticles : MonoBehaviour
{
    [SerializeField] private float baseIntensity;
    [SerializeField] private float lightTimeToLive = 1;
    [SerializeField] private float fullBrightnessTimeFactor = 0.1f;
    [SerializeField] private float intensityGamma = 10;
    private new Light light;
    private float elapsedTime = 0;
    private float fullBrightnessTimeOffset;

    private void Start()
    {
        light = GetComponentInChildren<Light>();
        SetLightIntensity(0);
        elapsedTime = 0;
        fullBrightnessTimeOffset = fullBrightnessTimeFactor * lightTimeToLive;
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }

    private void Update()
    {
        //TODO
        //logarithmic intensity
        //explosion position
        //light color
        elapsedTime += Time.deltaTime;
        float intensityFactor;
        if (elapsedTime < fullBrightnessTimeOffset)
            intensityFactor = elapsedTime / fullBrightnessTimeOffset;
        else
            intensityFactor = Mathf.Max(0, 1 - (elapsedTime - fullBrightnessTimeOffset) / (lightTimeToLive - fullBrightnessTimeOffset));

        //scale 0 -> (x^0 - 1)/(x-1) = 0; 1 -> (x ^ 1 - 1)/(x-1) = 1
        float logIntensity = (Mathf.Pow(intensityGamma, intensityFactor) - 1)/(intensityGamma - 1);

        SetLightIntensity(baseIntensity * logIntensity);
    }

    private void SetLightIntensity(float intensity)
    {
        light.GetComponent<HDAdditionalLightData>().intensity = intensity;
    }
}
