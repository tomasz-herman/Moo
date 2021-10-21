using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Shooting shooting;
    private PlayerMovement movement;
    void Start()
    {
        shooting = GetComponent<Shooting>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        
    }

    public void Upgrade()
    {
        shooting.triggerTimeout *= 0.9f;
        shooting.projectileSpeed *= 1.1f;
        movement.movementSpeed *= 1.1f;
    }
}
