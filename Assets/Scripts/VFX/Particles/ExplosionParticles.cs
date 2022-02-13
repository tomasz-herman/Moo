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
    [SerializeField] private GameObject singleColorParticleParent;
    [SerializeField] private ProjectileHitTerrainParticles terrainParticles;

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
        elapsedTime += Time.deltaTime;
        float intensityFactor;
        if (elapsedTime < fullBrightnessTimeOffset)
            intensityFactor = elapsedTime / fullBrightnessTimeOffset;
        else
            intensityFactor = Mathf.Max(0, 1 - (elapsedTime - fullBrightnessTimeOffset) / (lightTimeToLive - fullBrightnessTimeOffset));

        float logIntensity = Mathf.Pow(intensityFactor, intensityGamma);

        SetLightIntensity(baseIntensity * logIntensity);
    }

    public Color Color
    {
        set
        {
            terrainParticles.SparkColor = value;
            var particles = singleColorParticleParent.GetComponentsInChildren<ParticleSystem>()
                .Concat(new ParticleSystem[] { singleColorParticleParent.GetComponent<ParticleSystem>() });
            foreach (var particleSystem in particles)
            {
                var gradient = new ParticleSystem.MinMaxGradient(value);
                var main = particleSystem.main;
                main.startColor = gradient;
            }
        }
    }

    private void SetLightIntensity(float intensity)
    {
        light.GetComponent<HDAdditionalLightData>().intensity = intensity;
    }
}
