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

    public float Emission = 6;
    private float extraDamage = 1;
    private float length;
    private Vector3 ownerToBlade;
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

        UpdateTransform();
    }

    protected void Awake()
    {
        length = GetComponent<BoxCollider>().transform.localScale.z;
    }

    protected override void Update()
    {
        base.FixedUpdate();

        var dangle = speed * basicSpeed * Time.deltaTime;
        angle += dangle;

        UpdateTransform();

        if (angle > stopAngle)
            Destroy(gameObject);
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
                ApplyDamage(other, damage);

                bool raycastSuccess = false;
                foreach(var hit in Physics.RaycastAll(transform.position - transform.localPosition, transform.localPosition, 2*length))
                {
                    if(hit.transform.GetComponent<Entity>() == entity)
                    {
                        SpawnParticles(hit.transform.position);
                        raycastSuccess = true;
                        break;
                    }
                }
                if (!raycastSuccess)
                    SpawnParticles((entity.transform.position + transform.position)/2);
                
                hitEntities.Add(entity);
            }
    private void UpdateTransform()
    {
        Quaternion rotation = Quaternion.Euler(0, -angle, 0);
        transform.localRotation = rotation;
        transform.localPosition = rotation * ownerToBlade;
    }
}
