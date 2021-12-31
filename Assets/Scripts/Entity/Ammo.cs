using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Entity
{
    [SerializeField] int minAmmo;
    [SerializeField] int maxAmmo;
    private int remainingAmmo;
    private void Awake()
    {
        remainingAmmo = Utils.NumberBetween(minAmmo, maxAmmo);
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            var playerCapacity = player.ammoSystem.MaxAmmo - player.ammoSystem.Ammo;
            if (playerCapacity > remainingAmmo)
            {
                player.ammoSystem.Ammo += remainingAmmo;
                Destroy(gameObject);
            }
            else
            {
                player.ammoSystem.Ammo += playerCapacity;
                remainingAmmo -= playerCapacity;
            }
        }
    }
}
