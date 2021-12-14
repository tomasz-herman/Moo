using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Entity
{
    [SerializeField] int minHealth;
    [SerializeField] int maxHealth;
    private float remainingHealth;
    private void Awake()
    {
        remainingHealth = Utils.NumberBetween(minHealth, maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            var playerCapacity = player.healthSystem.MaxHealth - player.healthSystem.Health;
            if (playerCapacity > remainingHealth)
            {
                player.healthSystem.Health += remainingHealth;
                Destroy(gameObject);
            }
            else
            {
                player.healthSystem.Health += playerCapacity;
                remainingHealth -= playerCapacity;
            }
        }
    }
}
