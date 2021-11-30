using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWindow : GuiWindow
{
    public StatisticsSystem statSystem;
    public StatEntryList statList;

    public UpgradeSystem upgradeSystem;
    public StatEntryList upgradeList;

    void Start()
    {
        base.Start();
        statSystem.StatisticUpdated += (e, tuple) => RecalculateStatistics();
        upgradeSystem.Upgraded += (e, tuple) => RecalculateUpgrades();
        RecalculateStatistics();
        RecalculateUpgrades();
    }

    public void RecalculateStatistics()
    {
        statList.Clear();
        foreach(var stat in statSystem.GetStatistics())
        {
            statList.AddEntry(stat.type.ToString(), Mathf.CeilToInt(stat.value).ToString());
        }
    }
    public void RecalculateUpgrades()
    {
        upgradeList.Clear();
        foreach(var upgrade in upgradeSystem.GetUpgrades())
        {
            upgradeList.AddEntry(upgrade.type.ToString(), $"x{upgrade.count}");
        }
    }
}
