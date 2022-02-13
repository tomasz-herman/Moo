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
    public float length;
    private Vector3 ownerToBlade;
    private HashSet<Entity> hitEntities;

    public void Launch(GameObject owner, Vector3 direction, float damage, float speed)
    {
        base.Launch(owner, damage);

        this.speed = speed;
        this.angle = startAngle;

        transform.SetParent(owner.transform);
        ownerToBlade = new Vector3(0, transform.localPosition.y, length / 2);
        UpdateTransform();

        for (int i = 0; i < projectileUpgrades.Count; i++)
        {
            projectileUpgrades[i].OnLaunch(this, projectileUpgradesData[i]);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        hitEntities ??= new HashSet<Entity>();
        length = GetComponent<BoxCollider>().transform.localScale.z;
    }

    protected override void Start()
    {
        base.Start();
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.SetColor("_EmissiveColor", color * Emission);
        material.SetColor("_BaseColor", color);

        UpdateTransform();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var dangle = speed * basicSpeed * Time.deltaTime;
        angle += dangle;

        UpdateTransform();

        for (int i = 0; i < projectileUpgrades.Count; i++)
        {
            projectileUpgrades[i].OnUpdate(this, projectileUpgradesData[i]);
        }

        if (angle > stopAngle)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < projectileUpgrades.Count; i++)
        {
            projectileUpgrades[i].OnDrawGizmos(this, projectileUpgradesData[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner) return;

        Entity entity = other.GetComponent<Entity>();
        if (entity != null && !hitEntities.Contains(entity))
        {
            ApplyDamage(other, damage);

            //why is this needed?
            bool raycastSuccess = false;
            foreach (var hit in Physics.RaycastAll(transform.position - transform.localPosition, transform.localPosition, 2 * length))
            {
                if (hit.transform.GetComponent<Entity>() == entity)
                {
                    SpawnParticles(hit.transform.position);
                    raycastSuccess = true;
                    break;
                }
            }

            if (!raycastSuccess)
                SpawnParticles((entity.transform.position + transform.position) / 2);

            hitEntities.Add(entity);
        }
    }

    private void UpdateTransform()
    {
        Quaternion rotation = Quaternion.Euler(0, -angle, 0);
        transform.localRotation = rotation;
        transform.localPosition = rotation * ownerToBlade;
    }
}
