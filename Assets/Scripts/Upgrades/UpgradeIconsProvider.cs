using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeIconsProvider : MonoBehaviour
{
    [SerializeField] Sprite maxAmmo;
    [SerializeField] Sprite maxDamage;
    [SerializeField] Sprite maxHealth;
    [SerializeField] Sprite movementSpeed;
    [SerializeField] Sprite shootingSpeed;

    private Dictionary<UpgradeType, Sprite> icons = new Dictionary<UpgradeType, Sprite>();
    void Awake()
    {
        icons.Add(UpgradeType.MaxAmmo, maxAmmo);
        icons.Add(UpgradeType.WeaponDamage, maxDamage);
        icons.Add(UpgradeType.MaxHealth, maxHealth);
        icons.Add(UpgradeType.MovementSpeed, movementSpeed);
        icons.Add(UpgradeType.ShootingSpeed, shootingSpeed);
    }

    public Sprite GetIcon(UpgradeType type) => icons[type];
}
