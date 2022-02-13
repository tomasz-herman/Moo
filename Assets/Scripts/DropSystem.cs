using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropSystem : MonoBehaviour
{
    [SerializeField] Ammo ammoPrefab;
    [SerializeField] Health healthPrefab;
    [SerializeField] Upgrade dropItem;

    private static float minDropSpeed = 3f;
    private static float maxDropSpeed = 6f;

    [HideInInspector] public float upgradeDropCount;
    [HideInInspector] public float upgradeDropChance;
    [HideInInspector] public float healthDropChance;
    [HideInInspector] public float minHealth;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public float ammoDropChance;
    [HideInInspector] public float minAmmo;
    [HideInInspector] public float maxAmmo;

    public void Drop()
    {
        if (Random.value < healthDropChance)
        {
            Health health = Instantiate(healthPrefab, transform.position, transform.rotation);
            health.remainingHealth = Utils.FloatBetween(minHealth, maxHealth);
            Throw(health);
        }
        if (Random.value < ammoDropChance)
        {
            Ammo ammo = Instantiate(ammoPrefab, transform.position, transform.rotation);
            ammo.remainingAmmo = Utils.FloatBetween(minAmmo, maxAmmo);
            Throw(ammo);
        }
        int totalUpgradeCount = 0;
        for (int i = 0; i < upgradeDropCount; i++)
        {
            if (Random.value < upgradeDropChance)
                totalUpgradeCount++;
        }
        if(totalUpgradeCount > 0)
        {
            Upgrade upgrade = Instantiate(dropItem, transform.position, transform.rotation);
            upgrade.upgradeCount = totalUpgradeCount;
        }
    }

    private void Throw(MonoBehaviour obj)
    {
        Vector3 dropDirection = new Vector3(Utils.FloatBetween(-1, 1), Utils.FloatBetween(0, 0.5f), Utils.FloatBetween(-1, 1)).normalized;
        obj.GetComponent<Rigidbody>().velocity = dropDirection * Utils.FloatBetween(minDropSpeed, maxDropSpeed);
    }
}

