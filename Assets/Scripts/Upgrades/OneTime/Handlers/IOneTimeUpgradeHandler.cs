using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.Handlers
{
    internal interface IOneTimeUpgradeHandler
    {
        void OnEnemyHit(ProjectileBase projectile, Enemy enemy);
        void OnTerrainHit(GameObject projectile, Collider terrain);
        void OnUpdate(ProjectileBase projectile);
        void OnLaunch(ProjectileBase projectile);
    }
}