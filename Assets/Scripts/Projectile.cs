using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float timeToLive = 10f;
    private float elapsedTime = 0f;
    private GameObject owner;
    void Start()
    {
        
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
                    enemyHit.TakeDamage(25, owner.GetComponent<ScoreSystem>());
                }
            }
            if(owner != null && owner.GetComponent<Enemy>() != null) // Enemy was shooting
            {
                if (playerHit != null)
                {
                    playerHit.healthSystem.Health -= 5;
                }
            }
            Destroy(gameObject);
        }
    }
}
