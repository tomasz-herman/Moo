using UnityEngine;
using Assets.Scripts.Weapons;

public class WeaponColor : MonoBehaviour
{
    [SerializeField] private Material material;

    public Shooting shooting;
    public float colorIntensity = 10;
    private static readonly int EmissiveColor = Shader.PropertyToID("_EmissiveColor");

    void Start()
    {
        shooting = GetComponent<Shooting>();
        shooting.WeaponChanged += UpdateColor;
        UpdateColor(this, shooting.CurrentWeapon);
    }

    void UpdateColor(object sender, Weapon weapon)
    {
        material.color = weapon.color;
        material.SetColor(EmissiveColor, colorIntensity * weapon.color);
    }
}
