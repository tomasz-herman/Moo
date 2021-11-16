using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public int defaultAmmo = 69;
    public int defaultCapacity = 420;
    private int ammo, maxAmmo;
    public int Ammo
    {
        get { return ammo; }
        set
        {
            ammo = value;
            if (ammo > maxAmmo)
                ammo = maxAmmo;
            AmmoChanged?.Invoke(this, (ammo, maxAmmo));
        }
    }
    public int MaxAmmo
    {
        get { return maxAmmo; }
        set
        {
            maxAmmo = value;
            AmmoChanged?.Invoke(this, (ammo, maxAmmo));
        }
    }

    public EventHandler<(int ammo, int maxAmmo)> AmmoChanged;

    void Start()
    {
        ammo = defaultAmmo;
        maxAmmo = defaultCapacity;
    }
}
