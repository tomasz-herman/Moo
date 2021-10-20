using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private new Camera camera;
    public float movementSpeed = 2f;

    public float gravity = 1f;
    public float verticalSpeed = 0;

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

        if (characterController.isGrounded)
            verticalSpeed = 0;
        else
            verticalSpeed += gravity * Time.deltaTime;
        velocity.y -= verticalSpeed;

        characterController.Move(velocity);
    }
}
