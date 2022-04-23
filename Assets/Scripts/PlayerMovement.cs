using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MovementSystem
{
    private CharacterController characterController;
    private new Camera camera;
    private float movementSpeed = 2f;
    private bool iswalking = false;
    public bool IsWalking => iswalking;
    public EventHandler<float> SpeedChanged;
    [HideInInspector] public Vector3 direction;

    public override float Speed
    {
        get => movementSpeed;
        set
        {
            movementSpeed = value;
            SpeedChanged?.Invoke(this, movementSpeed);
        }
    }

    void Start()
    {
        movementSpeed = defaultMovementSpeed;
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    void OnMove(InputValue movementValue)
    {
        if (!Application.isFocused) return;
        
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
        Vector3 velocity = direction * movementSpeed * Time.fixedDeltaTime;

        iswalking = velocity != Vector3.zero;

        characterController.Move(velocity);
    }


}
