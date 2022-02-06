using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private new Camera camera;
    public float movementSpeed = 2f;
    private bool iswalking = false;
    public bool IsWalking { get { return iswalking; } }
    public EventHandler<float> SpeedChanged;
    private Vector3 direction;

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

    void OnMove(InputValue movementValue)
    {
        if(GameWorld.IsPaused()) return;

        Vector2 movementVector = movementValue.Get<Vector2>();
        float right = movementVector.x;
        float forward = movementVector.y;
        
        Vector3 toCharacter = characterController.transform.position - camera.transform.position;
        toCharacter.y = 0;
        toCharacter.Normalize();
        Vector3 toRight = Quaternion.Euler(0, 90, 0) * toCharacter;

        direction = toCharacter * forward + toRight * right;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = direction.normalized * movementSpeed * Time.deltaTime;

        iswalking = velocity != Vector3.zero;

        characterController.Move(velocity);
    }


}
