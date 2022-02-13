using System;
using Assets.Scripts.Upgrades;
using Assets.Scripts.Upgrades.Weapons;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades;
using Assets.Scripts.Upgrades.OneTime.ProjectilesExplodeAfterHittingEnemy.Upgrades;
using Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles.Upgrades;
using UnityEngine;

public class UpgradesProvider : MonoBehaviour
{
    [SerializeField] Sprite maxAmmoIcon;
    [SerializeField] Sprite maxHealthIcon;
    [SerializeField] Sprite movementSpeedIcon;
    [SerializeField] Sprite weaponDamageIcon;
    [SerializeField] Sprite weaponProjectileSpeedIcon;
    [SerializeField] Sprite weaponCooldownIcon;
    [SerializeField] Sprite weaponAmmoCostIcon;
    [SerializeField] Sprite shotgunProjectileCountIcon;
    [SerializeField] Sprite shotgunProjectileDispersionIcon;
    [SerializeField] Sprite swordReflectsEnemyProjectilesIcon;
    [SerializeField] Sprite projectilesExplodeAfterHittingEnemyIcon;
    [SerializeField] Sprite projectilesChainToNearestEnemy;

    private HealthSystem healthSystem;
    private AmmoSystem ammoSystem;
    private PlayerMovement movementSystem;
    private Shooting shootingSystem;

    private UpgradeView[] upgrades;
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        ammoSystem = GetComponent<AmmoSystem>();
        movementSystem = GetComponent<PlayerMovement>();
        shootingSystem = GetComponent<Shooting>();

        var upgradesList = new List<UpgradeView>
        {
            //Player upgrades
            new MaxHealthUpgrade(healthSystem, maxHealthIcon),
            new MaxAmmoUpgrade(ammoSystem, maxAmmoIcon),
            new MovementSpeedUpgrade(movementSystem, movementSpeedIcon),

            //Weapon damage upgrades
            new PistolDamageUpgrade(shootingSystem.Pistol, weaponDamageIcon),
            new ShotgunDamageUpgrade(shootingSystem.Shotgun, weaponDamageIcon),
            new MachineGunDamageUpgrade(shootingSystem.MachineGun, weaponDamageIcon),
            new GrenadeLauncherDamageUpgrade(shootingSystem.GrenadeLauncher, weaponDamageIcon),
            new SwordDamageUpgrade(shootingSystem.Sword, weaponDamageIcon),

            //Weapon projectile speed upgrades
            new PistolProjectileSpeedUpgrade(shootingSystem.Pistol, weaponProjectileSpeedIcon),
            new ShotgunProjectileSpeedUpgrade(shootingSystem.Shotgun, weaponProjectileSpeedIcon),
            new MachineGunProjectileSpeedUpgrade(shootingSystem.MachineGun, weaponProjectileSpeedIcon),
            new GrenadeLauncherProjectileSpeedUpgrade(shootingSystem.GrenadeLauncher, weaponProjectileSpeedIcon),
            new SwordProjectileSpeedUpgrade(shootingSystem.Sword, weaponProjectileSpeedIcon),

            //Weapon cooldown upgrades
            new PistolCooldownUpgrade(shootingSystem.Pistol, weaponCooldownIcon),
            new ShotgunCooldownUpgrade(shootingSystem.Shotgun, weaponCooldownIcon),
            new MachineGunCooldownUpgrade(shootingSystem.MachineGun, weaponCooldownIcon),
            new GrenadeLauncherCooldownUpgrade(shootingSystem.GrenadeLauncher, weaponCooldownIcon),
            new SwordCooldownUpgrade(shootingSystem.Sword, weaponCooldownIcon),

            //Shotgun projectile count upgrade
            new ShotgunProjectileCountUpgrade(shootingSystem.Shotgun, shotgunProjectileCountIcon),

            //Shotgun projectile dispersion upgrade
            new ShotgunProjectileDispersionUpgrade(shootingSystem.Shotgun, shotgunProjectileDispersionIcon),
            //TODO: add here ammo cost upgrades
            //One time upgrades

            //Sword reflect enemy projectiles upgrade
            new SwordReflectsEnemyProjectilesUpgrade(shootingSystem.Sword, swordReflectsEnemyProjectilesIcon),

            //Projectiles explode after hitting enemy upgrades
            //new PistolProjectilesExplodeAfterHittingEnemyUpgrade(shootingSystem.Pistol, projectilesExplodeAfterHittingEnemyIcon),
            //new ShotgunProjectilesExplodeAfterHittingEnemyUpgrade(shootingSystem.Shotgun, projectilesExplodeAfterHittingEnemyIcon),
            //new MachineGunProjectilesExplodeAfterHittingEnemyUpgrade(shootingSystem.MachineGun, projectilesExplodeAfterHittingEnemyIcon),

            //Projectiles are chained to nearest enemy upgrades
            new PistolProjectilesChainToNearestEnemyUpgrade(shootingSystem.Pistol, projectilesChainToNearestEnemy),
            new ShotgunProjectilesChainToNearestEnemyUpgrade(shootingSystem.Shotgun, projectilesChainToNearestEnemy),
            new MachineGunProjectilesChainToNearestEnemyUpgrade(shootingSystem.MachineGun, projectilesChainToNearestEnemy),
            new GrenadeLauncherProjectilesChainToNearestEnemyUpgrade(shootingSystem.GrenadeLauncher, projectilesChainToNearestEnemy)
        };

        try
        {
            _ = upgradesList.ToDictionary(x => x.upgradeType);
        }
        catch (ArgumentException)
        {
            Debug.LogError("Upgrades do not have unique types.");
        }

        if (upgradesList.Count != UpgradeTypeExtensions.AllUpgrades.Length)
        {
            Debug.LogError("Some upgrades are not (or there are too many upgrades) in the upgrades list.");
        }

        //rewrite values into array to improve performance
        upgrades = new UpgradeView[upgradesList.Count];
        foreach (var upgrade in upgradesList)
        {
            upgrades[(int)upgrade.upgradeType] = upgrade;
        }
    }

    public UpgradeView GetUpgrade(UpgradeType type) => upgrades[(int)type];
}
