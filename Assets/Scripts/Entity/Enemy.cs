using Assets.Scripts.SoundManager;
using UnityEngine;

public class Enemy : Entity
{
    public GameObject deathSummon;
    public GameObject dropItem;
    public float dropChance = 0.5f;
    public Vector3 summonPos1, summonPos2;
    public int pointsForKill = 1;

    public HealthSystem healthSystem;

    public UnityEngine.Events.UnityEvent KillEvent;

    public SoundTypeWithPlaybackSettings Sound;

    [HideInInspector]
    public Audio Audio;

    private AudioManager _audioManager;

    void Awake()
    {
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
        if (deathSummon != null)
            Instantiate(deathSummon, new Vector3(Utils.FloatBetween(summonPos1.x, summonPos2.x),
                Utils.FloatBetween(summonPos1.y, summonPos2.y), Utils.FloatBetween(summonPos1.z, summonPos2.z)), Quaternion.identity);
        if (dropItem != null)
        {
            if (Utils.FloatBetween(0, 1) <= dropChance)
                Instantiate(dropItem, transform.position, transform.rotation);
        }
        Destroy(gameObject);

        //TODO: delete this when vignette use case is implemented
        FindObjectOfType<DamagePostProcessing>().ApplyVignette();

        KillEvent.Invoke();
    }
}
