using Assets.Scripts.SoundManager;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public abstract class Enemy : Entity
{
    public EnemyData data;

    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public DropSystem dropSystem;

    [HideInInspector] public UnityEngine.Events.UnityEvent<GameObject> KillEvent;

    [HideInInspector] public Shooting shooting;
    [HideInInspector] public float movementSpeed;
    [HideInInspector] public float pointsForKill;
    public UnityEvent<bool> EnabledEvent { get; private set; } = new UnityEvent<bool>();

    Dissolve dissolve;

    private int level = 1;
    [HideInInspector] public bool isDead = false;
    private bool started = false;

    public AudioSourcePrefab AudioSourcePrefab;

    //Death sound
    [HideInInspector]
    public SoundTypeWithPlaybackSettings DeathSound;
    protected AudioSourcePrefab DeathAudioSourceInstance;

    //Random enemy hit sound player
    [HideInInspector] public RandomSoundPlayer RandomEnemyHurtSoundPlayer;

    void Awake()
    {
        dissolve = GetComponent<Dissolve>();
        dropSystem = GetComponent<DropSystem>();
        healthSystem = GetComponent<HealthSystem>();
        shooting = GetComponent<Shooting>();

        var deathSoundType = SoundHelpers.GetRandomEnemyDeathSound();
        this.DeathSound = new SoundTypeWithPlaybackSettings
        {
            SoundType = deathSoundType,
            PlaybackSettings = SoundHelpers.GetEnemyDeathPlaybackSettings(deathSoundType)
        };

        data = ApplicationData.EnemyData[EnemyType];

        healthSystem.defaultHealth = data.BaseHealth;
    }

    public void Start()
    {
        RandomEnemyHurtSoundPlayer = GetComponentInChildren<RandomSoundPlayer>();
        RandomEnemyHurtSoundPlayer.minSoundIntervalSeconds = 1;
        RandomEnemyHurtSoundPlayer.maxSoundIntervalSeconds = 2;
        FillRandomEnemyHurtSoundPlayer();

        started = true;

        if (AudioSourcePrefab != null)
        {
            this.DeathAudioSourceInstance = Object.Instantiate(AudioSourcePrefab, transform.position, Quaternion.identity);
            this.DeathAudioSourceInstance.InitializeSound(this.DeathSound);
            this.DeathAudioSourceInstance.Owner = gameObject;
        }

        Spawn();
    }

    private void RecalculateStatistics()
    {
        var gameplay = ApplicationData.GameplayData;

        float healthFactor = gameplay.GetEnemyHealthScalingMultiplier(level);
        healthSystem.MaxHealth = data.BaseHealth * healthFactor;

        foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
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
        if (healthSystem.Health <= 0)
        {
            Die(system);
            this.DeathAudioSourceInstance?.Play();
        }
        else
        {
            this.RandomEnemyHurtSoundPlayer?.PlayNextSound();
        }
    }

    public void Kill(ScoreSystem system = null)
    {
        if (isDead)
            return;
        healthSystem.Health = 0;
        Die(system);
        //there is no need to play sound there, because if I understand it correctly this method is called when parent of enemy dies
        //so a lot sounds would be played at the same time
    }

    private void Die(ScoreSystem system = null)
    {
        isDead = true;
        dissolve.StartDissolve(() =>
        {
            system?.AddScore(pointsForKill);

            //drop loot
            dropSystem.Drop();

            KillEvent.Invoke(gameObject);
            Destroy(gameObject);
        });
    }

    private void FillRandomEnemyHurtSoundPlayer()
    {
        if (RandomEnemyHurtSoundPlayer == null) return;

        if (RandomEnemyHurtSoundPlayer.RandomSoundQueue.Any()) return;

        var randomSoundQueue = SoundHelpers.GetEnemyHurtSoundTypes().Select(x => new SoundTypeWithPlaybackSettings()
        {
            SoundType = x,
            PlaybackSettings = SoundHelpers.GetEnemyHurtPlaybackSettings(x)
        }).ToArray();

        RandomEnemyHurtSoundPlayer.SetRandomSoundQueue(randomSoundQueue);
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
