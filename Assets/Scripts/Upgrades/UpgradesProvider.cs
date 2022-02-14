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
            new MaxHealthUpgrade(),
            new MaxAmmoUpgrade(),
            new MovementSpeedUpgrade(),

            //Weapon damage upgrades
            new PistolDamageUpgrade(),
            new ShotgunDamageUpgrade(),
            new MachineGunDamageUpgrade(),
            new GrenadeLauncherDamageUpgrade(),
            new SwordDamageUpgrade(),

            //Weapon projectile speed upgrades
            new PistolProjectileSpeedUpgrade(),
            new ShotgunProjectileSpeedUpgrade(),
            new MachineGunProjectileSpeedUpgrade(),
            new GrenadeLauncherProjectileSpeedUpgrade(),
            new SwordProjectileSpeedUpgrade(),

            //Weapon cooldown upgrades
            new PistolCooldownUpgrade(),
            new ShotgunCooldownUpgrade(),
            new MachineGunCooldownUpgrade(),
            new GrenadeLauncherCooldownUpgrade(),
            new SwordCooldownUpgrade(),

            //Weapon ammo cost upgrades
            new PistolAmmoCostUpgrade(),
            new ShotgunAmmoCostUpgrade(),
            new MachineGunAmmoCostUpgrade(),
            new GrenadeLauncherAmmoCostUpgrade(),

            //Shotgun projectile count upgrade
            new ShotgunProjectileCountUpgrade(),

            //Shotgun projectile dispersion upgrade
            new ShotgunProjectileDispersionUpgrade(),
            
            //One time upgrades

            //Sword reflect enemy projectiles upgrade
            new SwordReflectsEnemyProjectilesUpgrade(),

            //Projectiles explode after hitting enemy upgrades
            //new PistolProjectilesExplodeAfterHittingEnemyUpgrade(shootingSystem.Pistol, projectilesExplodeAfterHittingEnemyIcon),
            //new ShotgunProjectilesExplodeAfterHittingEnemyUpgrade(shootingSystem.Shotgun, projectilesExplodeAfterHittingEnemyIcon),
            //new MachineGunProjectilesExplodeAfterHittingEnemyUpgrade(shootingSystem.MachineGun, projectilesExplodeAfterHittingEnemyIcon),

            //Projectiles are chained to nearest enemy upgrades
            new PistolProjectilesChainToNearestEnemyUpgrade(),
            new ShotgunProjectilesChainToNearestEnemyUpgrade(),
            new MachineGunProjectilesChainToNearestEnemyUpgrade(),
            new GrenadeLauncherProjectilesChainToNearestEnemyUpgrade(),
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
