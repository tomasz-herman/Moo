using System;
using Assets.Scripts.Util;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Blade bladePrefab;
    public Grenade grenadePrefab;
    public Bullet bulletPrefab;

    private CircularList<Weapon> weapons = new CircularList<Weapon>();

    public float projectileSpeed = 3f;
    public float triggerTimeout = 0.5f;
    public float weaponDamage = 1f;

    public AmmoSystem ammoSystem;
    public WeaponBar weaponBar;

    public Pistol Pistol { get; private set; } 
    public Shotgun Shotgun { get; private set; }
    public MachineGun MachineGun { get; private set; }
    public GrenadeLauncher GrenadeLauncher { get; private set; }
    public Sword Sword { get; private set; }

    void Awake()
    {
        Pistol = new Pistol(projectilePrefab);
        Shotgun = new Shotgun(bulletPrefab);
        MachineGun = new MachineGun(projectilePrefab);
        GrenadeLauncher = new GrenadeLauncher(grenadePrefab);
        Sword = new Sword(bladePrefab);

        weapons.Add(Pistol);
        weapons.Add(Shotgun);
        weapons.Add(MachineGun);
        weapons.Add(GrenadeLauncher);
        weapons.Add(Sword);
    }

    void Update()
    {
        weapons.Current().DecreaseTime();
    }

    public void SelectWeapon(Type type)
    {
        Type first = weapons.Current().GetType();
        while(weapons.Current().GetType() != type)
        {
            if (first == weapons.Next().GetType()) break;
        }
    }

    public void NextWeapon()
    {
        weapons.Next();
        weaponBar.SlotUp();
    }

    public void PrevWeapon()
    {
        weapons.Prev();
        weaponBar.SlotDown();
    }

    public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction)
    {
        weapons.Current().TryShoot(shooter, position + direction, direction, this, ammoSystem);
    }
}
