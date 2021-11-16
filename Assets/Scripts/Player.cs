using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Shooting shooting;
    private PlayerMovement movement;
    public HealthSystem healthSystem;
    public UpgradeSystem upgradeSystem;
    public AmmoSystem ammoSystem;
    public ScoreSystem scoreSystem;
    void Start()
    {
        shooting = GetComponent<Shooting>();
        movement = GetComponent<PlayerMovement>();
        healthSystem = GetComponent<HealthSystem>();
        upgradeSystem = GetComponent<UpgradeSystem>();
        ammoSystem = GetComponent<AmmoSystem>();
        scoreSystem = GetComponent<ScoreSystem>();
    }

    public void Upgrade()
    {
        upgradeSystem.AddUpgrade();
    }
}
