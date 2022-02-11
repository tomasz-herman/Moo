using Assets.Scripts.SoundManager;
using UnityEngine;

public abstract class Enemy : Entity
{
    public EnemyData data;

    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public DropSystem dropSystem;
    
    [HideInInspector] public UnityEngine.Events.UnityEvent<GameObject> KillEvent;

    public SoundTypeWithPlaybackSettings Sound;

    [HideInInspector]
    public Audio Audio;

    private AudioManager _audioManager;
    [HideInInspector] public Shooting shooting;
    [HideInInspector] public float movementSpeed;
    [HideInInspector] public float pointsForKill;

    private int level = 1;
    private bool isDead = false;

    void Awake()
    {
        dropSystem = GetComponent<DropSystem>();
        healthSystem = GetComponent<HealthSystem>();
        shooting = GetComponent<Shooting>();
        Sound = new SoundTypeWithPlaybackSettings
        {
            SoundType = SoundType.EnemyKilled,
            PlaybackSettings = new PlaybackSettings
            {
                SpatialBlend = 1f,
                Volume = SoundTypeSettings.GetVolumeForSoundType(SoundType.EnemyKilled)
            }
        };

        data = ApplicationData.EnemyData[EnemyType];
        Spawn();
    }

    void Start()
    {
        _audioManager = AudioManager.Instance;
        Audio = _audioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, transform);
    }

    void OnDestroy()
    {
        Audio?.Dispose();
    }

    private void RecalculateStatistics()
    {
        var gameplay = ApplicationData.GameplayData;

        float healthFactor = gameplay.GetHealthScalingMultiplier(level);
        healthSystem.MaxHealth = data.BaseHealth * healthFactor;

        shooting.weaponDamageMultiplier = data.BaseDamageMultiplier * gameplay.GetDamageScalingMultiplier(level);
        shooting.projectileSpeedMultiplier = data.BaseProjectileSpeedMultiplier * gameplay.GetProjectileSpeedScalingMultiplier(level);
        shooting.triggerTimeoutMultiplier = data.BaseTriggerTimeoutMultiplier * gameplay.GetTriggerTimeoutScalingMultiplier(level);

        movementSpeed = data.BaseMovementSpeed * gameplay.GetMovementSpeedScalingMultiplier(level);
        pointsForKill = data.BaseScoreForKill * gameplay.GetScoreScalingMultiplier(level);

        dropSystem.healthDropChance = data.HealthDropChance;
        dropSystem.minHealth = data.BaseMinHealthDrop * healthFactor;
        dropSystem.maxHealth = data.BaseMaxHealthDrop * healthFactor;

        float ammoFactor = gameplay.GetAmmoScalingMultiplier(level);
        dropSystem.ammoDropChance = data.AmmoDropChance;
        dropSystem.minAmmo = data.BaseMinAmmoDrop * ammoFactor;
        dropSystem.maxAmmo = data.BaseMaxAmmoDrop * ammoFactor;

        dropSystem.upgradeDropChance = data.UpgradeDropChance;
        dropSystem.upgradeDropCount = data.UpgradeDropCount;
    }

    public void Spawn()
    {
        if (isDead)
            return;
        RecalculateStatistics();
        healthSystem.Health = healthSystem.MaxHealth;
    }

    public int Level
    {
        get { return level; }
        set
        {
            if (value < 1)
                return;
            level = value;
            RecalculateStatistics();
        }
    }

    public void TakeDamage(float damage, ScoreSystem system = null)
    {
        if (isDead)
            return;
        healthSystem.Health -= damage;
        Audio.PlayOneShot();
        if (healthSystem.Health <= 0)
            Die(system);
    }

    private void Die(ScoreSystem system = null)
    {
        system?.AddScore(pointsForKill);
    
        //drop loot
        dropSystem.Drop();

        KillEvent.Invoke(gameObject);
        isDead = true;
        Destroy(gameObject);
    }

    public abstract EnemyTypes EnemyType { get; }
}
