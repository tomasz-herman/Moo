using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Upgrades;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private readonly int CommonUpgradeCountOnPage = 3;

    public UpgradeWindow upgradeWindow;
    private int pendingUpgrades = 0;
    public Player player;

    private Dictionary<UpgradeType, int> upgrades = new Dictionary<UpgradeType, int>();
    public event EventHandler<(UpgradeType type, int Count)> Upgraded;

    public UpgradesProvider upgradesProvider;

    private Dictionary<UpgradeType, bool> _oneTimeUpgradesUsed;

    void Awake()
    {
        upgradesProvider = GetComponent<UpgradesProvider>();
        _oneTimeUpgradesUsed = UpgradeTypeExtensions.OneTimeUpgrades.ToDictionary(x => x, x => false);
    }

    public void AddUpgrade(int upgradeCount = 1)
    {
        bool wasZero = pendingUpgrades == 0;
        pendingUpgrades += upgradeCount;
        if(wasZero)
            upgradeWindow.Open();
        upgradeWindow.Recalculate();
    }

    public void RemoveUpgrade()
    {
        pendingUpgrades--;
        upgradeWindow.Recalculate();
    }

    public int GetUpgradeCount(UpgradeType type)
    {
        if (upgrades.TryGetValue(type, out int count))
            return count;
        return 0;
    }

    public int GetPendingUpgrades() { return pendingUpgrades; }

    public void Upgrade(UpgradeView upgrade)
    {
        var upgradeType = upgrade.upgradeType;
        if (upgrades.ContainsKey(upgradeType))
            upgrades[upgradeType]++;
        else
            upgrades[upgradeType] = 1;
        upgrade.OnUpgraded(player);
        if (upgradeType.IsOneTimeUpgrade())
        {
            _oneTimeUpgradesUsed[upgradeType] = true;
        }
        Upgraded?.Invoke(this, (upgradeType, upgrades[upgradeType]));
    }

    public UpgradeView[] GenerateRandomUpgrades()
    {
        var views = new UpgradeView[CommonUpgradeCountOnPage];

        var upgradeTypes = Enum.GetValues(typeof(UpgradeType));
        var isAdded = new bool[upgradeTypes.Length];

        for (int i = 0; i < CommonUpgradeCountOnPage; i++)
        {
            var index = Utils.NumberBetween(0, upgradeTypes.Length - 1);

            if (isAdded[index])
            {
                i--;
                continue;
            }

            isAdded[index] = true;
            var type = (UpgradeType)upgradeTypes.GetValue(index);

            if (type.IsOneTimeUpgrade() && _oneTimeUpgradesUsed[type])
            {
                i--;
                continue;
            }

            views[i] = upgradesProvider.GetUpgrade(type);
        }

        return views;
    }

    public IEnumerable<(UpgradeType type, int count)> GetUpgrades()
    {
        return upgrades.Select(stat => (stat.Key, stat.Value));
    }

}
