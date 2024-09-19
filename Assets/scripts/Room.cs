using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Room : MonoBehaviour
{

    public int SecurityPercentage;
    public BoxCollider box;

    private bool isPercentageRevealed = false;
    private bool __reveal_next_iteration = false;

    // Start is called before the first frame update
    void Start()
    {
        box.isTrigger = true;
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Player player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            Debug.Log("poteadilla " + player.name);
            player.EnteredRoom(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other);
        Player player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            player.ExitedRoom(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (__reveal_next_iteration)
        {
            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.UpdateRoom(this);
                __reveal_next_iteration = false;
            }
        }
    }

    public void RevealSecurityPercentage()
    {
        isPercentageRevealed = true;
        __reveal_next_iteration = true;
    }

    public int? GetSecurityPercentage()
    {
        if (isPercentageRevealed)
        {
            return SecurityPercentage;
        } else
        {
            return null;
        }
    }

    public bool IsSecurityPercentageRevealed()
    {
        return isPercentageRevealed;
    }
}
