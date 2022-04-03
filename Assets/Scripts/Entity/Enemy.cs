using Assets.Scripts.SoundManager;
using System;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent<bool> EnabledEvent { get; private set; } = new UnityEvent<bool>();

    private int level = 1;
    private bool isDead = false;
    private bool started = false;

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

        healthSystem.defaultHealth = data.BaseHealth;
    }

    public void Start()
    {
        started = true;
        _audioManager = AudioManager.Instance;
        Audio = _audioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, transform);
        Spawn();
    }

    void OnDestroy()
    {
        Audio?.Dispose();
    }

    private void RecalculateStatistics()
    {
        var gameplay = ApplicationData.GameplayData;

        float healthFactor = gameplay.GetEnemyHealthScalingMultiplier(level);
        healthSystem.MaxHealth = data.BaseHealth * healthFactor;

        foreach(WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
        {
            var weapon = shooting[weaponType];
            weapon.damageMultiplier = gameplay.GetEnemyDamageScalingMultiplier(level);
            weapon.projectileSpeedMultiplier = gameplay.GetEnemyProjectileSpeedScalingMultiplier(level);
            weapon.triggerTimeoutMultiplier = gameplay.GetEnemyTriggerTimeoutScalingMultiplier(level);
        }

        movementSpeed = data.BaseMovementSpeed * gameplay.GetEnemyMovementSpeedScalingMultiplier(level);
        pointsForKill = EnemyType.GetPointsForKill(level);

        dropSystem.healthDropChance = data.HealthDropChance;
        dropSystem.minHealth = data.BaseMinHealthDrop * healthFactor;
        dropSystem.maxHealth = data.BaseMaxHealthDrop * healthFactor;

        float ammoFactor = gameplay.GetEnemyAmmoScalingMultiplier(level);
        dropSystem.ammoDropChance = data.AmmoDropChance;
        dropSystem.minAmmo = data.BaseMinAmmoDrop * ammoFactor;
        dropSystem.maxAmmo = data.BaseMaxAmmoDrop * ammoFactor;

        dropSystem.upgradeDropChance = data.UpgradeDropChance;
        dropSystem.upgradeDropCount = data.UpgradeDropCount;
    }

    public void Spawn()
    {
        if (!started || isDead)
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

    public void Kill(ScoreSystem system = null)
    {
        if (isDead)
            return;
        healthSystem.Health = 0;
        Audio.PlayOneShot();
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

    public void OnEnable()
    {
        EnabledEvent.Invoke(true);
    }

    public void OnDisable()
    {
        EnabledEvent.Invoke(false);
    }
}
