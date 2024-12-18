using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool thrown, holding;
    private bool swingBall = true;
    Rigidbody rb;

    [SerializeField]
    public float speed = 1.0f;

    [SerializeField]
    public float thrownSpeed = 3.0f;

    private Transform target;
    private Transform finishTarget;

    private void Awake()
    {
        var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(1.0f, 0.5f, 0.5f);
        finishTarget = plane.transform;
        plane.transform.position = new Vector3(0.0f, 0.0f, 6.0f);
        plane.SetActive(false);

        // Create and position the cylinder. Reduce the size.
        var cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.localScale = new Vector3(0.15f, 1.0f, 0.15f);
        cylinder.SetActive(false);

        // Grab cylinder values and place on the target.
        target = cylinder.transform;
        target.transform.position = new Vector3(3.5f, 0.5f, -5f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        swingBall = true;
    }

    private void OnMouseDown()
    {
        holding = true;
        swingBall = false;

    }

    private void OnMouseDrag()
    {
        holding = true;
        swingBall = false;
    }

    private void Update()
    {
        if (swingBall)
        {
            Debug.Log("swingBall  " + swingBall);
            BallMoveTowards();
        }
        else
        {
            ThrowBall();
        }


    }


    void BallMoveTowards()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            target.position = new Vector3((-1.0f) * target.position.x, target.position.y, target.position.z);
        }
    }

    void ThrowBall()
    {
        var footStep = thrownSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, finishTarget.position, footStep);
        target.position = new Vector3(target.position.x, 0.0f * target.position.y, target.position.z);
    }
}
