using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Room))]
public abstract class Puzzle : MonoBehaviour
{

    protected Room room;

    protected void StartPuzzle()
    {
        room = this.GetComponent<Room>();
    }

    protected virtual void OnSolve()
    {
        room.RevealSecurityPercentage();
    }

    public abstract void HandleComponentInteracted(PuzzleInteractable interactable, Player player);
}
