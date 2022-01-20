using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponBar : MonoBehaviour
{
    public Image selector;
    public Image overlay;
    public Image slot1, slot2, slot3, slot4, slot5;
    private Image[] images;

    private int slotCount;
    private int selectedIndex;

    public Shooting shooting;
    public float timeout=0;
    public float remainingtime=0;
    public bool over1;
    void Start()
    {
        images = new Image[] { slot1, slot2, slot3, slot4, slot5 };
        slotCount = images.Length;
        selectedIndex = images.Length / 2;
        UpdateSelector();
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
        shooting.Pistol.WeaponShoot += UpdateSprite;
        shooting.Shotgun.WeaponShoot += UpdateSprite;
        shooting.Sword.WeaponShoot += UpdateSprite;
        shooting.GrenadeLauncher.WeaponShoot += UpdateSprite;
        shooting.MachineGun.WeaponShoot += UpdateSprite;
    }
    void Update()
    {

        if (remainingtime < timeout && over1 == true)
        {
            remainingtime += Time.deltaTime;
            //overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1f-remainingtime/timeout);
            overlay.rectTransform.sizeDelta = new Vector2(100, 100 - 100 * (remainingtime / timeout));
            overlay.rectTransform.localPosition = new Vector3(0, -(-50 + (remainingtime / timeout) * 40), 0);
        }
        else
            over1 = false;
            //overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
    }
    void UpdateSprite(object sender, float Timeout)
    {
        remainingtime = 0;
        over1 = true;
        timeout = Timeout*shooting.triggerTimeout;
        overlay.transform.position = images[selectedIndex].transform.position;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.75f);
        
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
