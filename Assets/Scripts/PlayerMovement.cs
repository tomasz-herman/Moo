using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private new Camera camera;
    public float movementSpeed = 2f;
    private bool iswalking = false;
    public bool IsWalking => iswalking;
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

    void Update()
    {
        if (!Application.isFocused) return;

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

        direction = toCharacter * moveForward + toRight * moveRight;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = direction.normalized * movementSpeed * Time.deltaTime;

        iswalking = velocity != Vector3.zero;

        characterController.Move(velocity);
    }


}
