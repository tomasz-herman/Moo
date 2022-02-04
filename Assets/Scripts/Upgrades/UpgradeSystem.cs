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

    private UpgradesProvider upgradesProvider;

    void Awake()
    {
        upgradesProvider = GetComponent<UpgradesProvider>();
    }

    public void AddUpgrade(int upgradeCount = 1)
    {
        bool wasZero = pendingUpgrades == 0;
        pendingUpgrades += upgradeCount;
        upgradeWindow.Recalculate();
        if(wasZero)
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
        var isAdded = new bool[upgradeTypes.Length];

        for (int i = 0; i < 3; i++)
        {
            var index = Utils.NumberBetween(0, upgradeTypes.Length - 1);
            var type = (UpgradeType)upgradeTypes.GetValue(index);
            if (isAdded[index])
            {
                i--;
                continue;
            }

            isAdded[index] = true;
            views[i] = upgradesProvider.GetUpgrade(type);
        }

        return views;
    }

    public IEnumerable<(UpgradeType type, int count)> GetUpgrades()
    {
        return upgrades.Select(stat => (stat.Key, stat.Value));
    }

}
