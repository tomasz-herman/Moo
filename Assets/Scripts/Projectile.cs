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
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.GetKilled(owner.GetComponent<ScoreSystem>());
            }
            Destroy(gameObject);
        }
    }
}
