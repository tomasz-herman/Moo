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
    public float PistolTimeout, ShotgunTimeout, MachineTimeout, LauncherTimeout, SwordTimeout=0;
    public float PistolRemainingTime, ShotgunRemainingTime, MachineRemainingTime, LauncherRemaningTime, SwordRemaningTime = 0;
    public bool PistolCooldown, ShotgunCooldown, MachineCooldown, SwordCooldown, LauncherCooldown = false;
    void Start()
    {
        images = new Image[] { slot1, slot2, slot3, slot4, slot5 };
        slotCount = images.Length;
        selectedIndex = images.Length / 2;
        UpdateSelector();
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
    }
    void Update()
    {
        if(PistolCooldown)
        {
            overlay.rectTransform.sizeDelta = new Vector2(100, 100 - 100 * (PistolRemainingTime / PistolTimeout));
            //overlay.rectTransform.localPosition = new Vector3(0, -(-50 + (PistolRemainingTime / PistolTimeout) * 40), 0);
            PistolRemainingTime += Time.deltaTime;
            if (PistolRemainingTime > PistolTimeout)
                PistolCooldown = false;
        }
        if (ShotgunCooldown)
        {
            overlayShotgun.rectTransform.sizeDelta = new Vector2(100, 100 - 100 * (ShotgunRemainingTime / ShotgunTimeout));
            //overlayShotgun.rectTransform.localPosition = new Vector3(-100, -(-50 + (ShotgunRemainingTime / ShotgunTimeout) * 40), 0);
            ShotgunRemainingTime += Time.deltaTime;
            if (ShotgunRemainingTime > ShotgunTimeout)
                ShotgunCooldown = false;
        }
        if (MachineCooldown)
        {
            overlayMachine.rectTransform.sizeDelta = new Vector2(100, 100 - 100 * (MachineRemainingTime / MachineTimeout));
            //overlayMachine.rectTransform.localPosition = new Vector3(-200, -(-50 + (MachineRemainingTime / MachineTimeout) * 40), 0);
            MachineRemainingTime += Time.deltaTime;
            if (MachineRemainingTime > MachineTimeout)
                MachineCooldown = false;
        }
        if (LauncherCooldown)
        {
            overlayLauncher.rectTransform.sizeDelta = new Vector2(100, 100 - 100 * (LauncherRemaningTime / LauncherTimeout));
            //overlayLauncher.rectTransform.localPosition = new Vector3(200, -(-50 + (LauncherRemaningTime / LauncherTimeout) * 40), 0);
            LauncherRemaningTime += Time.deltaTime;
            if (LauncherRemaningTime > LauncherTimeout)
                LauncherCooldown = false;
        }
        if (SwordCooldown)
        {
            overlaySword.rectTransform.sizeDelta = new Vector2(100, 100 - 100 * (SwordRemaningTime / SwordTimeout));
            //overlaySword.rectTransform.localPosition = new Vector3(100, -(-50 + (SwordRemaningTime / SwordTimeout) * 40), 0);
            SwordRemaningTime += Time.deltaTime;
            if (SwordRemaningTime > SwordTimeout)
                SwordCooldown = false;
        }

    }
    void UpdateSprite(object sender, (float Timeout, WeaponType weaponType) args)
    {
        
        
        switch (args.weaponType)
        {
            case WeaponType.Pistol:
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.75f);
                PistolCooldown = true;
                PistolRemainingTime = 0;
                PistolTimeout = args.Timeout * shooting.Pistol.triggerTimeout;
                break;
            case WeaponType.Shotgun:
                overlayShotgun.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.75f);
                ShotgunCooldown = true;
                ShotgunRemainingTime = 0;
                ShotgunTimeout = args.Timeout * shooting.Shotgun.triggerTimeout;
                break;
            case WeaponType.MachineGun:
                overlayMachine.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.75f);
                MachineCooldown = true;
                MachineRemainingTime = 0;
                MachineTimeout = args.Timeout * shooting.MachineGun.triggerTimeout;
                break;
            case WeaponType.GrenadeLauncher:
                overlayLauncher.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.75f);
                LauncherCooldown = true;
                LauncherRemaningTime = 0;
                LauncherTimeout = args.Timeout * shooting.GrenadeLauncher.triggerTimeout;
                break;
            case WeaponType.Sword:
                overlaySword.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.75f);
                SwordCooldown = true;
                SwordRemaningTime = 0;
                SwordTimeout = args.Timeout * shooting.Sword.triggerTimeout;
                break;
        }
        
        
    }

    public void SelectSlot(int slot)
    {
        selectedIndex = slot;
        UpdateSelector();
    }

    public void SlotDown() { SelectSlot((selectedIndex + 1) % slotCount); }

    public void SlotUp() { SelectSlot((selectedIndex + slotCount - 1) % slotCount); }

    public void UpdateSelector()
    {
        selector.transform.position = images[selectedIndex].transform.position;
    }

}
