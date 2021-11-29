using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ProjectileBase
{
    public Color color;

    protected override float baseDamage => 10f;
    private float extraDamage = 0;

    void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    public void Launch(GameObject owner, Vector3 velocity, float extradamage)
    {
        this.owner = owner;
        extraDamage = extradamage;
        GetComponent<Rigidbody>().velocity = velocity;
        gameObject.transform.LookAt(transform.position + velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != owner)
        {
            ApplyDamage(other, baseDamage + extraDamage);
            Destroy(gameObject);
        }
    }
}
