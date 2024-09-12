using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Character
{
    Tyrone,
    Jack,
    Emma,
    Walter
}

public class Player : MonoBehaviour
{

    public Character character;

    [Header("Rigid Body")]
    public float Mass = 1;
    public float Drag = 5;
    public float AngularDrag = 0.05f;
    private bool AutomaticCenterOfMass = true;
    private bool AutomaticTensor = true;
    private bool UseGravity = true;
    private bool IsKinematic = false;
    private RigidbodyInterpolation Interpolate = RigidbodyInterpolation.Interpolate;
    private CollisionDetectionMode CollisionDetection = CollisionDetectionMode.Continuous;
    // Constrains
    // Layer Overrides

    [Header("First Person Camera")]
    public float SensibilityX = 250;
    public float SensibilityY = 250;

    [Header("Player Movement")]
    public float MaxSpeed = 70;

    [Header("Player Look At")]
    public Camera cam;


    private Room currentRoom;
    private PlayerOverlay overlay;
    private bool hiding = false;
    private List<Room> SolvedRooms = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = this.AddComponent<Rigidbody>();
        rb.mass = Mass;
        rb.drag = Drag;
        rb.angularDrag = AngularDrag;
        rb.automaticCenterOfMass = AutomaticCenterOfMass;
        rb.automaticInertiaTensor = AutomaticTensor;
        rb.useGravity = UseGravity;
        rb.isKinematic = IsKinematic;
        rb.interpolation = Interpolate;
        rb.collisionDetectionMode = CollisionDetection;

        PlayerCamera cameraManager = this.AddComponent<PlayerCamera>();
        cameraManager.sensibilityX = SensibilityX;
        cameraManager.sensibilityY = SensibilityY;
        cameraManager.Camera = this.GetComponentInChildren<SnapCamera>().gameObject; //Agarra el snap que ata a la camara

        PlayerMovement playerMovement = this.AddComponent<PlayerMovement>();
        playerMovement.MaxSpeed = MaxSpeed;
        playerMovement.rb = rb;

        PlayerLookAt playerLookAt = this.AddComponent<PlayerLookAt>();
        playerLookAt.CameraComponent = cam;

        overlay = this.GetComponentInChildren<PlayerOverlay>();
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)) {
            if(hiding)
            {
                StopHiding();
            } 
            else
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
            overlay.Fade();
            hiding = true;
        }
    }

    void StopHiding()
    {
        Debug.Log("Salis del escondite ");
        overlay.ResetFade();
        hiding = false;
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

}
