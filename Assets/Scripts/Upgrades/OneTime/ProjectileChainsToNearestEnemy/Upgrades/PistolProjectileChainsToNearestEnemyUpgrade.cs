using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class PistolProjectileChainsToNearestEnemyUpgrade : ProjectileChainsToNearestEnemyUpgradeBase
    {
        protected PistolProjectileChainsToNearestEnemyUpgrade(Pistol pistol, Sprite sprite, string weaponName)
            : base(pistol, sprite, UpgradeType.PistolProjectileChainsToNearestEnemy) { }

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
