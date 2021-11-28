using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : ProjectileBase
{
    private float startAngle = -45;
    private float stopAngle = 45;
    private float angle;

    private float basicSpeed = 15;
    private float speed;

    public Color color;
    protected override float baseDamage => 30f;
    private float extraDamage = 0;

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }

    protected override void Update()
    {
        base.Update();

        var dangle = speed * basicSpeed * Time.deltaTime;
        angle += dangle;
        if (angle > stopAngle)
            Destroy(gameObject);

        transform.localPosition = Quaternion.Euler(0, dangle, 0) * transform.localPosition;
        SetRotation();
    }

    public void Launch(GameObject owner, Vector3 direction, float extradamage, float speed)
    {
        this.owner = owner;
        this.speed = speed;
        this.angle = startAngle;
        this.extraDamage = extradamage;

        transform.SetParent(owner.transform);
        transform.position = transform.position + Quaternion.Euler(0, 360 + startAngle, 0) * direction;
        SetRotation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != owner)
        {
            ApplyDamage(other, baseDamage + extraDamage);
            Destroy(gameObject);
        }
    }
    private void SetRotation()
    {
        var direction = 2 * transform.position - owner.transform.position;
        direction.y = transform.position.y;
        transform.LookAt(direction);
    }

}
