using Assets.Scripts.Weapons;
using UnityEngine;

public class Blade : ProjectileBase
{
    private float startAngle = -45;
    private float stopAngle = 45;
    private float angle;

    private float basicSpeed = 15;
    private float speed;

    public Color color;
    public float Emission = 6;
    protected override float baseDamage => 30f;
    private float extraDamage = 0;

    protected override void Start()
    {
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.SetColor("_EmissiveColor", color * Emission);
        material.SetColor("_BaseColor", color);
        base.Start();
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
        this.Owner = owner;
        this.speed = speed;
        this.angle = startAngle;
        this.extraDamage = extradamage;

        transform.SetParent(owner.transform);
        transform.position = transform.position + Quaternion.Euler(0, 360 + startAngle, 0) * direction;
        SetRotation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Owner)
        {
            ApplyDamage(other, baseDamage + extraDamage);
            Destroy(gameObject);
        }
    }
    private void SetRotation()
    {
        var direction = 2 * transform.position - Owner.transform.position;
        direction.y = transform.position.y;
        transform.LookAt(direction);
    }

}
