using Assets.Scripts.Upgrades;
using Assets.Scripts.Upgrades.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradesProvider : MonoBehaviour
{
    [SerializeField] Sprite maxAmmoIcon;
    [SerializeField] Sprite maxHealthIcon;
    [SerializeField] Sprite movementSpeedIcon;
    [SerializeField] Sprite weaponDamageIcon;
    [SerializeField] Sprite weaponProjectileSpeedIcon;
    [SerializeField] Sprite weaponCooldownIcon;

    private HealthSystem healthSystem;
    private AmmoSystem ammoSystem;
    private PlayerMovement movementSystem;
    private Shooting shootingSystem;

    private UpgradeView[] upgrades;
    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        ammoSystem = GetComponent<AmmoSystem>();
        movementSystem = GetComponent<PlayerMovement>();
        shootingSystem = GetComponent<Shooting>();

        var dict = new Dictionary<UpgradeType, UpgradeView>();
        dict.Add(UpgradeType.MaxHealth, new MaxHealthUpgrade(healthSystem, maxHealthIcon));
        dict.Add(UpgradeType.MaxAmmo, new MaxAmmoUpgrade(ammoSystem, maxAmmoIcon));
        dict.Add(UpgradeType.MovementSpeed, new MovementSpeedUpgrade(movementSystem, movementSpeedIcon));

        dict.Add(UpgradeType.PistolDamage, new PistolDamageUpgrade(shootingSystem.Pistol, weaponDamageIcon));
        dict.Add(UpgradeType.ShotgunDamage, new ShotgunDamageUpgrade(shootingSystem.Shotgun, weaponDamageIcon));
        dict.Add(UpgradeType.MachineGunDamage, new MachineGunDamageUpgrade(shootingSystem.MachineGun, weaponDamageIcon));
        dict.Add(UpgradeType.GrenadeLauncherDamage, new GrenadeLauncherDamageUpgrade(shootingSystem.GrenadeLauncher, weaponDamageIcon));
        dict.Add(UpgradeType.SwordDamage, new SwordDamageUpgrade(shootingSystem.Sword, weaponDamageIcon));

        dict.Add(UpgradeType.PistolProjectileSpeed, new PistolProjectileSpeedUpgrade(shootingSystem.Pistol, weaponProjectileSpeedIcon));
        dict.Add(UpgradeType.ShotgunProjectileSpeed, new ShotgunProjectileSpeedUpgrade(shootingSystem.Shotgun, weaponProjectileSpeedIcon));
        dict.Add(UpgradeType.MachineGunProjectileSpeed, new MachineGunProjectileSpeedUpgrade(shootingSystem.MachineGun, weaponProjectileSpeedIcon));
        dict.Add(UpgradeType.GrenadeLauncherProjectileSpeed, new GrenadeLauncherProjectileSpeedUpgrade(shootingSystem.GrenadeLauncher, weaponProjectileSpeedIcon));
        dict.Add(UpgradeType.SwordProjectileSpeed, new SwordProjectileSpeedUpgrade(shootingSystem.Sword, weaponProjectileSpeedIcon));

        dict.Add(UpgradeType.PistolCooldown, new PistolCooldownUpgrade(shootingSystem.Pistol, weaponCooldownIcon));
        dict.Add(UpgradeType.ShotgunCooldown, new ShotgunCooldownUpgrade(shootingSystem.Shotgun, weaponCooldownIcon));
        dict.Add(UpgradeType.MachineGunCooldown, new MachineGunCooldownUpgrade(shootingSystem.MachineGun, weaponCooldownIcon));
        dict.Add(UpgradeType.GrenadeLauncherCooldown, new GrenadeLauncherCooldownUpgrade(shootingSystem.GrenadeLauncher, weaponCooldownIcon));
        dict.Add(UpgradeType.SwordCooldown, new SwordCooldownUpgrade(shootingSystem.Sword, weaponCooldownIcon));


        upgrades = new UpgradeView[dict.Count];
        foreach(var pair in dict)
            upgrades[(int)pair.Key] = pair.Value;
    }
    public UpgradeView GetUpgrade(UpgradeType type) => upgrades[(int)type];

}
