using System;
using System.Linq;

namespace Assets.Scripts.Upgrades
{
    internal static class UpgradeTypeExtensions
    {
        private static readonly UpgradeType FirstOneTimeUpgrade = UpgradeType.SwordReflectsEnemyProjectiles;

        public static readonly UpgradeType[] OneTimeUpgrades;

        static UpgradeTypeExtensions()
        {
            var upgrades = Enum.GetValues(typeof(UpgradeType)).Cast<UpgradeType>();
            OneTimeUpgrades = upgrades.Where(x => (int)x >= (int)FirstOneTimeUpgrade).ToArray();
        }

        public static bool IsOneTimeUpgrade(this UpgradeType upgrade)
        {
            return OneTimeUpgrades.Contains(upgrade);
        }
    }
}
