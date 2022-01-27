using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : Entity
{
    [SerializeField] int minAmmo;
    [SerializeField] int maxAmmo;
    private int remainingAmmo;
    public GameObject FloatingTextPrefab;
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
        transform.Rotate(0, Utils.FloatBetween(0, 360), 0);
    }
    
    private void Awake()
    {
        remainingAmmo = Utils.NumberBetween(minAmmo, maxAmmo);
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
            var playerCapacity = player.ammoSystem.MaxAmmo - player.ammoSystem.Ammo;
            if(playerCapacity <= 0) return;
            if (playerCapacity > remainingAmmo)
            {
                ShowText("+" + remainingAmmo);

                player.ammoSystem.Ammo += remainingAmmo;
                Destroy(gameObject);
            }
            else
            {
                ShowText("+" + playerCapacity);
                player.ammoSystem.Ammo += playerCapacity;
                remainingAmmo -= playerCapacity;
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
            textObject.transform.GetComponentInChildren<TextMeshPro>().color = Color.cyan;
            textObject.transform.GetComponentInChildren<TextMeshPro>().SetText(text);
            Destroy(textObject, 3);
        }
    }
}
