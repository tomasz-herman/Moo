using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : Entity
{
    [HideInInspector] public float remainingHealth;
    public GameObject FloatingTextPrefab;
    private new Camera camera;
    
    private void Start()
    {
        camera = Camera.main;
        transform.Rotate(0, Utils.FloatBetween(0, 360), 0);
    }

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            var playerCapacity = player.HealthSystem.MaxHealth - player.HealthSystem.Health;
            if(playerCapacity <= 0) return;
            if (playerCapacity >= remainingHealth)
            {
                int displayedValue = Mathf.CeilToInt(remainingHealth);
                ShowText("+" + displayedValue);

                player.HealthSystem.Health += remainingHealth;
                Destroy(gameObject);
            }
            else
            {
                int displayedValue = Mathf.CeilToInt(playerCapacity);
                ShowText("+" + displayedValue);

                player.HealthSystem.Health += playerCapacity;
                remainingHealth -= playerCapacity;
            }
        }
    }

    private void ShowText(string text)
    {
        if (FloatingTextPrefab)
        {
            var textObject = Instantiate(FloatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
            textObject.transform.LookAt(camera.transform.position);
            textObject.transform.Rotate(Vector3.up, 180);
            textObject.transform.GetComponentInChildren<TextMeshPro>().color = Color.green;
            textObject.transform.GetComponentInChildren<TextMeshPro>().SetText(text);
            Destroy(textObject, 3);
        }
    }
}
