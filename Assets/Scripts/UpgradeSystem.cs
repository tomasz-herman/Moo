using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public UpgradeWindow upgradeWindow;
    private int pendingUpgrades = 0;
    public Player player;

    public void AddUpgrade()
    {
        pendingUpgrades++;
        upgradeWindow.Recalculate();
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
        if (index == 0)
            player.healthSystem.MaxHealth += 20;
        else if (index == 1)
        {
            player.ammoSystem.MaxAmmo += 50;
            player.ammoSystem.Ammo += 100;
        }
        else
            player.scoreSystem.AddScore(Utils.NumberBetween(1, 10));
    }
}
