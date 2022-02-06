using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class GrenadeLauncherProjectileChainsToNearestEnemyUpgrade : ProjectileChainsToNearestEnemyUpgradeBase
    {
        protected GrenadeLauncherProjectileChainsToNearestEnemyUpgrade(GrenadeLauncher grenadeLauncher, Sprite sprite, string weaponName)
            : base(grenadeLauncher, sprite, UpgradeType.GrenadeLauncherProjectileChainsToNearestEnemy) { }

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
