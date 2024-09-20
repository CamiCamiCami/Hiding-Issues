using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Character
{
    Tyrone,
    Jack,
    Emma,
    Walter
}

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{

    public Character character;

    [Header("First Person Camera")]
    public float Sensibility = 250;

    [Header("Player Movement")]
    public float Speed = 5f;

    [Header("Player Look At")]
    public Camera cam;


    private Room currentRoom;
    private PlayerOverlay overlay;
    private PlayerCamera playerCamera;
    private PlayerMovement playerMovement;
    private PlayerLookAt playerLookAt;
    private bool hiding = false;
    private bool canHide = false;
    private bool isLookingAtCanvas = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.AddComponent<PlayerMovement>();
        playerMovement.Speed = Speed;
        playerMovement.cc = this.GetComponent<CharacterController>();

        playerLookAt = this.AddComponent<PlayerLookAt>();
        playerLookAt.CameraComponent = cam;

        playerCamera = this.AddComponent<PlayerCamera>();
        playerCamera.cameraTransform = cam.transform;
        playerCamera.mouseSensitivity = Sensibility;

        overlay = this.GetComponentInChildren<PlayerOverlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLookingAtCanvas && Input.GetKeyDown(KeyCode.Escape)) {
            this.ExitCanvas();
        }

        if(canHide && Input.GetKeyDown(KeyCode.H)) 
        { 
            if (hiding)
            {
                StopHiding();
            } else
            {
                Hide();
            }
        }
    }

    void Hide()
    {
        if (currentRoom != null)
        {
            Debug.Log("Te escondess en " + currentRoom.name);
            hiding = true;
            this.Immobilize();
            overlay.Fade();
            overlay.ResetHidePopUp();
        }
    }

    void StopHiding()
    {
        hiding = false;
        this.Mobilize();
        overlay.ResetFade();
        overlay.ShowHidePopUp();
    }

    public void EnteredRoom(Room room)
    {
        overlay.SetSecurityPercentage(room.GetSecurityPercentage());
        currentRoom = room;
    }

    public void ExitedRoom(Room room)
    {
        if (currentRoom == room)
        {
            overlay.VoidSecurityPercentage();
            currentRoom = null;
        }
    }

    public void UpdateRoom(Room room) 
    {
        if (currentRoom == room)
        {
            overlay.SetSecurityPercentage(room.GetSecurityPercentage());
        }
    }

    public bool IsHiding()
    {
        return hiding;
    }

    public Room GetCurrentRoom()
    {
        return currentRoom;
    }

    public void Immobilize()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;  // Desactiva el movimiento
            Debug.Log("Movimiento deshabilitado.");
        }
    }

    public void Mobilize()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = true;  // Activa nuevamente el movimiento
            Debug.Log("Movimiento habilitado.");
        }
    }

    public void ShowHidePopUp()
    {
        this.overlay.ShowHidePopUp();
    }

    public void ResetHidePopUp()
    {
        this.overlay.ResetHidePopUp();
    }

    public void CanHide()
    {
        canHide = true;
        overlay.ShowHidePopUp();
    } 

    public void CannotHide()
    {
        canHide = false;
        overlay.ResetHidePopUp();
    }

    Canvas canvasLookingAt = null;
    public void DisplayCanvas(Canvas canvas) {
        isLookingAtCanvas = true;
        playerCamera.LockCamera();
        playerMovement.DisableMovement();
        playerLookAt.DisableInteraction();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.enabled = true;
        canvasLookingAt = canvas;
    }

    private void ExitCanvas() {
        isLookingAtCanvas = false;
        playerCamera.FreeCamera();
        playerMovement.EnableMovement();
        playerLookAt.EnableInteraction();
        canvasLookingAt.enabled = false;
        canvasLookingAt = null;
    }

}
