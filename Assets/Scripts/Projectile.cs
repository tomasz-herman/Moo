using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageApplier
{ 
    public Color color;
    void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    public void Launch(GameObject owner, Vector3 velocity)
    {
        this.owner = owner;
        GetComponent<Rigidbody>().velocity = velocity;
        gameObject.transform.LookAt(transform.position + velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != owner)
        {
            ApplyDamage(other);
            Destroy(gameObject);
        }
    }
}
