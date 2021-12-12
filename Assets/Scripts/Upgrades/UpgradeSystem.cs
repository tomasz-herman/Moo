using Assets.Scripts.Upgrades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public UpgradeWindow upgradeWindow;
    private int pendingUpgrades = 0;
    public Player player;

    private Dictionary<UpgradeType, int> upgrades = new Dictionary<UpgradeType, int>();
    public event EventHandler<(UpgradeType type, int Count)> Upgraded;

    private UpgradeIconsProvider iconProvider;

    void Awake()
    {
        iconProvider = GetComponent<UpgradeIconsProvider>();
    }

    public void AddUpgrade()
    {
        pendingUpgrades++;
        upgradeWindow.Recalculate();
        if(pendingUpgrades == 1)
            upgradeWindow.Open();
    }

    public void RemoveUpgrade()
    {
        pendingUpgrades--;
        upgradeWindow.Recalculate();
    }

    public int GetPendingUpgrades() { return pendingUpgrades; }

    public void Upgrade(UpgradeView upgrade)
    {
        //TODO: ammo
        var upgradetype = upgrade.CommitUpdate();

        if (upgrades.ContainsKey(upgradetype))
            upgrades[upgradetype]++;
        else
            upgrades[upgradetype] = 1;
        Upgraded?.Invoke(this, (upgradetype, upgrades[upgradetype]));
    }

    public UpgradeView[] GenerateRandomUpgrades()
    {
        var views = new UpgradeView[3];

        var upgradeTypes = Enum.GetValues(typeof(UpgradeType));

        //TODO: random different upgrades
        for (int i = 0; i < 3; i++)
        {
            var type = (UpgradeType)upgradeTypes.GetValue(Utils.NumberBetween(0, upgradeTypes.Length - 1));
            views[i] = GetUpdateView(type);
        }

        return views;
    }

    public IEnumerable<(UpgradeType type, int count)> GetUpgrades()
    {
        return upgrades.Select(stat => (stat.Key, stat.Value));
    }

    private UpgradeView GetUpdateView(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.MaxHealth:
                return new MaxHealthUpgrade(player, iconProvider.GetIcon(UpgradeType.MaxHealth));
            case UpgradeType.MaxAmmo:
                return new MaxAmmoUpgrade(player, iconProvider.GetIcon(UpgradeType.MaxAmmo));
            case UpgradeType.MovementSpeed:
                return new MovementSpeedUpdate(player, iconProvider.GetIcon(UpgradeType.MovementSpeed));
            case UpgradeType.ShootingSpeed:
                return new ShootingSpeedUpdate(player, iconProvider.GetIcon(UpgradeType.ShootingSpeed));
            case UpgradeType.WeaponDamage:
                return new WeaponDamageUpgrade(player, iconProvider.GetIcon(UpgradeType.WeaponDamage));
            default:
                throw new Exception("Upgrade type not implemented");
        }
    }
}
