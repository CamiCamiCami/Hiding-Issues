using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveToManager : MonoBehaviour
{

    public GameObject monster_path;

    private int n = 15;

    private int current_step = 1;

    private float speed = 2f;

    void Awake()
    {
        
    }

    void Update()
    {

        if (current_step <= n) {

            Transform current_transform = this.transform;
            //Debug.Log(current_step.ToString());
            Transform target_transform = monster_path.transform.Find(current_step.ToString());
            this.transform.position = Vector3.Lerp(current_transform.position, target_transform.position, Time.deltaTime * speed);

            if (Vector3.Distance(current_transform.position, target_transform.position) < 0.1f) {

                current_step++;

            }

        }

    }

}
