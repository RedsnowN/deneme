using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Krakterhareket : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float defaultmoveSpeed = 0;
    private float h�zl�movespeed = 5f;
    public float jumpForce = 5f;
    public float lookSpeed = 2f;
    public float verticalLookRange = 60f;
    public float smoothRotation = 5f;
    public float yer�ekimi;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isJumping = false;
    private float mouseX;
    private float mouseY;
    private Transform cameraTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = Camera.main.transform;
        defaultmoveSpeed = moveSpeed;
        h�zl�movespeed = moveSpeed * 3;
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            for (float i = moveSpeed; i < h�zl�movespeed; i++)
            {
                moveSpeed++ ;
            }
            
        }
        else
            moveSpeed = defaultmoveSpeed;
        

        Vector3 moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;
        moveDirection *= moveSpeed;

        if (controller.isGrounded)
        {
            isJumping = false;
            playerVelocity.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                playerVelocity.y += Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
                isJumping = true;
            }
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime * yer�ekimi;
        moveDirection.y = playerVelocity.y;

        controller.Move(moveDirection * Time.deltaTime);

        float mouseXDelta = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseYDelta = Input.GetAxis("Mouse Y") * lookSpeed;

        mouseX += mouseXDelta;
        mouseY -= mouseYDelta;
        mouseY = Mathf.Clamp(mouseY, -verticalLookRange, verticalLookRange);

        // Sadece kamera d�n���
        cameraTransform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);

        // Karakter d�n���
        transform.rotation = Quaternion.Euler(0f, mouseX, 0f);
    }
}
