using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

public class Blade : ProjectileBase
{
    public float startAngle = -45;
    public float stopAngle = 45;
    private float angle;

    private float basicSpeed = 15;
    private float speed;

    public Color color;
    public float Emission = 6;
    protected override float baseDamage => 30f;
    private float extraDamage = 1;

    private HashSet<Entity> hitEntities = new HashSet<Entity>();

    public Vector3 direction;

    private bool isStarted = false;

    public void Launch(GameObject owner, Vector3 direction, float extradamage, float speed)
    {
        Start();

        this.Owner = owner;
        this.speed = speed;
        this.angle = startAngle;
        this.extraDamage = extradamage;
        this.direction = direction;

        transform.SetParent(owner.transform);
        transform.position = transform.position + Quaternion.Euler(0, 360 + startAngle, 0) * direction;
        SetRotation();

        foreach (var upgrade in ProjectileUpgrades)
        {
            upgrade.OnLaunch(this);
        }
    }

    protected override void Start()
    {
        if (isStarted)
            return;

        isStarted = true;
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.SetColor("_EmissiveColor", color * Emission);
        material.SetColor("_BaseColor", color);
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var dangle = speed * basicSpeed * Time.deltaTime;
        angle += dangle;
        if (angle > stopAngle)
            Destroy(gameObject);

        transform.localPosition = Quaternion.Euler(0, dangle, 0) * transform.localPosition;
        SetRotation();
    }

    private void OnDrawGizmos()
    {
        foreach (var upgrade in ProjectileUpgrades)
        {
            upgrade.OnDrawGizmos(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Owner)
        {
            Entity entity = other.GetComponent<Entity>();
            if(entity != null && !hitEntities.Contains(entity))
            {
                ApplyDamage(other, baseDamage * extraDamage);
                hitEntities.Add(entity);
            }
        }
    }

    private void SetRotation()
    {
        var direction = 2 * transform.position - Owner.transform.position;
        direction.y = transform.position.y;
        transform.LookAt(direction);
    }
}
