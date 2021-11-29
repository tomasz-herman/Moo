using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public HealthSystem healthSystem;

    public Slider healthSlider;

    private new Camera camera;

    private void Start()
    {
        healthSystem.HealthChanged += UpdateSlider; 
        healthSlider.value = 1;
        camera = Camera.main;
    }

    private void Update()
    {
        healthSlider.transform.LookAt(camera.transform.position);
    }

    private void UpdateSlider(object sender, (float health, float maxHealth) e)
    {
        healthSlider.value = e.health / e.maxHealth;
    }
}
