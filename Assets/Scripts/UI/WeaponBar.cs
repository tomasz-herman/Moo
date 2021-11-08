using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBar : MonoBehaviour
{
    public Image selector;
    public Image slot1, slot2, slot3, slot4, slot5; //we need inspector access
    private Image[] images;

    private int slotCount;
    private int selectedIndex;
    void Start()
    {
        images = new Image[] { slot1, slot2, slot3, slot4, slot5 };
        slotCount = images.Length;
        selectedIndex = images.Length / 2;
        UpdateSelector();
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
