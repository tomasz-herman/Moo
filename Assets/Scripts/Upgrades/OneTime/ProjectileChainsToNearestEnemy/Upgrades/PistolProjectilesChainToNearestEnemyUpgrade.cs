using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Upgrades
{
    public class PistolProjectilesChainToNearestEnemyUpgrade : ProjectilesChainToNearestEnemyUpgradeBase
    {
        public PistolProjectilesChainToNearestEnemyUpgrade(Pistol pistol, Sprite sprite)
            : base(pistol, sprite, UpgradeType.PistolProjectilesChainToNearestEnemy) { }

        public override UpgradeType CommitUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
