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

    private CircularList<Weapon> weapons = new CircularList<Weapon>();

    public float projectileSpeed = 3f;
    public float triggerTimeout = 0.5f;

    private ContinuousTrigger trigger = new ContinuousTrigger();
    public AmmoSystem ammoSystem;

    void Start()
    {
        weapons.Add(new Pistol(projectilePrefab));
        weapons.Add(new Shotgun(projectilePrefab));
        weapons.Add(new MachineGun(projectilePrefab));
        weapons.Add(new GrenadeLauncher(grenadePrefab));
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
        weapons.Current().TryShoot(shooter, position, direction, this, ammoSystem);
    }
}
