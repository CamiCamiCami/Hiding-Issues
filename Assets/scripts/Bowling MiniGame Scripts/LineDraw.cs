using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numPoints = 50;
    public float timeStep = 0.1f;
    public Vector3 initialVelocity;
    [SerializeField]
    public Transform ball;
    
    Rigidbody rb;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ball = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        initialVelocity = rb.velocity;
    }

    void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Vector3[] points = new Vector3[numPoints];
        Vector3 startingPosition = ball.position;
        Vector3 startingVelocity = initialVelocity;

        for (int i = 0; i < numPoints; i++)
        {
            float time = i * timeStep;
            points[i] = startingPosition + startingVelocity * time + 0.5f * Physics.gravity * time * time;
        }

        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(points);
    }
}
