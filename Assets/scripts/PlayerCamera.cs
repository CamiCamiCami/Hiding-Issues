using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    // Variables
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    private bool canMove = true;

    void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;

    }
    
    void Update()
    {
        if (!canMove) {
            return;
        }

        float inputX = Input.GetAxis("Mouse X")*mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        cameraTransform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        this.transform.Rotate(Vector3.up * inputX);
    }

    public void LockCamera()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        canMove = false;
    }

    public void FreeCamera()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
    }
}
