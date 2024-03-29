using Assets.Scripts.SoundManager;
using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField] private ExplosionParticles explosionParticles;

    private float explosionSpeed = 40f;
    private float explosionRange = 10f;
    private float damageDecay = 7f; // how many times damage drops on the edge of explosionRange
    private bool isExplosing = false;

    protected override void Start()
    {
        //TODO: move this to inspector when weapon will allow that
        Sound = new SoundTypeWithPlaybackSettings
        {
            SoundType = SoundType.GrenadeExplosion,
            PlaybackSettings = new PlaybackSettings
            {
                SpatialBlend = 1f,
                Volume = SoundHelpers.GetVolumeForSoundType(SoundType.GrenadeExplosion)
            }
        };

        if (AudioSourcePrefab != null)
        {
            AudioSourceInstance = Object.Instantiate(AudioSourcePrefab, transform.position, Quaternion.identity);
            AudioSourceInstance.InitializeSound(Sound);
            AudioSourceInstance.Owner = gameObject;
        }

        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (gameObject.transform.localScale.x > explosionRange)
            Destroy(gameObject);

        if (isExplosing)
            gameObject.transform.localScale += explosionSpeed * Time.deltaTime * Vector3.one;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner) return;
        if (nonCollidableObjects.Contains(other.gameObject)) return;

        if (!isExplosing)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (Owner != null && Owner.GetComponent<Player>() != null && enemy != null) // Player was shooting and enemy was hit
            {
                for (int i = 0; i < projectileUpgrades.Count; i++)
                {
                    projectileUpgrades[i].OnEnemyHit(this, enemy, projectileUpgradesData[i]);
                }
            }

            isExplosing = true;
            var rigidbody = GetComponent<Rigidbody>();
            var backtrackedPosition = transform.position - rigidbody.velocity * 2f * Time.fixedDeltaTime;
            rigidbody.velocity = Vector3.zero;
            transform.position = backtrackedPosition;
            PlaySound();

            var particles = Instantiate(explosionParticles, transform.position, transform.rotation);
            particles.Color = color;
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
        }

        var distance = Vector3.Distance(gameObject.transform.position, other.transform.position);
        var dmg = damage / (1 + damageDecay * distance / explosionRange); //damage is higher near the explosion
        ApplyDamage(other, dmg);
    }
}
