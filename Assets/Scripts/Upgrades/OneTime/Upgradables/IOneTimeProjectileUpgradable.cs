using System.Collections.Generic;
using Assets.Scripts.Upgrades.OneTime.Handlers;

namespace Assets.Scripts.Upgrades.OneTime.Upgradables
{
    internal interface IOneTimeProjectileUpgradable
    {
        List<IOneTimeProjectileUpgradeHandler> ProjectileUpgrades { get; set; }
    }
}
