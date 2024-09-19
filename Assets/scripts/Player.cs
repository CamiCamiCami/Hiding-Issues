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
    public float SensibilityX = 250;
    public float SensibilityY = 250;

    [Header("Player Movement")]
    public float Speed = 5f;

    [Header("Player Look At")]
    public Camera cam;

    private Room currentRoom;
    private PlayerOverlay overlay;
    private bool hiding = false;
    private bool canHide = false;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerCamera cameraManager = this.AddComponent<PlayerCamera>();
        //cameraManager.sensibilityX = SensibilityX;
        //cameraManager.sensibilityY = SensibilityY;
        //cameraManager.Camera = this.GetComponentInChildren<SnapCamera>().gameObject; //Agarra el snap que ata a la camara

        PlayerMovement playerMovement = this.AddComponent<PlayerMovement>();
        playerMovement.Speed = Speed;
        playerMovement.cc = this.GetComponent<CharacterController>();

        PlayerLookAt playerLookAt = this.AddComponent<PlayerLookAt>();
        playerLookAt.CameraComponent = cam;

        overlay = this.GetComponentInChildren<PlayerOverlay>();

        return;
    }

    // Update is called once per frame
    void Update()
    {
        //esto es para probar el reset
        if(Input.GetKeyDown(KeyCode.M)) {
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

}
