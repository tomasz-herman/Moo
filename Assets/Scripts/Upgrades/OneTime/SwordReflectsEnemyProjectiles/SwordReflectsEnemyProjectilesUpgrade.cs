using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Upgrades.OneTime.Handlers;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles
{
    public class SwordReflectsEnemyProjectilesUpgrade : UpgradeView
    {
        private readonly Shooting _shootingSystem;
        public SwordReflectsEnemyProjectilesUpgrade(Shooting shootingSystem, Sprite sprite)
            : base("Lightsaber", "Sword reflects enemy projectiles.", sprite)
        {
            _shootingSystem = shootingSystem;
        }

        public override UpgradeType CommitUpdate()
        {
            var projectileUpgrade = new SwordReflectsEnemyProjectilesUpgradeHandler(_shootingSystem.Sword);
            //_shootingSystem.Sword.bladePrefab.ProjectileUpgrades ??= new List<IOneTimeProjectileUpgradeHandler>();
            if (!_shootingSystem.Sword.BladeUpgrades.OfType<SwordReflectsEnemyProjectilesUpgradeHandler>().Any())
            {
                _shootingSystem.Sword.BladeUpgrades.Add(projectileUpgrade);
            }

            return UpgradeType.SwordReflectsEnemyProjectiles;
        }
    }
}

/*
interface IUpgradeHandler
{
    OnEnemyHit(ProjectileBase projectile, Enemy enemy);
    OnTerrainHit(GameObject projectile, Collider terrain);
    OnUpdate(ProjectileBase projectile);
    OnLaunch(ProjectileBase projectile);
    // Jakieś inne potrzebne metody?
}

public abstract class ProjectileBase : Entity
{
    public List<IUpgradeHandler> upgradeHandlers = new List<IUpgradeHandler>();

    // inne pola

    protected override void Update()
    {
        // to co było

        foreach (IUpgradeHandler handler : upgradeHandlers) handler.OnUpdate(this);
    }

    public void ApplyDamage(Collider other, float damage)
    {
        Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
        Player playerHit = other.gameObject.GetComponent<Player>();
        if (Owner != null && Owner.GetComponent<Player>() != null) // Player was shooting
        {
            if (enemyHit != null)
            {
                enemyHit.TakeDamage(damage, Owner.GetComponent<ScoreSystem>());
                foreach (IUpgradeHandler handler : upgradeHandlers) handler.OnEnemyHit(this, enemyHit);

            }
        }
        if (Owner == null || (Owner != null && Owner.GetComponent<Enemy>() != null)) // Enemy was shooting (if it is null it means it is dead enemy)
        {
            if (playerHit != null)
            {
                playerHit.healthSystem.Health -= damage;
            }
        }

        if //(/*Jakieś wykrywanie czy trafiło w teren/)
        {
            foreach (IUpgradeHandler handler : upgradeHandlers) handler.OnTerrainHit(this, other);
        }
    }

    // Tą metodę może tu dodać bo jest w granade i bullet:
    public void Launch(GameObject owner, Vector3 velocity, float extradamage)
    {
        foreach (IUpgradeHandler handler : upgradeHandlers) handler.OnLaunch(this);
    }
}
*/