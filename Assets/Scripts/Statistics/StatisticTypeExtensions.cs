using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;


internal static class StatisticTypeExtensions
{
    public static string GetName(this StatisticType type) => Utils.EnumStringToReadable(type.ToString());
    public static Color GetColor(this StatisticType type)
    {
        var str = type.ToString();

        foreach (var t in Enum.GetValues(typeof(UpgradeColor)).Cast<UpgradeColor>())
            if (str.Contains(t.ToString()))
                return t.GetColor();

        return Color.white;
    }
}
