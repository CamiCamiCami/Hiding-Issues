using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed = 5f;
    public CharacterController cc;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        Vector3 new_movement = new Vector3(x, 0, z);

        new_movement = this.transform.TransformDirection(new_movement);
        new_movement.y = 0;

        cc.Move(new_movement * Speed * Time.deltaTime);

    }

}
