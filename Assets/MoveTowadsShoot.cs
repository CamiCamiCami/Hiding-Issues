using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowadsShoot : MonoBehaviour
{

    private Vector3 newPosition;

    private bool isTouch;
    // Adjust the speed for the application.
    public float speed = 1.0f;

    // The target (cylinder) position.
    private Transform target;

    [SerializeField]
    private Rigidbody rb; // del target

    // Prepara un cilindro que marca los límites de movimiento de la pelota
    void Awake()
    {
        // Create and position the cylinder. Reduce the size.
        var cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.SetActive(true); // Hide the cylinder.
        cylinder.transform.localScale = new Vector3(0.15f, 1.0f, 0.15f);

        // Grab cylinder values and place on the target.
        target = cylinder.transform;
        target.transform.position = new Vector3(3.5f, 0.5f, -5f);

    }

    private void OnMouseUp()
    {
        isTouch = true;
        //Debug.Log(isTouch);
    }


    void Update()
    {
        if (isTouch)
        {
            Stop();
            PickupBall();
        }
        else
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                // Swap the position of the cylinder.
                target.position = new Vector3(-1.0f * target.position.x, target.position.y, target.position.z);
            }
        }
    }


    void PickupBall()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 30f;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 80f * Time.deltaTime);
    }


    // Detiene el movimiento de la pelota.
    void Stop()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
    }

}
