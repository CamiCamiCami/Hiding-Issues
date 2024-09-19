using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    public BoxCollider box;

    void Start()
    {
        if (box == null)
        {
            box = GetComponent<BoxCollider>();
        }
        box.isTrigger = true;
        box.size = new Vector3(3.5f, 3.5f, 3.5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.CanHide();
            Debug.Log("Me com� un mueble.");
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.CannotHide();
            Debug.Log("Me desescond�.");
        }
    }
}
