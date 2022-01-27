using Assets.Scripts.SoundManager;
using UnityEngine;

public class Enemy : Entity
{
    public int pointsForKill = 1;
    
    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public DropSystem dropSystem;
    
    [HideInInspector] public UnityEngine.Events.UnityEvent<GameObject> KillEvent;

    public SoundTypeWithPlaybackSettings Sound;

    [HideInInspector]
    public Audio Audio;

    private AudioManager _audioManager;

    void Awake()
    {
        dropSystem = GetComponent<DropSystem>();
        Sound = new SoundTypeWithPlaybackSettings
        {
            SoundType = SoundType.EnemyKilled,
            PlaybackSettings = new PlaybackSettings
            {
                SpatialBlend = 1f,
                Volume = SoundTypeSettings.GetVolumeForSoundType(SoundType.EnemyKilled)
            }
        };
    }

    void Start()
    {
        _audioManager = AudioManager.Instance;
        Audio = _audioManager.CreateSound(Sound.SoundType, Sound.PlaybackSettings, transform);
        healthSystem = GetComponent<HealthSystem>();
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
}
