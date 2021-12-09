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

    public Sprite[] upgradeImages;

    void Start()
    {
        GenerateRandomUpgrades();
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
                return new MaxHealthUpgrade(player, upgradeImages[Utils.NumberBetween(0, upgradeImages.Length - 1)]);
            case UpgradeType.MaxAmmo:
                return new MaxAmmoUpgrade(player, upgradeImages[Utils.NumberBetween(0, upgradeImages.Length - 1)]);
            case UpgradeType.Score:
                return new AddScoreUpgrade(player, upgradeImages[Utils.NumberBetween(0, upgradeImages.Length - 1)]);
            case UpgradeType.MovementSpeed:
                return new MovementSpeedUpdate(player, upgradeImages[Utils.NumberBetween(0, upgradeImages.Length - 1)]);
            default:
                throw new Exception("Upgrade type not implemented");
        }
    }
}
