using System;
using System.Collections.Generic;
using System.Linq;
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

    public float projectileSpeedMultiplier = 1f;
    public float triggerTimeoutMultiplier = 1f;
    public float weaponDamageMultiplier = 1f;

    public AmmoSystem ammoSystem;
    public WeaponBar weaponBar;

    public Pistol Pistol { get; private set; } 
    public Shotgun Shotgun { get; private set; }
    public MachineGun MachineGun { get; private set; }
    public GrenadeLauncher GrenadeLauncher { get; private set; }
    public Sword Sword { get; private set; }

    public event EventHandler<Weapon> WeaponChanged;
    public Weapon CurrentWeapon => weapons.Current();

    private Dictionary<WeaponType, Weapon> weaponMap;

    private GameObject _owner;

    private bool _initializedWeaponOwner;

    private void Awake()
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
        weapons.Sort((x, y) => x.WeaponType.CompareTo(y.WeaponType));
        
        SelectWeapon(WeaponType.Pistol);

        weaponMap = weapons.ToDictionary(w => w.WeaponType);
        if(weaponBar != null) WeaponChanged += (sender, weapon) => weaponBar.SelectSlot(weapon.WeaponType); 
    }

    private void Start()
    {
        //TODO: do not use entity but some object that shows weapon sound position
        this._owner = GetComponent<Entity>().gameObject;

        //there is some bug when player is weapon owners so we use lazy evaluation here
        var player = GetComponent<Player>();
        if (player != null) return;

        this.InitializeWeaponOwners();
    }

    private void Update()
    {
        weapons.Current().DecreaseTime();
    }

    public void SelectWeapon(WeaponType type)
    {
        weapons.SetIndex((int)type);
        WeaponChanged?.Invoke(this, weapons.Current());
    }

    public void NextWeapon()
    {
        weapons.Next();
        WeaponChanged?.Invoke(this, weapons.Current());
    }

    public void PrevWeapon()
    {
        weapons.Prev();
        WeaponChanged?.Invoke(this, weapons.Current());
    }

    public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction)
    {
        if (!_initializedWeaponOwner)
        {
            this.InitializeWeaponOwners();
        }
        weapons.Current().TryShoot(shooter, position + direction, direction, this, ammoSystem);
    }

    public float GetTriggerTimeout(WeaponType type) { return weaponMap[type].basetriggerTimeout * triggerTimeoutMultiplier; }

    public bool HasEnoughAmmo()
    {
        return weapons.Current().HasEnoughAmmo(ammoSystem);
    }

    public Weapon this[WeaponType type] => weaponMap[type];

    private void InitializeWeaponOwners()
    {
        foreach (var weapon in this.weapons)
        {
            weapon.Owner = this._owner;
        }
        _initializedWeaponOwner = true;
    }
}
