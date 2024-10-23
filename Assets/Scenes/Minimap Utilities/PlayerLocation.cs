using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    public GameObject parent;
    public GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        // Find the child Transform
        //if (child == null)
            //child = parent.GetComponentInChildren<Transform>().gameObject;

        //If you want to find it by TAG. For this you have to make sure you give your player object the tag "Player".
        //child = GameObject.FindGameObjectWithTag("Jugador");
        

    }

    // Update is called once per frame
    void Update()
    {
        //child = GameObject.Find("Player0");
        child = GameObject.FindGameObjectWithTag("Player");
        //child = parent.GetComponentInChildren<Transform>().gameObject;
        //Debug.Log("Player Position: X = " + rb.position.x + " --- Y = " + rb.position.y + " --- Z = " + rb.position.z);
        Debug.Log("Player Position: X = " + child.transform.position.x + " --- Y = " + child.transform.position.y + " --- Z = " +
        child.transform.position.z);
    }
}
