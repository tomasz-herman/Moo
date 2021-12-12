using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Entity
{
    [SerializeField] int minHealth;
    [SerializeField] int maxHealth;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.healthSystem.Health += Utils.NumberBetween(minHealth, maxHealth);
            Destroy(gameObject);
        }
    }
}
