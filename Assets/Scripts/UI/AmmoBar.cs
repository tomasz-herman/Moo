using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Image fill;
    public TMP_Text text;
    public int maxAmmo = 100;
    public int ammo = 50;

    public void Start()
    {
        UpdateBar();
    }

    public void SetMaxAmmo(int value)
    {
        maxAmmo = value;
        UpdateBar();
    }
    public void SetHealth(int value)
    {
        ammo = value;
        UpdateBar();
    }

    private void UpdateBar()
    {
        fill.transform.localScale = new Vector3(1, (float)ammo / maxAmmo, 1);
        text.text = $"{ammo}/{maxAmmo}";
    }
}
