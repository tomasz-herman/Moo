using Assets.Scripts.SoundManager;
using UnityEngine;

public abstract class Enemy : Entity
{
    private EnemyData data;

    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public DropSystem dropSystem;
    
    [HideInInspector] public UnityEngine.Events.UnityEvent<GameObject> KillEvent;

    public SoundTypeWithPlaybackSettings Sound;

    [HideInInspector]
    public Audio Audio;

    private AudioManager _audioManager;
    [HideInInspector] public Shooting shooting;
    [HideInInspector] public float movementSpeed;
    [HideInInspector] public int pointsForKill;

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
        healthSystem.MaxHealth = data.BaseHealth;
        healthSystem.Health = healthSystem.MaxHealth;

        shooting.weaponDamageMultiplier = data.BaseDamageMultiplier;
        shooting.projectileSpeedMultiplier = data.BaseProjectileSpeedMultiplier;

        movementSpeed = data.BaseMovementSpeed;
        pointsForKill = data.BaseScoreForKill;
        
        //TODO setup drops
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

    public void TakeDamage(float damage, ScoreSystem system = null)
    {
        healthSystem.Health -= damage;
        Audio.PlayOneShot();
        if (healthSystem.Health > 0) return;
        Die(system);
    }

    private void Die(ScoreSystem system = null)
    {
        system?.AddScore(pointsForKill);
    
        //drop loot
        dropSystem.Drop();

        KillEvent.Invoke(gameObject);
        Destroy(gameObject);
    }

    public abstract EnemyTypes EnemyType { get; }
}
