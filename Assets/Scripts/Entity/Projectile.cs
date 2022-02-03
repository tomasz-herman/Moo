using Assets.Scripts.Weapons;
using UnityEngine;

public class Projectile : ProjectileBase
{
    [SerializeField] private ProjectileHitTerrainParticles hitTerrainParticles;
    [SerializeField] private int particleCount = 30;
    public Color color;
    public float Emission = 6;

    protected override float baseDamage => 10f;
    protected float extraDamage = 0;

    protected override void Start()
    {
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.SetColor("_EmissiveColor", color*Emission);
        material.SetColor("_BaseColor", color);
        base.Start();
    }

    public virtual void Launch(GameObject owner, Vector3 velocity, float extradamage)
    {
        this.Owner = owner;
        extraDamage = extradamage;
        GetComponent<Rigidbody>().velocity = velocity;
        gameObject.transform.LookAt(transform.position + velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Owner)
        {
            ApplyDamage(other, CalculateDamage(other));

            //TODO: Uncomment when chambers' terrain has proper layering
            //if (Layers.TerrainLayers.Contains(other.gameObject.layer))
            {
                var particles = Instantiate(hitTerrainParticles, transform.position, transform.rotation);
                particles.SparkColor = color;
                particles.ParticleCount = particleCount;
            }
                

            Destroy(gameObject);
        }
    }

    protected virtual float CalculateDamage(Collider other)
    {
        return baseDamage * extraDamage;
    }
}
