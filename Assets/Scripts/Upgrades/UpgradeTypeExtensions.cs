using System;
using System.Linq;

namespace Assets.Scripts.Upgrades
{
    internal static class UpgradeTypeExtensions
    {
        private static readonly UpgradeType FirstOneTimeUpgrade = UpgradeType.SwordReflectsEnemyProjectiles;

        public static readonly UpgradeType[] AllUpgrades;
        public static readonly UpgradeType[] OneTimeUpgrades;

        static UpgradeTypeExtensions()
        {
            AllUpgrades = Enum.GetValues(typeof(UpgradeType)).Cast<UpgradeType>().ToArray();
            OneTimeUpgrades = AllUpgrades.Where(x => (int)x >= (int)FirstOneTimeUpgrade).ToArray();
        }

        public static bool IsOneTimeUpgrade(this UpgradeType upgrade)
        {
            return OneTimeUpgrades.Contains(upgrade);
        }
    }
}
