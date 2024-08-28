using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera2Test : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;

    private Rigidbody rb;
    private bool isGrounded;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("PlayerMovement script started.");
    }

    void Update()
    {
        MyInput();
        GroundCheck();
        Debug.Log("Is Grounded: " + isGrounded);
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            Debug.Log("Moving player.");
            MovePlayer();
        }
        else
        {
            Debug.Log("Player is not grounded.");
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Debug.Log("Horizontal Input: " + horizontalInput + " Vertical Input: " + verticalInput);
    }

    private void MovePlayer()
    {
        // Calculate Movement Direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Prevent Camera Flying
        moveDirection.y = 0;

        // Move the player using Rigidbody
        rb.MovePosition(transform.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
        Debug.Log("MoveDirection: " + moveDirection);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        Debug.Log("Ground check performed. Grounded: " + isGrounded);
    }
}
