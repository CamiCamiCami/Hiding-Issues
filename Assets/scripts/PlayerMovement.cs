using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed;
    public CharacterController cc;
    public float Gravity;

    private bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        if (!canMove) {
            return;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        Vector3 new_movement = new Vector3(x, 0, z);
        new_movement *= Speed * Time.deltaTime;
        new_movement.y = -Gravity;

        new_movement = this.transform.TransformDirection(new_movement);
        

        cc.Move(new_movement);

    }

    public void DisableMovement() {
        canMove = false;
    }

    public void EnableMovement() {
        canMove = true;
    }

}
