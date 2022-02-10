using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

public class Blade : ProjectileBase
{
    private float startAngle = -45;
    private float stopAngle = 45;
    private float angle;

    private float basicSpeed = 15;
    private float speed;

    public float Emission = 6;
    private float length;
    private Vector3 ownerToBlade;
    private HashSet<Entity> hitEntities = new HashSet<Entity>();

    protected override void Start()
    {
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
        base.Update();

        var dangle = speed * basicSpeed * Time.deltaTime;
        angle += dangle;

        UpdateTransform();

        if (angle > stopAngle)
            Destroy(gameObject);
    }

    public void Launch(GameObject owner, Vector3 direction, float damage, float speed)
    {
        Launch(owner, damage);
        this.speed = speed;
        this.angle = startAngle;

        transform.SetParent(owner.transform);
        ownerToBlade = new Vector3(0, transform.localPosition.y, length / 2);
        UpdateTransform();
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
        }
    }
    private void UpdateTransform()
    {
        Quaternion rotation = Quaternion.Euler(0, -angle, 0);
        transform.localRotation = rotation;
        transform.localPosition = rotation * ownerToBlade;
    }
}
