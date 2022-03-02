using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        var upgrades = upgradeSystem.GetUpgrades()
            .Select(c => new { c.type, c.count, color = c.type.GetColor() })
            .OrderBy(c => c.type);
        foreach (var upgrade in upgrades)
        {
            upgradeList.AddEntry(upgrade.type.GetName(), $"x{upgrade.count}", upgrade.color);
        }
    }
}
