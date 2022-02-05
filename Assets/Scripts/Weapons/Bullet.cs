using Assets.Scripts.Weapons;
using UnityEngine;

public class Bullet : Projectile
{
    private Vector3 initPosition;
    private float damageDecayFactor = 0.1f;

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
        var dmg = (damage) / (1 + damageDecayFactor * distance);
        return dmg;
    }
}
