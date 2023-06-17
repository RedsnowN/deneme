using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Krakterhareket : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float defaultmoveSpeed = 0;
    private float hýzlýmovespeed = 5f;
    public float jumpForce = 5f;
    public float lookSpeed = 2f;
    public float verticalLookRange = 60f;
    public float smoothRotation = 5f;
    public float yerçekimi;

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
        hýzlýmovespeed = moveSpeed * 3;
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            for (float i = moveSpeed; i < hýzlýmovespeed; i++)
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

        playerVelocity.y += Physics.gravity.y * Time.deltaTime * yerçekimi;
        moveDirection.y = playerVelocity.y;

        controller.Move(moveDirection * Time.deltaTime);

        float mouseXDelta = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseYDelta = Input.GetAxis("Mouse Y") * lookSpeed;

        mouseX += mouseXDelta;
        mouseY -= mouseYDelta;
        mouseY = Mathf.Clamp(mouseY, -verticalLookRange, verticalLookRange);

        // Sadece kamera dönüþü
        cameraTransform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);

        // Karakter dönüþü
        transform.rotation = Quaternion.Euler(0f, mouseX, 0f);
    }
}
