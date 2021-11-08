using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Image fill;
    public TMP_Text text;
    public AmmoSystem ammoSystem;

    public void Start()
    {
        ammoSystem.AmmoChanged += (sender, args) => UpdateBar();
        UpdateBar();
    }

    public void UpdateBar()
    {
        int ammo = ammoSystem.Ammo;
        int maxAmmo = ammoSystem.MaxAmmo;
        fill.transform.localScale = new Vector3((float)ammo / maxAmmo, 1, 1);
        text.text = $"{ammo}/{maxAmmo}";
    }
}
