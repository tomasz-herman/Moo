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

    [SerializeField] int drops = 1;

    public void Drop()
    {
        for (int i = 0; i < drops; i++)
        {
            var rand = Utils.FloatBetween(0, 1);
            if (rand <= dropUpgradeChance)
                Instantiate(dropItem, transform.position, transform.rotation);
            else if (rand <= dropUpgradeChance + dropAmmoChance)
                Instantiate(ammoPrefab, transform.position, transform.rotation);
            else if (rand <= dropUpgradeChance + dropAmmoChance + dropHealthChance)
                Instantiate(healthPrefab, transform.position, transform.rotation);
        }
        
    }
}

