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
                Volume = SoundTypeSettings.GetVolumeForSoundType(SoundType.GrenadeExplosion)
            }
        };

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
        if (other.gameObject != Owner)
        {
            if (!isExplosing)
            {
                isExplosing = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                PlaySound();
                
                Instantiate(explosionParticles, transform.position, transform.rotation);
                gameObject.GetComponentInChildren<Renderer>().enabled = false;
            }

            var distance = Vector3.Distance(gameObject.transform.position, other.transform.position);
            var dmg = damage / (1 + damageDecay * distance / explosionRange); //damage is higher near the explosion
            ApplyDamage(other, dmg);
        }
    }
}
