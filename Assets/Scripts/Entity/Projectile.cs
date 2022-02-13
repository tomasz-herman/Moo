using Assets.Scripts.Weapons;
using UnityEngine;

public class Projectile : ProjectileBase
{
    public float Emission = 6;

    protected override void Start()
    {
        base.Start();
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.SetColor("_EmissiveColor", color * Emission);
        material.SetColor("_BaseColor", color);
    }

    public virtual void Launch(GameObject owner, Vector3 velocity, float damage)
    {
        Launch(owner, damage);
        GetComponent<Rigidbody>().velocity = velocity;
        gameObject.transform.LookAt(transform.position + velocity);

        for (int i = 0; i < projectileUpgrades.Count; i++)
        {
            projectileUpgrades[i].OnLaunch(this, projectileUpgradesData[i]);
        }
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

        ApplyDamage(other, CalculateDamage(other));

        //TODO: Uncomment when chambers' terrain has proper layering
        //if (Layers.TerrainLayers.Contains(other.gameObject.layer))
        {
            SpawnParticles(transform.position);
        }

        Destroy(gameObject);
    }

    protected virtual float CalculateDamage(Collider other) { return damage; }
}
