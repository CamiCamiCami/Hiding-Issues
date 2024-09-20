using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    // Variables
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    private bool locked = true;

    void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    
    void Update()
    {

        float inputX = Input.GetAxis("Mouse X")*mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);

        /*
        if (Input.GetKeyDown(KeyCode.LeftShift)) {

            locked = !locked;

        }

        if (locked) {

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        } else {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        */
       
    }
}
