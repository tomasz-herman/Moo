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

    //TODO remove when proper upgrade class is defined
    private UpgradeView upgrade1, upgrade2, upgrade3;
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

    public void Upgrade(int index)
    {
        //TODO change all of this when proper upgrade system is done
        UpgradeType upgrade;
        if (index == 0)
        {
            player.healthSystem.MaxHealth += 100;
            upgrade = UpgradeType.TestUpgradeHealth;
        }  
        else if (index == 1)
        {
            player.ammoSystem.MaxAmmo += 50;
            player.ammoSystem.Ammo += 100;
            upgrade = UpgradeType.TestUpgradeAmmo;
        }
        else
        {
            player.scoreSystem.AddScore(Utils.NumberBetween(1, 10));
            upgrade = UpgradeType.TestUpgradeScore;
        }

        if (upgrades.ContainsKey(upgrade))
            upgrades[upgrade]++;
        else
            upgrades[upgrade] = 1;
        Upgraded?.Invoke(this, (upgrade, upgrades[upgrade]));

        GenerateRandomUpgrades();
    }

    private void GenerateRandomUpgrades()
    {
        //TODO change when proper upgrade system is implemented
        var words = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.".Split(' ');

        upgrade1 = new UpgradeView($"Name {Utils.NumberBetween(101, 999)}", string.Join(" ", words.OrderBy(w => Utils.FloatBetween(0, 1))), upgradeImages[Utils.NumberBetween(0, upgradeImages.Length - 1)]);
        upgrade2 = new UpgradeView($"Name {Utils.NumberBetween(101, 999)}", string.Join(" ", words.OrderBy(w => Utils.FloatBetween(0, 1))), upgradeImages[Utils.NumberBetween(0, upgradeImages.Length - 1)]);
        upgrade3 = new UpgradeView($"Name {Utils.NumberBetween(101, 999)}", string.Join(" ", words.OrderBy(w => Utils.FloatBetween(0, 1))), upgradeImages[Utils.NumberBetween(0, upgradeImages.Length - 1)]);
    }

    public UpgradeView[] GetNextUpgrades()
    {
        //TODO change when proper upgrade system is implemented
        return new UpgradeView[] { upgrade1, upgrade2, upgrade3 };
    }

    public IEnumerable<(UpgradeType type, int count)> GetUpgrades()
    {
        return upgrades.Select(stat => (stat.Key, stat.Value));
    }
}
