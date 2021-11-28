using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : DamageApplier
{
    public Color color;

    private float speed = 100f;
    private float explosionRange = 50;
    private bool isExplosing = false;
    void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    protected override void Update()
    {
        base.Update();

        if(gameObject.transform.lossyScale.x > explosionRange)
            Destroy(gameObject);

        if (isExplosing)
            gameObject.transform.localScale += speed * Time.deltaTime * Vector3.one;
    }

    public void Launch(GameObject owner, Vector3 velocity)
    {
        this.owner = owner;
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

            //TODO: damage is higher near the explosion
            //Vector3.Distance
            ApplyDamage(other);
        }
    }
}
