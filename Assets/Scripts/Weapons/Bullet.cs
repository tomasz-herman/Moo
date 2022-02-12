using UnityEngine;

public class Bullet : Projectile
{
    private Vector3 lastFramePosition;
    private float distance;
    private float damageDecayFactor = 0.1f;
    private float damageOver2;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        distance += Vector3.Distance(gameObject.transform.position, lastFramePosition);
    }

    public override void Launch(GameObject owner, Vector3 velocity, float extraDamage)
    {
        base.Launch(owner, velocity, extraDamage);
        lastFramePosition = owner.transform.position;
        distance = 0f;
        damageOver2 = 0.5f * damage;
    }

    protected override float CalculateDamage(Collider other)
    {
        var dmg = (damage) / (1 + damageDecayFactor * distance);
        dmg = Mathf.Max(dmg, damageOver2);
        return dmg;
    }
}
