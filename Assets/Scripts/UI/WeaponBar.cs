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
    public Image slot1, slot2, slot3, slot4, slot5;
    private Image[] images;

    private int slotCount;
    private int selectedIndex;

    public Shooting shooting;
    public float PistolTimeout, ShotgunTimeout, MachineTimeout, LauncherTimeout, SwordTimeout = 0;
    public float PistolRemainingTime, ShotgunRemainingTime, MachineRemainingTime, LauncherRemaningTime, SwordRemaningTime = 0;
    public bool PistolCooldown, ShotgunCooldown, MachineCooldown, SwordCooldown, LauncherCooldown = false;
    void Start()
    {
        images = new Image[] { slot1, slot2, slot3, slot4, slot5 };
        slotCount = images.Length;
        selectedIndex = images.Length / 2;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
        overlayShotgun.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
        overlayMachine.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
        overlayLauncher.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
        overlaySword.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
        shooting.Pistol.WeaponShoot += UpdateSprite;
        shooting.Shotgun.WeaponShoot += UpdateSprite;
        shooting.Sword.WeaponShoot += UpdateSprite;
        shooting.GrenadeLauncher.WeaponShoot += UpdateSprite;
        shooting.MachineGun.WeaponShoot += UpdateSprite;
        setAllwhite();
        UpdateSelector();
    }
    void Update()
    {
        if (PistolCooldown)
        {
            overlay.rectTransform.localScale = new Vector3(1, 1 - (PistolRemainingTime / PistolTimeout), 1);
            PistolRemainingTime += Time.deltaTime;
            if (PistolRemainingTime > PistolTimeout)
                PistolCooldown = false;
        }
        if (ShotgunCooldown)
        {
            overlayShotgun.rectTransform.localScale = new Vector3(1, 1 - (ShotgunRemainingTime / ShotgunTimeout), 1);
            ShotgunRemainingTime += Time.deltaTime;
            if (ShotgunRemainingTime > ShotgunTimeout)
                ShotgunCooldown = false;
        }
        if (MachineCooldown)
        {
            overlayMachine.rectTransform.localScale = new Vector3(1, 1 - (MachineRemainingTime / MachineTimeout), 1);
            MachineRemainingTime += Time.deltaTime;
            if (MachineRemainingTime > MachineTimeout)
                MachineCooldown = false;
        }
        if (LauncherCooldown)
        {
            overlayLauncher.rectTransform.localScale = new Vector3(1, 1 - (LauncherRemaningTime / LauncherTimeout), 1);
            LauncherRemaningTime += Time.deltaTime;
            if (LauncherRemaningTime > LauncherTimeout)
                LauncherCooldown = false;
        }
        if (SwordCooldown)
        {
            overlaySword.rectTransform.localScale = new Vector3(1, 1 - (SwordRemaningTime / SwordTimeout), 1);
            SwordRemaningTime += Time.deltaTime;
            if (SwordRemaningTime > SwordTimeout)
                SwordCooldown = false;
        }

    }
    void UpdateSprite(object sender, (float Timeout, WeaponType type) args)
    {
        switch (args.type)
        {
            case WeaponType.Pistol:
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.2f);
                PistolCooldown = true;
                PistolRemainingTime = 0;
                PistolTimeout = args.Timeout;
                break;
            case WeaponType.Shotgun:
                overlayShotgun.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.2f);
                ShotgunCooldown = true;
                ShotgunRemainingTime = 0;
                ShotgunTimeout = args.Timeout;
                break;
            case WeaponType.MachineGun:
                overlayMachine.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.2f);
                MachineCooldown = true;
                MachineRemainingTime = 0;
                MachineTimeout = args.Timeout;
                break;
            case WeaponType.GrenadeLauncher:
                overlayLauncher.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.2f);
                LauncherCooldown = true;
                LauncherRemaningTime = 0;
                LauncherTimeout = args.Timeout;
                break;
            case WeaponType.Sword:
                overlaySword.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.2f);
                SwordCooldown = true;
                SwordRemaningTime = 0;
                SwordTimeout = args.Timeout;
                break;
        }


    }

    public void SelectSlot(int slot)
    {
        images[selectedIndex].color = Color.white;
        selectedIndex = slot;
        UpdateSelector();
    }

    public void SlotDown() { SelectSlot((selectedIndex + 1) % slotCount); }

    public void SlotUp() { SelectSlot((selectedIndex + slotCount - 1) % slotCount); }

    public void UpdateSelector()
    {
        selector.transform.position = images[selectedIndex].transform.position;
        selector.color = ApplicationData.WeaponData[shooting.CurrentWeapon.WeaponType].color;
        images[selectedIndex].color = ApplicationData.WeaponData[shooting.CurrentWeapon.WeaponType].color;
    }

    public void setAllwhite()
    {
        for (int i = 0; i < 5; i++)
        {
            images[i].color = Color.white;
        }
    }

}
