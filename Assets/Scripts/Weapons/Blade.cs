using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float timeToLive = 10f;
    private float elapsedTime = 0f;

    private float startAngle = -45;
    private float stopAngle = 45;
    private float angle;

    private float basicSpeed = 15;
    private float speed;

    private GameObject owner;
    public Color color;
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > timeToLive)
            Destroy(gameObject);

        var dangle = speed * basicSpeed * Time.deltaTime;
        angle += dangle;
        if (angle > stopAngle)
            Destroy(gameObject);

        transform.localPosition = Quaternion.Euler(0, dangle, 0) * transform.localPosition;
        SetRotation();
    }

    public void Launch(GameObject owner, Vector3 direction, float speed)
    {
        this.owner = owner;
        this.speed = speed;
        this.angle = startAngle;

        transform.SetParent(owner.transform);
        transform.position = transform.position + Quaternion.Euler(0, 360 + startAngle, 0) * direction;
        SetRotation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != owner)
        {
            Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
            Player playerHit = other.gameObject.GetComponent<Player>();
            if (owner != null && owner.GetComponent<Player>() != null) // Player was shooting
            {
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(15, owner.GetComponent<ScoreSystem>());
                }
            }
            if (owner == null || (owner != null && owner.GetComponent<Enemy>() != null)) // Enemy was shooting (if it is null it means it is dead enemy)
            {
                if (playerHit != null)
                {
                    playerHit.healthSystem.Health -= 10;
                }
            }
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
