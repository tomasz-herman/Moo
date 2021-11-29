using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ProjectileBase
{
    public Color color;

    private float explosionSpeed = 40f;
    private float explosionRange = 10f;
    private float damageDecay = 7f; // how many times damage drops on the edge of explosionRange
    private bool isExplosing = false;

    protected override float baseDamage => 50f;
    private float extraDamage = 0;

    void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    protected override void Update()
    {
        base.Update();

        if (gameObject.transform.localScale.x > explosionRange)
            Destroy(gameObject);

        if (isExplosing)
            gameObject.transform.localScale += explosionSpeed * Time.deltaTime * Vector3.one;
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
        if (other.gameObject != owner)
        {
            if (!isExplosing)
            {
                isExplosing = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            var distance = Vector3.Distance(gameObject.transform.position, other.transform.position);
            var damage = (baseDamage + extraDamage) / (1 + damageDecay * distance / explosionRange); //damage is higher near the explosion
            ApplyDamage(other, damage);
        }
    }
}
