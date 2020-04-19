using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    bool isDragging = false;
    bool isCrouching = false;

    float xRotation = 0f;

    public Vector3 normalPosition, crouchingPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown("left ctrl"))
        {
            isCrouching = !isCrouching;
        }
        if (isCrouching)
        {
            transform.localPosition = crouchingPosition;
        }
        else
        {
            transform.localPosition = normalPosition;
        }

        if (!isDragging)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * PlayerSettings.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * PlayerSettings.mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
