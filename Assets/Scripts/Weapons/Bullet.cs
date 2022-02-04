using Assets.Scripts.Weapons;
using UnityEngine;

public class Bullet : Projectile
{
    protected override float baseDamage => 30f;

    private Vector3 initPosition;

    protected override void Start()
    {
        base.Start();
    }
    public override void Launch(GameObject owner, Vector3 velocity, float extradamage)
    {
        base.Launch(owner, velocity, extradamage);
        initPosition = owner.transform.position;
    }

    protected override float CalculateDamage(Collider other)
    {
        var distance = Vector3.Distance(gameObject.transform.position, initPosition);
        var damage = (baseDamage * extraDamage) / (1 + distance);
        return damage;
    }
}
