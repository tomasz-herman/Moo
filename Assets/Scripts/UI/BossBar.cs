using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] public Image bar, weapon;
    [SerializeField] public TMP_Text text;

    private HealthSystem healthSystem;
    private Shooting shooting;
    private Enemy trackedEnemy;
    private bool visible;

    public bool Visible
    {
        get { return visible; }
        set
        {
            visible = value;
            CalculateVisibility();
        }
    }

    public Enemy TrackedEnemy
    {
        get { return trackedEnemy; }
        set
        {
            if (trackedEnemy != null)
            {
                healthSystem.HealthChanged -= UpdateBar;
                shooting.WeaponChanged -= OnWeaponChange;
                trackedEnemy.KillEvent.RemoveListener(OnKill);
            }
            trackedEnemy = value;
            if (value != null)
            {
                healthSystem = trackedEnemy.healthSystem;
                healthSystem.HealthChanged += UpdateBar;

                shooting = trackedEnemy.GetComponent<Shooting>();
                shooting.WeaponChanged += OnWeaponChange;

                trackedEnemy.KillEvent.AddListener(OnKill);

                UpdateBar(this, (healthSystem.Health, healthSystem.MaxHealth));
                SetWeaponSprite(shooting.CurrentWeapon.WeaponType);
            }
            CalculateVisibility();
        }
    }

    public Color Color { get { return bar.color; } set { bar.color = value; } }

    private void Start()
    {
        Visible = true;
        CalculateVisibility();
    }

    //TODO delete this region when there is a way to access current chamber's boss, preferably with EnterChamberEvent/ExitChamberEvent
    #region DELETE_WHEN_BOSSBAR_IMPLEMENTATION_IS_READY
    private float timeToSwap = 0;
    private void Update()
    {
        if (timeToSwap <= 0)
        {
            //Temporary implementation to give example on how to use it
            var possibleEnemies = FindObjectsOfType<Enemy>();
            TrackedEnemy = possibleEnemies.Length == 0 ? null : possibleEnemies[Utils.NumberBetween(0, possibleEnemies.Length - 1)];
            if(TrackedEnemy != null)
            {
                if (TrackedEnemy.GetComponent<BossEnemyAI>() != null)
                    Color = Color.red;
                else if (TrackedEnemy.GetComponent<SimpleEnemyAI>() != null)
                    Color = Color.grey;
            }
            timeToSwap = 5;
        }
        timeToSwap -= Time.deltaTime;
    }
    #endregion

    private void CalculateVisibility()
    {
        bool show = visible && trackedEnemy != null;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(show);
        }
    }

    private void UpdateBar(object sender, (float health, float maxHealth) args)
    {
        bar.transform.localScale = new Vector3(args.health / args.maxHealth, 1, 1);

        int healthPercent = Mathf.CeilToInt(100 * args.health / args.maxHealth);
        text.text = $"{healthPercent}%";
    }

    private void OnWeaponChange(object sender, Weapon weapon)
    {
        SetWeaponSprite(weapon.WeaponType);
    }

    private void SetWeaponSprite(WeaponType type)
    {
        weapon.sprite = ApplicationData.WeaponData[type].image;
    }

    private void OnKill(GameObject obj)
    {
        TrackedEnemy = null;
    }
}
