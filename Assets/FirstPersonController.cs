using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float jumpForce = 5.0f;

    private float verticalRotation = 0;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private float gravity = 9.81f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Handle mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);

        // Handle keyboard input for movement
        float moveForward = Input.GetAxis("Vertical") * speed;
        float moveSideways = Input.GetAxis("Horizontal") * speed;

        Vector3 movement = new Vector3(moveSideways, 0, moveForward);
        movement = transform.TransformDirection(movement);

        // Apply gravity
        if (characterController.isGrounded)
        {
            moveDirection.y = -gravity;

            // Handle jumping
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the character controller
        characterController.Move((movement + moveDirection) * Time.deltaTime);
    }

    public void Bounce(float force)
    {
        moveDirection.y = force;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }

}

