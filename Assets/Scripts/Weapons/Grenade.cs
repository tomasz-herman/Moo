using Assets.Scripts.SoundManager;
using Assets.Scripts.Weapons;
using UnityEngine;

public class Grenade : ProjectileBase
{
    [SerializeField] private ExplosionParticles explosionParticles;
    public float Emission = 6;

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
                Volume = SoundTypeSettings.GetVolumeForSoundType(SoundType.GrenadeExplosion)
            }
        };

        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.SetColor("_EmissiveColor", color * Emission);
        material.SetColor("_BaseColor", color);
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (gameObject.transform.localScale.x > explosionRange)
            Destroy(gameObject);

        if (isExplosing)
            gameObject.transform.localScale += explosionSpeed * Time.deltaTime * Vector3.one;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Owner)
        {
            if (!isExplosing)
            {
                isExplosing = true;
                var rigidbody = GetComponent<Rigidbody>();
                var backtrackedPosition = transform.position - rigidbody.velocity * 2f * Time.fixedDeltaTime;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                PlaySound();

                transform.position = backtrackedPosition;
                var particles = Instantiate(explosionParticles, transform.position, transform.rotation);
                particles.Color = color;
                gameObject.GetComponentInChildren<Renderer>().enabled = false;
            }

            var distance = Vector3.Distance(gameObject.transform.position, other.transform.position);
            var dmg = damage / (1 + damageDecay * distance / explosionRange); //damage is higher near the explosion
            ApplyDamage(other, dmg);
        }
    }

    private void OnDestroy()
    {
        Audio?.Dispose();
    }

    public void Launch(GameObject owner, Vector3 velocity, float extradamage)
    {
        Launch(owner, extradamage);
        GetComponent<Rigidbody>().velocity = velocity;
        gameObject.transform.LookAt(transform.position + velocity);
    }
}
