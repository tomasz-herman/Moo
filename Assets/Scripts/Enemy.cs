using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathSummon;
    public Vector3 summonPos1, summonPos2;
    public int pointsForKill = 1;
    public float movementSpeed = 1f;
    private CharacterController characterController;

    public Vector3 movementDirection;
    public float remainingMovementTime = 0;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
        gameObject.transform.LookAt(gameObject.transform.position + movementDirection);
        remainingMovementTime -= Time.deltaTime;
        if (remainingMovementTime <= 0)
        {
            remainingMovementTime = Utils.FloatBetween(2, 8);
            movementDirection = new Vector3(Utils.FloatBetween(-1, 1), 0, Utils.FloatBetween(-1, 1)).normalized;
        }
    }

    public void GetKilled(ScoreSystem system = null)
    {
        system?.AddScore(pointsForKill);
        if (deathSummon != null)
            Instantiate(deathSummon, new Vector3(Utils.FloatBetween(summonPos1.x, summonPos2.x),
                Utils.FloatBetween(summonPos1.y, summonPos2.y), Utils.FloatBetween(summonPos1.z, summonPos2.z)), Quaternion.identity);
        Destroy(gameObject);
    }
}
