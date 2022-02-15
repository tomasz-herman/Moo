using Assets.Scripts.Upgrades.OneTime.ProjectileChainsToNearestEnemy.Handlers;
using Assets.Scripts.Upgrades.OneTime.SwordReflectsEnemyProjectiles.Handlers;
using Assets.Scripts.Weapons;

public class Player : Entity, IUpgradeable
{
    public Shooting ShootingSystem { get; private set; }
    public PlayerMovement movement;
    public HealthSystem HealthSystem { get; private set; }
    public UpgradeSystem UpgradeSystem { get; private set; }
    public AmmoSystem AmmoSystem { get; private set; }
    public MovementSystem MovementSystem { get { return movement; } }
    public ScoreSystem scoreSystem;
    public float TeleporterScale = 2;

    private DamagePostProcessing damagePostProcessing;
    void Start()
    {
        ShootingSystem = GetComponent<Shooting>();
        movement = GetComponent<PlayerMovement>();
        HealthSystem = GetComponent<HealthSystem>();
        UpgradeSystem = GetComponent<UpgradeSystem>();
        AmmoSystem = GetComponent<AmmoSystem>();
        scoreSystem = GetComponent<ScoreSystem>();

        HealthSystem.HealthChanged += CheckDeath;
        HealthSystem.DamageReceived += OnDamageReceived;
        HealthSystem.defaultHealth = ApplicationData.GameplayData.DefaultPlayerHealth;
        HealthSystem.MaxHealth = HealthSystem.defaultHealth;
        HealthSystem.Health = HealthSystem.MaxHealth;

        damagePostProcessing = FindObjectOfType<DamagePostProcessing>();
        damagePostProcessing.healthSystem = HealthSystem;

        AmmoSystem.defaultCapacity = ApplicationData.GameplayData.DefaultPlayerAmmo;
        AmmoSystem.MaxAmmo = AmmoSystem.defaultCapacity;
        AmmoSystem.Ammo = AmmoSystem.MaxAmmo;
    }

    public void Upgrade(int upgradeCount = 1)
    {
        UpgradeSystem.AddUpgrade(upgradeCount);
    }

    public void CheckDeath(object sender, (float health, float maxHealth) args)
    {
        if(args.health <= 0)
        {
            GameWorld.EndGame(false);
        }
    }

    private void OnDamageReceived(object sender, float damage)
    {
        damagePostProcessing.ApplyVignette();
    }
}
