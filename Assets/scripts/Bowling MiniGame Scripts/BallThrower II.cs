using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : MonoBehaviour
{
    private GameObject Ball;

    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPosition;
    private Vector2 endPosition;


    // The min distance to count it as a flick
    public float MinSwipDistance = 0;
    private float BallVelocity = 0;
    private float BallSpeed = 0;
    public float MaxBallSpeed = 40;
    private Vector3 angle;

    private bool thrown, holding;
    private Vector3 newPosition;
    public float smooth = 0.7f;
    Rigidbody rb;




    // Start is called before the first frame update
    void Start()
    {
        setupBall();
    }

    // Declares the ball
    void setupBall()
    {
        GameObject _ball = GameObject.FindGameObjectWithTag("Player");
        Ball = _ball;
        rb = Ball.GetComponent<Rigidbody>();
        ResetBall();
    }

    void ResetBall()
    {
        angle = Vector3.zero;
        endPosition = Vector2.zero;
        startPosition = Vector2.zero;
        BallSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;
        thrown = holding = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        Ball.transform.position = transform.position;
    }

    void PickupBall()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 30f;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Ball.transform.localPosition = Vector3.Lerp(Ball.transform.localPosition, newPosition, 80f * Time.deltaTime);
    }

    private void Update()
    {
        if (holding)
        {
            PickupBall();
        }
        if (thrown)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, 100f))
            {
                if (_hit.transform == Ball.transform)
                {
                    startTime = Time.time;
                    startPosition = Input.mousePosition;
                    holding = true;
                }
            }
        } else if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            endPosition = Input.mousePosition;
            swipeDistance = (endPosition - startPosition).magnitude;
            swipeTime = endTime - startTime;

            if (swipeTime < 0.5f && swipeDistance > 30f)
            {
                // Thrown the ball
                CalSpeed();
                CalAngle();
                rb.AddForce(new Vector3 (angle.x * BallSpeed, angle.y * BallSpeed, angle.z * BallSpeed * (-10)));
                rb.useGravity =true;
                holding = false;
                thrown = true;
                Invoke("ResetBall", 4f);
            } else
            {
                ResetBall();
            }
        }
    }


    void CalAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPosition.x, endPosition.y + 50f, Camera.main.nearClipPlane + 5));
    }
    void CalSpeed()
    {
        if (swipeTime > 0)
        {
            BallVelocity = swipeDistance / (swipeDistance - swipeTime);
        }

        BallSpeed = BallVelocity * 40;

        if (BallSpeed >= MaxBallSpeed)
        {
            BallSpeed = MaxBallSpeed;
        }

        if (BallSpeed <= MaxBallSpeed)
        {
            BallSpeed = BallSpeed + 40;
        }
        swipeTime = 0;
    }

}
