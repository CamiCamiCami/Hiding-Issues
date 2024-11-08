using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TirarPelota : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    [SerializeField]
    private Rigidbody rb;

    private bool isShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    private void OnMouseDown()
    {
        mousePressDownPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.z)) * forceMultiplier;

        if (!isShoot )
        {
            TrayectoriaPelota.Instance.UpdateTrajectory(forceV, rb, transform.position);
        }
    }

    private void OnMouseUp()
    {
        mouseReleasePos = Input.mousePosition;
        Shoot(mouseReleasePos - mousePressDownPos);
    }

    [SerializeField]
    private float forceMultiplier = 20;

    void Shoot(Vector3 Force)
    {
        if(isShoot)
        {
            return;
        }

        rb.AddForce(new Vector3(Force.x, Force.y, Force.z * (-10)) * forceMultiplier);
        isShoot = false;
    }

}
