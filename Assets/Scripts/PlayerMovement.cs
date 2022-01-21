using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private new Camera camera;
    public float movementSpeed = 2f;
    private bool iswalking = false;
    public bool IsWalking { get { return iswalking; } }
    public EventHandler<float> SpeedChanged;

    public float Speed
    {
        get { return movementSpeed; }
        set
        {
            movementSpeed = value;
            SpeedChanged?.Invoke(this, movementSpeed);
        }
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    void Update()
    {
        Vector3 toCharacter = characterController.transform.position - camera.transform.position;
        toCharacter.y = 0;
        toCharacter.Normalize();
        Vector3 toRight = Quaternion.Euler(0, 90, 0) * toCharacter;

        float moveForward = 0, moveRight = 0;
        if (Input.GetKey(KeyCode.D))
            moveRight++;
        if (Input.GetKey(KeyCode.A))
            moveRight--;
        if (Input.GetKey(KeyCode.W))
            moveForward++;
        if (Input.GetKey(KeyCode.S))
            moveForward--;

        Vector3 direction = toCharacter * moveForward + toRight * moveRight;
        Vector3 velocity = direction.normalized * movementSpeed * Time.deltaTime;

        iswalking = velocity != Vector3.zero;

        characterController.Move(velocity);
    }


    
    
}
