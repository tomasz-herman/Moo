using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class DropSystem : MonoBehaviour
{
    [SerializeField] Ammo ammoPrefab;
    [SerializeField] Health healthPrefab;
    [SerializeField] Upgrade dropItem;

    [SerializeField] float dropUpgradeChance;
    [SerializeField] float dropAmmoChance;
    [SerializeField] float dropHealthChance;
    public void Drop()
    {
        var rand = Utils.FloatBetween(0, 1);
        if (rand <= dropUpgradeChance)
            Instantiate(dropItem, transform.position, transform.rotation);
        else if (rand <= dropUpgradeChance + dropAmmoChance)
            Instantiate(ammoPrefab, transform.position + Vector3.up / 2, transform.rotation);
        else if (rand <= dropUpgradeChance + dropAmmoChance + dropHealthChance)
            Instantiate(healthPrefab, transform.position + Vector3.up / 2, transform.rotation);
    }
}

