using Assets.Scripts.SoundManager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField] private ExplosionParticles explosionParticles;

    private float explosionRange = 10f;
    private int explosionRayCount = 1000;
    private float damageDecay = 7f; // how many times damage drops on the edge of explosionRange

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner) return;
        if (nonCollidableObjects.Contains(other.gameObject)) return;

        var enemy = other.gameObject.GetComponent<Enemy>();
        if (Owner != null && Owner.GetComponent<Player>() != null && enemy != null) // Player was shooting and enemy was hit
        {
            for (int i = 0; i < projectileUpgrades.Count; i++)
            {
                projectileUpgrades[i].OnEnemyHit(this, enemy, projectileUpgradesData[i]);
            }
        }

        var rigidbody = GetComponent<Rigidbody>();
        var backtrackedPosition = transform.position - rigidbody.velocity * 2f * Time.fixedDeltaTime;
        rigidbody.velocity = Vector3.zero;
        transform.position = backtrackedPosition;
        PlaySound();

        var particles = Instantiate(explosionParticles, transform.position, transform.rotation);
        particles.Color = color;
        gameObject.GetComponentInChildren<Renderer>().enabled = false;

        int layer = gameObject.layer;
        int mask = 0;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(layer, i))
                mask |= (1 << i);
        }

        Dictionary<Collider, float> hitColliders = new Dictionary<Collider, float>();
        for (int i = 0; i < explosionRayCount; i++)
        {
            Vector3 direction = new Vector3(Utils.FloatBetween(-1, 1), Utils.FloatBetween(-1, 1), Utils.FloatBetween(-1, 1));
            foreach (var hit in Physics.RaycastAll(backtrackedPosition, direction, explosionRange, mask).OrderBy(ray => ray.distance))
            {
                Collider collider = hit.collider;
                if (Layers.TerrainLayers.Contains(collider.gameObject.layer))
                    break;
                float distance = hit.distance;
                if (hitColliders.ContainsKey(collider))
                    distance = Mathf.Min(distance, hitColliders[collider]);
                hitColliders[collider] = distance;
            }
        }
        foreach (var data in hitColliders)
        {
            var dmg = damage / (1 + damageDecay * data.Value / explosionRange); //damage is higher near the explosion
            ApplyDamage(data.Key, dmg);
        }
        Destroy(gameObject);
    }
}
