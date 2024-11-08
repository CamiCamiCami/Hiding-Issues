using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;
    public float MinSwipDist = 0;
    private float BallVelocity = 0;
    private float BallSpeed = 0;
    public float MaxBallSpeed = 40;
    private Vector3 angle;
    private bool thrown, holding;
    private Vector3 newPosition, resetPos;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
        ResetBall();
    }

    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
        holding = true;
    }

    private void OnMouseDrag()
    {
        PickupBall();
        CalSpeed();
        CalAngle();
        Vector3 force = new Vector3(angle.x * BallSpeed, angle.y * BallSpeed, angle.z * BallSpeed * (-10));
        rb.AddForce(force);
        TrayectoriaPelota.Instance.UpdateTrajectory(force, rb, rb.position);
        holding = true;
    }

    private void OnMouseUp()
    {
        endTime = Time.time;
        endPos = Input.mousePosition;
        swipeDistance = (endPos - startPos).magnitude;
        swipeTime = endTime - startTime;
        

        if (swipeTime < 0.5f && swipeDistance > 30f)
        {
            // Throw ball
            CalSpeed();
            CalAngle();
            rb.AddForce(new Vector3(angle.x * BallSpeed, angle.y * BallSpeed, angle.z * BallSpeed * (-10)));
            rb.useGravity = true;
            holding = false;
            thrown = true;
            Invoke("ResetBall", 6f);
        }
        else
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        angle = Vector3.zero;
        endPos = Vector2.zero;
        startPos = Vector2.zero;
        BallSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;
        thrown = holding = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        transform.position = resetPos;
    }

    void PickupBall()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 30f;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 80f * Time.deltaTime);
    }

    private void Update()
    {
        if (holding)
        {
            OnMouseDrag();
            //PickupBall();
        }
       
    }

    private void CalAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, Camera.main.nearClipPlane + 5));
    }

    void CalSpeed()
    {
        if (swipeTime > 0)
        {
            BallVelocity = swipeDistance / swipeTime;
            BallSpeed = BallVelocity * 40;
            if (BallSpeed > MaxBallSpeed)
            {
                BallSpeed = MaxBallSpeed;
            }
        }
    }
}
