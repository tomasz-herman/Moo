using System;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public float defaultAmmo = 1000;
    public float defaultCapacity = 1000;
    private float ammo, maxAmmo;
    public float Ammo
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
    public float MaxAmmo
    {
        get { return maxAmmo; }
        set
        {
            maxAmmo = value;
            AmmoChanged?.Invoke(this, (ammo, maxAmmo));
        }
    }

    public EventHandler<(float ammo, float maxAmmo)> AmmoChanged;

    void Awake()
    {
        ammo = defaultAmmo;
        maxAmmo = defaultCapacity;
    }
}
