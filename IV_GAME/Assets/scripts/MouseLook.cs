using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public PlayerMovement2 grapplingScript;

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;






  
    void Start()
    {
        mouseSensitivity = Sensitivity.sensitivity;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        Debug.Log(mouseSensitivity);
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity*0.6f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity *0.6f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }

    public void AdjustSpeed (float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
    }
}
