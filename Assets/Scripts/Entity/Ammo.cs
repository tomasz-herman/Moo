using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Entity
{
    [SerializeField] int minAmmo;
    [SerializeField] int maxAmmo;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ammoSystem.Ammo += Utils.NumberBetween(minAmmo, maxAmmo);
            Destroy(gameObject);
        }
    }
}
