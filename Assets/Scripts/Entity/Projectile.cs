using Assets.Scripts.Weapons;
using UnityEngine;

public class Projectile : ProjectileBase
{
    [SerializeField] private Particles hitTerrainParticles;
    public Color color;
    public float Emission = 6;

    protected override float baseDamage => 10f;
    private float extraDamage = 0;

    protected override void Start()
    {
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.SetColor("_EmissiveColor", color*Emission);
        material.SetColor("_BaseColor", color);
        base.Start();
    }

    public void Launch(GameObject owner, Vector3 velocity, float extradamage)
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
            ApplyDamage(other, baseDamage * extraDamage);

            //TODO: Uncomment when chambers' terrain has proper layering
            //if (Layers.TerrainLayers.Contains(other.gameObject.layer))
                Instantiate(hitTerrainParticles, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
