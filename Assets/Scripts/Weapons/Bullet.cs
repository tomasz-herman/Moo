using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ProjectileBase
{
    public Color color;

    protected override float baseDamage => 30f;
    private float extraDamage = 0;

    private Vector3 initPosition;

    void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
    }
    public void Launch(GameObject owner, Vector3 velocity, float extradamage)
    {
        this.owner = owner;
        extraDamage = extradamage;
        initPosition = owner.transform.position;
        GetComponent<Rigidbody>().velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != owner)
        {
            var distance = Vector3.Distance(gameObject.transform.position, initPosition);
            var damage = (baseDamage + extraDamage) / (1 + distance);
            ApplyDamage(other, damage);
            Destroy(gameObject);
        }
    }
}
