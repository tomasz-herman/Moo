using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamagePostProcessing : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;
    public float vignetteIntensity = 0.5f;
    public float vignetteTime = 1f;

    private float remainingTime = 0f;

    void Start()
    {
        volume.profile.TryGetSettings(out vignette);
    }

    void Update()
    {
        vignette.intensity.value = vignetteIntensity * remainingTime / vignetteTime;
        remainingTime = Mathf.Max(remainingTime - Time.deltaTime, 0);
    }

    public void ApplyVignette()
    {
        remainingTime = vignetteTime;
    }
}
