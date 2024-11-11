using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool swingBall = true;
    Rigidbody rb;

    [SerializeField]
    public float speed = 1.0f;

    [SerializeField]
    GameObject messageInABottle;


    private Transform target;
    private Transform finishTarget;

    private void Awake()
    {
        messageInABottle.SetActive(false);
        // Create and position the cylinder. Reduce the size.
        var cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.localScale = new Vector3(0.15f, 1.0f, 0.15f);
        cylinder.SetActive(false);

        // Grab cylinder values and place on the target.
        target = cylinder.transform;
        target.transform.position = new Vector3(-3.5f, 0.75f, 5.34f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision other)
    {
        swingBall = false;
        messageInABottle.SetActive(true);
    }

    void OnCollisionStay(Collision other)
    {
        swingBall = false;
        messageInABottle.SetActive(true);
    }


    private void Update()
    {
        if (swingBall)
        {
            BallMoveTowards();
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
}
