using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float timeToLive = 10f;
    private float elapsedTime = 0f;
    private GameObject owner;
    public Color color;
    public LayerMask enemyMask;
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
        if (other.gameObject != owner)
        {
            Detonate();
        }
    }
    private void Detonate()
    {
        var others = Physics.OverlapSphere(gameObject.transform.position, 5, enemyMask);
        foreach (var other in others)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.GetKilled(owner.GetComponent<ScoreSystem>());
        }
        Destroy(gameObject);
    }
}
