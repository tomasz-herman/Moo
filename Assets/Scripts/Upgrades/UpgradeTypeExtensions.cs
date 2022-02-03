using System.Linq;

namespace Assets.Scripts.Upgrades
{
    internal static class UpgradeTypeExtensions
    {
        public static readonly UpgradeType[] OneTimeUpgrades =
        {
            UpgradeType.SwordReflectsEnemyProjectiles,
            UpgradeType.ProjectilesExplodeAfterHittingEnemy,
            UpgradeType.ProjectileChainsToNearestEnemy
        };

        public static bool IsOneTimeUpgrade(this UpgradeType upgrade)
        {
            return OneTimeUpgrades.Contains(upgrade);
        }
    }
}
