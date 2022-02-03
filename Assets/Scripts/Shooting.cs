using System;
using Assets.Scripts.Util;
using Assets.Scripts.Weapons;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Blade bladePrefab;
    public Grenade grenadePrefab;
    public Bullet bulletPrefab;

    private CircularList<Weapon> weapons = new CircularList<Weapon>();

    [HideInInspector] public float projectileSpeedMultiplier = 1f;
    public float triggerTimeout = 0.5f;
    [HideInInspector] public float weaponDamageMultiplier = 1f;

    public AmmoSystem ammoSystem;
    public WeaponBar weaponBar;

    public Pistol Pistol => new Pistol(projectilePrefab);
    public Shotgun Shotgun => new Shotgun(bulletPrefab);
    public MachineGun MachineGun => new MachineGun(projectilePrefab);
    public GrenadeLauncher GrenadeLauncher => new GrenadeLauncher(grenadePrefab);
    public Sword Sword => new Sword(bladePrefab);

    public event EventHandler<Weapon> WeaponChanged;
    public Weapon CurrentWeapon { get { return weapons.Current(); } }

    void Awake()
    {
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
        WeaponChanged?.Invoke(this, weapons.Current());
    }

    public void NextWeapon()
    {
        weapons.Next();
        weaponBar.SlotUp();
        WeaponChanged?.Invoke(this, weapons.Current());
    }

    public void PrevWeapon()
    {
        weapons.Prev();
        weaponBar.SlotDown();
        WeaponChanged?.Invoke(this, weapons.Current());
    }

    public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction)
    {
        weapons.Current().TryShoot(shooter, position + direction, direction, this, ammoSystem);
    }
}
