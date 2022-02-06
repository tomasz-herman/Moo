using Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles.Handlers;
using Assets.Scripts.Weapons;

public class Player : Entity
{
    public Shooting shooting;
    public PlayerMovement movement;
    public HealthSystem healthSystem;
    public UpgradeSystem upgradeSystem;
    public AmmoSystem ammoSystem;
    public ScoreSystem scoreSystem;
    public float TeleporterScale = 2;

    private DamagePostProcessing damagePostProcessing;
    void Start()
    {
        shooting = GetComponent<Shooting>();
        movement = GetComponent<PlayerMovement>();
        healthSystem = GetComponent<HealthSystem>();
        upgradeSystem = GetComponent<UpgradeSystem>();
        ammoSystem = GetComponent<AmmoSystem>();
        scoreSystem = GetComponent<ScoreSystem>();

        healthSystem.HealthChanged += CheckDeath;
        healthSystem.DamageReceived += OnDamageReceived;
        healthSystem.MaxHealth = ApplicationData.GameplayData.DefaultPlayerHealth;
        healthSystem.Health = healthSystem.MaxHealth;

        damagePostProcessing = FindObjectOfType<DamagePostProcessing>();
        damagePostProcessing.healthSystem = healthSystem;

        ammoSystem.MaxAmmo = ApplicationData.GameplayData.DefaultPlayerAmmo;
        ammoSystem.Ammo = ammoSystem.MaxAmmo;

        //TODO: delete (if you see this in PR let MichalR know)
        var currweap = shooting.CurrentWeapon;
        shooting.SelectWeapon(WeaponType.Sword);
        var sword = shooting.CurrentWeapon as Sword;
        var projectileUpgrade = new SwordReflectsEnemyProjectilesUpgradeHandler(sword);

        sword?.BladeUpgrades.Add(projectileUpgrade);
        shooting.SelectWeapon(currweap.WeaponType);
    }

    public void Upgrade(int upgradeCount = 1)
    {
        upgradeSystem.AddUpgrade(upgradeCount);
    }

    public void CheckDeath(object sender, (float health, float maxHealth) args)
    {
        if(args.health <= 0)
        {
            GameWorld.EndGame(false, scoreSystem.GetScore());
        }
    }

    private void OnDamageReceived(object sender, float damage)
    {
        damagePostProcessing.ApplyVignette();
    }
}
