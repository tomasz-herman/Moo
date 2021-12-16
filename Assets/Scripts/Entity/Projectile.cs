using Assets.Scripts.Weapons;
using UnityEngine;

public class Projectile : ProjectileBase
{
    public Color color;

    protected override float baseDamage => 10f;
    private float extraDamage = 0;

    protected override void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
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
            ApplyDamage(other, baseDamage + extraDamage);
            Destroy(gameObject);
        }
    }
}
