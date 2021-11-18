using Assets.Scripts.Util;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Blade bladePrefab;

    private CircularList<Weapon> weapons = new CircularList<Weapon>();

    public float projectileSpeed = 3f;
    public float triggerTimeout = 0.5f;

    private ContinuousTrigger trigger = new ContinuousTrigger();
    public AmmoSystem ammoSystem;

    void Start()
    {
        weapons.Add(new Pistol(projectilePrefab));
        weapons.Add(new Shotgun(projectilePrefab));
        weapons.Add(new Sword(bladePrefab));
    }
    void Update()
    {
        weapons.Current().DecreaseTime();
    }
    public void NextWeapon() => weapons.Next();
    public void PrevWeapon() => weapons.Prev();
    public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction)
    {
        if (ammoSystem.Ammo == 0)
            return;
        weapons.Current().TryShoot(shooter, position, direction, this, ammoSystem);
    }
}
