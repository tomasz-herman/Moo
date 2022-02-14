using System;
using System.Linq;
using UnityEngine;


internal static class UpgradeTypeExtensions
{
    private static readonly UpgradeType FirstOneTimeUpgrade = UpgradeType.SwordReflectsEnemyProjectiles;

    public static readonly UpgradeType[] AllUpgrades;
    public static readonly UpgradeType[] OneTimeUpgrades;

    static UpgradeTypeExtensions()
    {
        AllUpgrades = Enum.GetValues(typeof(UpgradeType)).Cast<UpgradeType>().ToArray();
        OneTimeUpgrades = AllUpgrades.Where(x => (int)x >= (int)FirstOneTimeUpgrade).ToArray();
    }

    public static bool IsOneTimeUpgrade(this UpgradeType upgrade)
    {
        return OneTimeUpgrades.Contains(upgrade);
    }
}

public static class UpgradeIconExtensions
{
    public static Sprite GetSprite(this UpgradeIcon icon)
    {
        return ApplicationData.UpgradeData[icon].image;
    }
}

public static class UpgradeColorExtensions
{
    public static Color GetColor(this UpgradeColor color)
    {
        return color switch
        {
            UpgradeColor.GrenadeLauncher => ApplicationData.WeaponData[WeaponType.GrenadeLauncher].color,
            UpgradeColor.Pistol => ApplicationData.WeaponData[WeaponType.Pistol].color,
            UpgradeColor.MachineGun => ApplicationData.WeaponData[WeaponType.MachineGun].color,
            UpgradeColor.Sword => ApplicationData.WeaponData[WeaponType.Sword].color,
            UpgradeColor.Shotgun => ApplicationData.WeaponData[WeaponType.Shotgun].color,
            UpgradeColor.OneTime => ApplicationData.UpgradeData.OneTimeUpgradeColor,
            _ => Color.white
        };
    }
}
