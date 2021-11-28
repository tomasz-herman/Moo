using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float timeToLive = 10f;
    private float elapsedTime = 0f;
    private GameObject owner;
    public Color color;
    void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > timeToLive)
            Destroy(gameObject);
    }

    public void Launch(GameObject owner, Vector3 velocity)
    {
        this.owner = owner;
        GetComponent<Rigidbody>().velocity = velocity;
        gameObject.transform.LookAt(transform.position + velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != owner)
        {
            Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
            Player playerHit = other.gameObject.GetComponent<Player>();
            if(owner != null && owner.GetComponent<Player>() != null) // Player was shooting
            {
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(15, owner.GetComponent<ScoreSystem>());
                }
            }
            if(owner == null || (owner != null && owner.GetComponent<Enemy>() != null)) // Enemy was shooting (if it is null it means it is dead enemy)
            {
                if (playerHit != null)
                {
                    playerHit.healthSystem.Health -= 10;
                }
            }
            Destroy(gameObject);
        }
    }
}
