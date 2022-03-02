using System;
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

        //get colors and sort
        var upgrades = upgradeSystem.GetUpgrades()
            .Select(c => new { c.type, c.count, color = c.type.GetColor() })
            .OrderBy(c => c.type);

        var added = new bool[Enum.GetValues(typeof(UpgradeType)).Length];
        //add collected upgrades
        foreach (var upgrade in upgrades)
        {
            added[(int)upgrade.type] = true;
            upgradeList.AddEntry(upgrade.type.GetName(), $"x{upgrade.count}", upgrade.color);
        }

        //add rest of upgrades
        for (int i = 0; i < added.Length; i++)
        {
            if (added[i]) continue;

            upgradeList.AddEntry(((UpgradeType)i).GetName(), $"x0", Color.gray);
        }
    }
}
