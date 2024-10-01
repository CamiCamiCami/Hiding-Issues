using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Callbacks;
using UnityEngine;

public class BallMovementManager : MonoBehaviour
{

    private Rigidbody rb;
    private float speed = 4f;
    public GameObject cameraObject;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 current_velocity = this.rb.velocity;
        float new_x = this.speed * x;
        float new_y = current_velocity.y;
        float new_z = this.speed * z;

        this.rb.velocity = new Vector3(new_x, new_y, new_z);
    }
}
