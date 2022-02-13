using Assets.Scripts.Weapons;
using UnityEngine;

public class Projectile : ProjectileBase
{
    public float Emission = 6;

    protected override void Start()
    {
        base.Start();
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.SetColor("_EmissiveColor", color*Emission);
        material.SetColor("_BaseColor", color);
    }

    public virtual void Launch(GameObject owner, Vector3 velocity, float damage)
    {
        Launch(owner, damage);
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
                SpawnParticles(transform.position);
            }
                

            Destroy(gameObject);
        }
    }

    protected virtual float CalculateDamage(Collider other) { return damage; }
}
