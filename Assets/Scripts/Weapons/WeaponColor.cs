using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Util;
using Assets.Scripts.Weapons;

public class WeaponColor : MonoBehaviour
{
    [SerializeField] private Material material;

    public Shooting shooting;
    void Start()
    {
        shooting = GetComponent<Shooting>();
        material.color = Color.red;
        shooting.WeaponChanged += UpdateColor;
    }

    void UpdateColor(object sender, Weapon br)
    {
        material.color = br.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
