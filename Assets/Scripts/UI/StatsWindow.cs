using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWindow : GuiWindow
{
    public StatisticsSystem statSystem;
    public StatEntryList statList;

    public UpgradeSystem upgradeSystem;
    public StatEntryList upgradeList;

    public override void Awake()
    {
        base.Awake();
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
            statList.AddEntry(stat.type.GetName(), $"{stat.value:0.##}", stat.type.GetColor());
        }
    }
    public void RecalculateUpgrades()
    {
        upgradeList.Clear();
        foreach(var upgrade in upgradeSystem.GetUpgrades())
        {
            upgradeList.AddEntry(upgrade.type.GetName(), $"x{upgrade.count}", upgrade.type.GetColor());
        }
    }
}
