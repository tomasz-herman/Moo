using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponBar : MonoBehaviour
{
    public Image selector;
    public Image overlay;
    public Image overlayShotgun;
    public Image overlayMachine;
    public Image overlayLauncher;
    public Image overlaySword;
    [SerializeField] private Image slot1, slot2, slot3, slot4, slot5;
    private Image[] images;

    private WeaponType selectedWeapon = WeaponType.Pistol;

    public Shooting shooting;

    private Dictionary<WeaponType, WeaponInfo> weaponData = new Dictionary<WeaponType, WeaponInfo>();

    private Color overlayOffColor, overlayOnColor;
    void Start()
    {
        foreach(WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            weaponData.Add(type, new WeaponInfo());
        }
        weaponData[WeaponType.MachineGun].image = slot1;
        weaponData[WeaponType.MachineGun].overlay = overlayMachine;
        weaponData[WeaponType.Shotgun].image = slot2;
        weaponData[WeaponType.Shotgun].overlay = overlayShotgun;
        weaponData[WeaponType.Pistol].image = slot3;
        weaponData[WeaponType.Pistol].overlay = overlay;
        weaponData[WeaponType.Sword].image = slot4;
        weaponData[WeaponType.Sword].overlay = overlaySword;
        weaponData[WeaponType.GrenadeLauncher].image = slot5;
        weaponData[WeaponType.GrenadeLauncher].overlay = overlayLauncher;

        overlayOffColor = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
        overlayOnColor = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.2f);

        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            weaponData[type].overlay.color = overlayOffColor;
            shooting[type].WeaponShoot += UpdateSprite;
        }
        setAllwhite();
        UpdateSelector();
    }

    void Update()
    {
        foreach(var weaponInfo in weaponData.Values)
        {
            if(weaponInfo.cooldown)
            {
                weaponInfo.overlay.rectTransform.localScale = new Vector3(1, 1 - (weaponInfo.remainingTime / weaponInfo.timeout), 1);
                weaponInfo.remainingTime += Time.deltaTime;
                if (weaponInfo.remainingTime > weaponInfo.timeout)
                {
                    weaponInfo.overlay.color = overlayOffColor;
                    weaponInfo.cooldown = false;
                }
                    
            }
        }
    }
    void UpdateSprite(object sender, (float Timeout, WeaponType type) args)
    {
        var weaponInfo = weaponData[args.type];
        weaponInfo.overlay.color = overlayOnColor;
        weaponInfo.cooldown = true;
        weaponInfo.remainingTime = 0;
        weaponInfo.timeout = args.Timeout;
    }

    public void SelectSlot(WeaponType type)
    {
        weaponData[selectedWeapon].image.color = Color.white;
        selectedWeapon = type;
        UpdateSelector();
    }

    public void UpdateSelector()
    {
        var weaponInfo = weaponData[selectedWeapon];
        selector.transform.position = weaponInfo.image.transform.position;
        selector.color = ApplicationData.WeaponData[selectedWeapon].color;
        weaponInfo.image.color = ApplicationData.WeaponData[selectedWeapon].color;
    }

    public void setAllwhite()
    {
        foreach(var weaponInfo in weaponData.Values)
        {
            weaponInfo.image.color = Color.white;
        }
    }

    private class WeaponInfo
    {
        internal float timeout;
        internal float remainingTime;
        internal bool cooldown;
        internal Image image;
        internal Image overlay;
    }

}
