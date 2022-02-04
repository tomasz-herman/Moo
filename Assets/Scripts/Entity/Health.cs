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
            var playerCapacity = player.healthSystem.MaxHealth - player.healthSystem.Health;
            if(playerCapacity <= 0) return;
            if (playerCapacity > remainingHealth)
            {
                ShowText("+" + (int)remainingHealth);

                player.healthSystem.Health += remainingHealth;
                Destroy(gameObject);
            }
            else
            {
                ShowText("+" + (int)playerCapacity);

                player.healthSystem.Health += playerCapacity;
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
