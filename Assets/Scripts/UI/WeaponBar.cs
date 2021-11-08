using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBar : MonoBehaviour
{
    public Image selector;
    public Image slot1, slot2, slot3, slot4, slot5; //we need inspector access
    private Image[] images;

    private int selectedIndex;
    void Start()
    {
        images = new Image[] { slot1, slot2, slot3, slot4, slot5 };
        selectedIndex = images.Length / 2;
        UpdateSelector();
    }

    public void SelectSlot(int slot)
    {
        selectedIndex = slot;
        UpdateSelector();
    }

    public void UpdateSelector()
    {
        selector.transform.position = images[selectedIndex].transform.position;
    }

}
