using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DamagePostProcessing : MonoBehaviour
{
    private Volume volume;
    [HideInInspector] public HealthSystem healthSystem;
    private Vignette vignette;
    private ColorAdjustments colorGrading;
    public float vignetteIntensity = 0.5f;
    public float vignetteTime = 1f;
    public float desaturateStartFactor = 0.2f;

    private float remainingTime = 0f;
    void Start()
    {
        volume = gameObject.GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out colorGrading);
    }

    void Update()
    {
        vignette.intensity.value = vignetteIntensity * remainingTime / vignetteTime;
        remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0);

        float factor = healthSystem.Health / healthSystem.MaxHealth;
        colorGrading.saturation.value = Mathf.Clamp(100 * (factor / desaturateStartFactor - 1), -100, 0);
    }

    public void ApplyVignette()
    {
        remainingTime = vignetteTime;
    }
}
