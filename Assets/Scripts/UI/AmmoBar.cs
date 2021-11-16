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
        ammoSystem.AmmoChanged += UpdateBar;
        UpdateBar(this, (ammoSystem.Ammo, ammoSystem.MaxAmmo));
    }

    public void UpdateBar(object sender, (int ammo, int maxAmmo) args)
    {
        fill.transform.localScale = new Vector3((float)args.ammo / args.maxAmmo, 1, 1);
        text.text = $"{args.ammo}/{args.maxAmmo}";
    }
}
