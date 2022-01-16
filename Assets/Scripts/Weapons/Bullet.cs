using Assets.Scripts.Weapons;
using UnityEngine;

public class Bullet : ProjectileBase
{
    public Color color;

    protected override float baseDamage => 30f;
    private float extraDamage = 0;

    private Vector3 initPosition;

    protected override void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
        base.Start();
    }
    public void Launch(GameObject owner, Vector3 velocity, float extradamage)
    {
        this.Owner = owner;
        extraDamage = extradamage;
        initPosition = owner.transform.position;
        GetComponent<Rigidbody>().velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Owner)
        {
            var distance = Vector3.Distance(gameObject.transform.position, initPosition);
            var damage = (baseDamage + extraDamage) / (1 + distance);
            ApplyDamage(other, damage);
            Destroy(gameObject);
        }
    }
}
