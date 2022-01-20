using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePostProcessing : MonoBehaviour
{
    //public PostProcessVolume volume;
    //public HealthSystem healthSystem;
    //private Vignette vignette;
    //private ColorGrading colorGrading;
    //public float vignetteIntensity = 0.5f;
    //public float vignetteTime = 1f;
    //public float desaturateStartFactor = 0.2f;

    //private float remainingTime = 0f;

    //void Start()
    //{
    //    volume.profile.TryGetSettings(out vignette);
    //    volume.profile.TryGetSettings(out colorGrading);
    //}

    //void Update()
    //{
    //    vignette.intensity.value = vignetteIntensity * remainingTime / vignetteTime;
    //    remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0);

    //    float factor = healthSystem.Health / healthSystem.MaxHealth;
    //    colorGrading.saturation.value = Mathf.Clamp(100 * (factor / desaturateStartFactor - 1), -100, 0);
    //}

    //public void ApplyVignette()
    //{
    //    remainingTime = vignetteTime;
    //}
}
