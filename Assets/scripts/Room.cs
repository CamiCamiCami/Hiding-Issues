using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Puzzle))]
public class Room : MonoBehaviour
{

    public int SecurityPercentage;
    public BoxCollider box;
    public Puzzle puzzle;

    // Start is called before the first frame update
    void Start()
    {
        box.isTrigger = true;
        puzzle = this.GetComponent<Puzzle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            Debug.Log("poteadilla " + player.name);
            player.EnteredRoom(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            player.ExitedRoom(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            player.UpdateRoom(this);
        }
    }

    public int? GetSecurityPercentage()
    {
        if (puzzle != null && puzzle.isSolved)
        {
            return SecurityPercentage;
        } else
        {
            return null;
        }
    }
}
