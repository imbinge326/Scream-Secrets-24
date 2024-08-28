using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform rotation;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.visible = false; // Set this to true to keep the cursor visible
        Cursor.lockState = CursorLockMode.None; // Ensure the cursor is not locked

        // If dont want to see the Cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    
    void Update()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamp to prevent flipping over
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        // Apply the rotations to the camera
        rotation.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
